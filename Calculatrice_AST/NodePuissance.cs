using System;
using System.Collections.Generic;

namespace Calculatrice_AST
{
    internal class NodePuissance : NodeOperation
    {
        private static readonly Dictionary<char, Func<double, double, double>> dictionary
            = new Dictionary<char, Func<double, double, double>>
              {
                  { '^', Math.Pow }
              };

        #region Constructeurs
        
        public NodePuissance(char val) : base(val)
        {
        }

        #endregion

        public override Dictionary<char, Func<double, double, double>> Dictionary
        {
            get { return dictionary; }
        }

        public override bool IsPuissance()
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