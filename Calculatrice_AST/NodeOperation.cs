using System;
using System.Collections.Generic;
using System.Globalization;
using NLog;

namespace Calculatrice_AST
{
    public abstract class NodeOperation : AnodeAst
    {
        private static readonly Logger logger = LogManager.GetLogger("Default");

        internal NodeOperation(char val)
        {
            Content = val;
            IsTree = false;
        }

        protected char Content { get; set; }

        protected AnodeAst FilsDroit { get; set; }
        protected AnodeAst FilsGauche { get; set; }

        public abstract Dictionary<char, Func<double, double, double>> Dictionary { get; }

        public override string ToString()
        {
            return Content.ToString(CultureInfo.InvariantCulture);
        }

        public override bool ToSubTree(LinkedList<AnodeAst> list, LinkedListNode<AnodeAst> currentNode)
        {
            bool hadError;
            LinkedListNode<AnodeAst> previousNode = currentNode.Previous;
            LinkedListNode<AnodeAst> nextNode = currentNode.Next;

            if ((previousNode == null) || (nextNode == null))
            {
                logger.Warn("Chaine opération non valide");
                hadError = true;
            }
            else
            {
                // création de l'arbre
                FilsGauche = previousNode.Value;
                FilsDroit = nextNode.Value;

                // Delete childNodes from the list
                list.Remove(previousNode);
                list.Remove(nextNode);

                // dé-marquage du noeud : il doit maintenant être considéré comme un sous-arbre.
                IsTree = true;

                hadError = false;
            }
            if (! hadError)
            {
                logger.Info("Node to Tree {0} {1} {2}", FilsGauche.EvalNode(), Content, FilsDroit.EvalNode());
            }

            return hadError;
        }

        protected bool CheckNode(out double leftVal, out double rightVal)
        {
            leftVal = 0.0;
            rightVal = 0.0;

            if (FilsGauche == null)
            {
                logger.Info("Missing left child on node " + Content);
                return false;
            }
            if (FilsDroit == null)
            {
                logger.Info("Missing right child on node " + Content);
                return false;
            }

            double? valeurGauche = FilsGauche.EvalNode();
            double? valeurDroit = FilsDroit.EvalNode();

            if (valeurGauche == null)
            {
                logger.Info("Can't evaluate left child of node : " + Content);
                return false;
            }
            if (valeurDroit == null)
            {
                logger.Info("Can't evaluate right child of node : " + Content);
                return false;
            }

            leftVal = valeurGauche.Value;
            rightVal = valeurDroit.Value;
            return true;
        }

        public override double? EvalNode()
        {
            double leftVal;
            double rightVal;

            if (CheckNode(out leftVal, out rightVal))
            {
                return Dictionary[Content](leftVal, rightVal);
            }
            return null;
        }
    }
}