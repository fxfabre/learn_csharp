using System.Globalization;

namespace Calculatrice_AST
{
    internal class NodeNumber : AnodeAst
    {
        public readonly double Content = 0.0;

        public NodeNumber(double val)
        {
            Content = val;
        }

        public override double? EvalNode()
        {
            return Content;
        }

        public static bool IsOperationSupported(char c)
        {
            return char.IsDigit(c) || (c == ',');
        }

        public override string ToString()
        {
            return Content.ToString(CultureInfo.InvariantCulture);
        }
    }
}