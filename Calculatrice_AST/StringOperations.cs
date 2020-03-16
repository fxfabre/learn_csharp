using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NLog;

namespace Calculatrice_AST
{
    public class StringOperations
    {
        #region Fields

        /*******************
         * Fields          *
         *******************/
        private static readonly Logger logger = LogManager.GetLogger("Default");

        private readonly string myOperation;
        private LinkedList<AnodeAst> listeTokens;
        private bool processingFailed;
        private AnodeAst treeOperation;

        public AnodeAst TreeOperation
        {
            get { return treeOperation; }
        }

        #endregion

        #region Constructeurs

        /*******************
         * Constructors    *
         *******************/

        public StringOperations(string s)
        {
            // remplace 1 + 2 par 1+2
            s = Regex.Replace(s, " ", string.Empty);

            // remplace 19.2 par 19,2
            s = Regex.Replace(s, @"\.", ",");

            processingFailed = false;
            myOperation = s;
        }

        #endregion

        /*******************
         * Methodes        *
         *******************/

        private LinkedListNode<AnodeAst> FindNode(LinkedList<AnodeAst> liste,
            Func<LinkedListNode<AnodeAst>, bool> predicate)
        {
            LinkedListNode<AnodeAst> currentNode = liste.First;

            while (currentNode != null)
            {
                if (predicate(currentNode))
                {
                    return currentNode;
                }
                currentNode = currentNode.Next;
            }
            return null;
        }

        private bool ComputeParentheses(LinkedList<AnodeAst> list)
        {
            if (processingFailed) return false;

            LinkedListNode<AnodeAst> openingParenthesis = FindNode(list, e => e.Value.IsParentheseOuvrante());
            LinkedListNode<AnodeAst> closingParenthesis = null;
            LinkedListNode<AnodeAst> currentNode = openingParenthesis;
            int parenthesisCount = 0;
            bool foundParenthesis = false;

            // Find the opening and closing parenthesis :
            // Find a subset : From (3+(2+5))+(1+2) : extract first (3+(2+5)) and then (1+2)
            while (currentNode != null)
            {
                if (currentNode.Value.IsParentheseOuvrante())
                {
                    // +1 pour parenthese ouvrante
                    ++parenthesisCount;
                }
                else if (currentNode.Value.IsParentheseFermante())
                {
                    // -1 pour parenthese fermante
                    --parenthesisCount;
                    if (parenthesisCount == 0)
                    {
                        closingParenthesis = currentNode;
                        currentNode = list.Last; // get out of the loop
                    }
                }
                currentNode = currentNode.Next;
            }

            // If we found parenthesis
            if (closingParenthesis != null)
            {
                foundParenthesis = true;

                var subList = new LinkedList<AnodeAst>();
                // Isolate the operations inside the parenthesis
                currentNode = openingParenthesis.Next;
                while (currentNode != closingParenthesis)
                {
                    subList.AddLast(currentNode.Value);
                    currentNode = currentNode.Next;
                }

                // Compute operation
                AnodeAst resultNode = ComputeSubTree(subList);

                // add resultNode in list
                list.AddBefore(openingParenthesis, resultNode);

                // Delete nodes in list
                currentNode = openingParenthesis.Next;
                while (currentNode != closingParenthesis)
                {
                    list.Remove(openingParenthesis);
                    openingParenthesis = currentNode;
                    currentNode = currentNode.Next;
                }
                list.Remove(openingParenthesis);
                list.Remove(currentNode);
            }

            return foundParenthesis;
        }
        
        private void ComputeOperation(LinkedList<AnodeAst> list, Func<LinkedListNode<AnodeAst>, bool> predicate)
        {
            if (processingFailed) return;

            LinkedListNode<AnodeAst> node = FindNode(list, predicate);
            while (node != null)
            {
                processingFailed |= node.Value.ToSubTree(list, node);

                if (processingFailed)
                {
                    node = null;
                }
                else
                {
                    // Find next node multiplication
                    node = FindNode(list, predicate);
                }
            }
        }


        /*******************
         * Export          *
         *******************/

        /**
         * TODO : a la fin de la création de la liste, vérifier à chaque fois que le 
         *      noeud de gauche et celui de droit sont compatibles avec l'opération
         *      -> définition d'une pseudo grammaire
         */

        private void ToList()
        {
            if (processingFailed) return;
            if (listeTokens != null) return;

            listeTokens = new LinkedList<AnodeAst>();

            // pour différencier le traitement des moins : 12 * -5 ou 12 - 5
            bool previousIsOperator = true;
            string nombre = string.Empty;

            for (int i = 0; i < myOperation.Length; ++i)
            {
                //  digit point et virgule
                if (NodeNumber.IsOperationSupported(myOperation[i]))
                {
                    do
                    {
                        nombre += myOperation[i];
                        ++i;
                    } while ( (i < myOperation.Length) && NodeNumber.IsOperationSupported(myOperation[i]) );
                    --i;

                    double value = double.Parse(nombre);
                    nombre = string.Empty;
                    listeTokens.AddLast(new NodeNumber(value));

                    previousIsOperator = false;
                }
                    // ( )
                else if (NodeControl.IsOperationSupported(myOperation[i]))
                {
                    listeTokens.AddLast(new NodeControl(myOperation[i]));
                    previousIsOperator = false;
                }
                    //  ^
                else if (NodePuissance.IsOperationSupported(myOperation[i]))
                {
                    if (previousIsOperator)
                    {
                        processingFailed = true;
                        i = myOperation.Length;
                    }
                    else
                    {
                        listeTokens.AddLast(new NodePuissance(myOperation[i]));
                        previousIsOperator = true;
                    }
                }
                    //  * /
                else if (NodeMultiplication.IsOperationSupported(myOperation[i]))
                {
                    if (previousIsOperator)
                    {
                        processingFailed = true;
                        i = myOperation.Length;
                    }
                    else
                    {
                        listeTokens.AddLast(new NodeMultiplication(myOperation[i]));
                        previousIsOperator = true;
                    }
                }
                    //  Fubini et Factorielle
                else if (NodeFunction.IsOperationSupported(myOperation[i]))
                {
                    string s = string.Empty;
                    while ((i < myOperation.Length) && NodeFunction.IsOperationSupported(myOperation[i]))
                    {
                        s = s + myOperation[i];
                        ++i;
                    }
                    --i;
                    listeTokens.AddLast(new NodeFunction(s));
                    previousIsOperator = true;
                }
                    //  + -
                else if (NodeAddition.IsOperationSupported(myOperation[i]))
                {
                    // 3 * -2 -> traitement du moins comme partie du nombre 2
                    if (previousIsOperator)
                    {
                        nombre += myOperation[i];
                    }
                        // 3 - 2 -> ajout du moins comme un opérateur
                    else
                    {
                        listeTokens.AddLast(new NodeAddition(myOperation[i]));
                        previousIsOperator = true;
                    }
                }
                else
                {
                    logger.Warn("Unknown char : " + myOperation[i]);
                    processingFailed = true;

                    // get out of the loop
                    i = myOperation.Length;
                }
            }

            if (! processingFailed)
            {
                CheckList();
            }
        }

        private void CheckList()
        {
            int countParenthesis = 0;
            LinkedListNode<AnodeAst> currentNode = listeTokens.First;

            while (currentNode != null)
            {
                if (currentNode.Value.IsParentheseOuvrante())
                {
                    countParenthesis++;
                }
                else if (currentNode.Value.IsParentheseFermante())
                {
                    countParenthesis--;
                }

                if (countParenthesis < 0)
                {
                    logger.Warn("Wrong string operation. Check parenthesis !");
                    processingFailed = true;
                    currentNode = null;
                }
                else
                {
                    currentNode = currentNode.Next;
                }
            }

            if (countParenthesis != 0)
            {
                logger.Warn("Wrong string operation. Please close all parenthesiss !");
                processingFailed = true;
            }
        }

        private void ComputeTree()
        {
            ToList();

            if (! processingFailed)
            {
                treeOperation = ComputeSubTree(listeTokens);
            }
        }

        private AnodeAst ComputeSubTree(LinkedList<AnodeAst> liste)
        {
            // Construction de l'arbre suivant les priorités de calcul
            while (ComputeParentheses(liste)) ;
            ComputeOperation(liste, e => e.Value.IsFonction());
            ComputeOperation(liste, e => e.Value.IsPuissance());
            ComputeOperation(liste, e => e.Value.IsMultiplication());
            ComputeOperation(liste, e => e.Value.IsSomme());

            return liste.First.Value;
        }

        public double? EvalTree()
        {
            ComputeTree();
            return processingFailed ? null : treeOperation.EvalNode();
        }


        /*******************
         * Display         *
         *******************/

        public string DisplayString()
        {
            return myOperation;
        }

        public void DisplayList()
        {
            ToList();

            foreach (AnodeAst node in listeTokens)
            {
                Console.Write(node + "  ");
            }
            Console.WriteLine();
        }
    }
}