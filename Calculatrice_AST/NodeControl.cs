using System.Collections.Generic;
using NLog;

namespace Calculatrice_AST
{
    internal class NodeControl : AnodeAst
    {
        private static readonly Logger logger = LogManager.GetLogger("Default");

        #region Fields

        /*******************
         * Fields          *
         *******************/
        private static readonly Dictionary<char, bool> dictionary
            = new Dictionary<char, bool>
              {
                  { '(', true },
                  { ')', true }
              };

        private readonly char content;

        #endregion
        
        public NodeControl(char val)
        {
            content = val;
        }

        public override double? EvalNode()
        {
            logger.Error("This parenthesis " + content + " must not be in the tree !");
            return null;
        }

        public static bool IsOperationSupported(char c)
        {
            return dictionary.ContainsKey(c);
        }

        public override string ToString()
        {
            return "" + content;
        }

        public override bool IsParentheseOuvrante()
        {
            return content == '(';
        }

        public override bool IsParentheseFermante()
        {
            return content == ')';
        }
    }
}