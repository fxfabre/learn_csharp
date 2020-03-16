using System.Collections.Generic;
using NLog;

namespace Calculatrice_AST
{
    public abstract class AnodeAst
    {
        protected AnodeAst()
        {
            IsTree = false;
        }

        protected bool IsTree { get; set; }

        public abstract double? EvalNode();

        public virtual bool IsMultiplication()
        {
            return false;
        }

        public virtual bool IsPuissance()
        {
            return false;
        }

        public virtual bool IsSomme()
        {
            return false;
        }

        public virtual bool IsParentheseOuvrante()
        {
            return false;
        }

        public virtual bool IsParentheseFermante()
        {
            return false;
        }

        public virtual bool IsFonction()
        {
            return false;
        }

        public virtual bool ToSubTree(LinkedList<AnodeAst> list, LinkedListNode<AnodeAst> currentNode)
        {
            // Error : shouldn't call this function
            return true;
        }
    }
}