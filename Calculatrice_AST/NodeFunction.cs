using System;
using System.Collections.Generic;
using NLog;


namespace Calculatrice_AST
{
    internal class NodeFunction : AnodeAst
    {
        private static readonly Logger logger = LogManager.GetLogger("Default");

        private static readonly Dictionary<string, sidesNumberEnum> sideNumbers
            = new Dictionary<string, sidesNumberEnum>
              {
                  { "fibo", sidesNumberEnum.RIGHT },
                  { "fib", sidesNumberEnum.RIGHT },
                  { "!", sidesNumberEnum.LEFT }
              };

        private static readonly Dictionary<string, Func<int, int>> dictionary
            = new Dictionary<string, Func<int, int>>
              {
                  { "fibo", Tools.Fibonacci },
                  { "fib", Tools.Fibonacci },
                  { "!", Tools.Factorielle }
              };

        protected AnodeAst ChildNode { get; set; }

        private readonly string content;
        private bool hasError;

        #region Constructeurs

        public NodeFunction(string s)
        {
            s = s.ToLower();
            if (dictionary.ContainsKey(s))
            {
                content = s;
                hasError = false;
            }
            else
            {
                content = string.Empty;
                hasError = true;
            }
        }

        #endregion

        public static bool IsOperationSupported(char c)
        {
            return char.IsLetter(c) || (c == '!');
        }

        public override string ToString()
        {
            return content;
        }

        public override bool ToSubTree(LinkedList<AnodeAst> list, LinkedListNode<AnodeAst> currentNode)
        {
            if (hasError) return true;

            LinkedListNode<AnodeAst> numberNode;
            if (sideNumbers[content] == sidesNumberEnum.LEFT)
            {
                numberNode = currentNode.Previous;
            }
            else
            {
                numberNode = currentNode.Next;
            }

            if (numberNode == null)
            {
                logger.Info("String opération not correct");
                hasError = true;
            }
            else
            {
                // création de l'arbre
                ChildNode = numberNode.Value;

                // Delete childNode from the list
                hasError |= ! list.Remove(ChildNode);
            }

            // dé-marquage du noeud : il doit maintenant être considéré comme un sous-arbre.
            IsTree = true;

            logger.Info(string.Format("Function to sub tree {0}, node = {1}", content, ChildNode.EvalNode()));
            return hasError;
        }

        public override double? EvalNode()
        {
            double? result = null;
            int valueInt = 0;

            // if the function is not known
            if (! dictionary.ContainsKey(content))
            {
                hasError = true;
            }

            if (! hasError)
            {
                // Eval the child node and check for errors
                result = ChildNode.EvalNode();
                if (result == null)
                {
                    hasError = true;
                }
            }

            if (! hasError)
            {
                // convert child value to int. Error if too far from the nearest int
                valueInt = (int) Math.Round(result.Value);
                if (Math.Abs(valueInt - result.Value) > 1e-8)
                {
                    hasError = true;
                }
            }

            if (hasError)
            {
                return null;
            }
            return dictionary[content](valueInt);
        }

        public override bool IsFonction()
        {
            return !IsTree;
        }

        private enum sidesNumberEnum
        {
            LEFT,
            RIGHT
        }
    }
}