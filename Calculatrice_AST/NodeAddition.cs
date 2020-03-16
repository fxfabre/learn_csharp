using System;
using System.Collections.Generic;

namespace Calculatrice_AST
{
    public sealed class NodeAddition : NodeOperation
    {
        private static readonly Dictionary<char, Func<double, double, double>> dictionary
            = new Dictionary<char, Func<double, double, double>>
              {
                  { '+', (a, b) => (a + b) },
                  { '-', (a, b) => (a - b) }
              };


        public override Dictionary<char, Func<double, double, double>> Dictionary
        {
            get { return dictionary; }
        }

        #region Constructeurs

        public NodeAddition(char val) : base(val)
        {
        }

        #endregion

        public override bool IsSomme()
        {
            // renvoie vrai tant qu'on n'a pas intégré dans l'arbre
            return !IsTree;
        }

        public static bool IsOperationSupported(char c)
        {
            return dictionary.ContainsKey(c);
        }
    }
}