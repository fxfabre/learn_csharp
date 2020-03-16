using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Calculatrice
{
    public static class Calculette
    {
        public static double Eval(string s)
        {
            return CalculatriceExplode(s);
        }

        public static double CalculatriceExplode(string s)
        {
            var operation = s;
            operation = FiltreString(operation);
            string[] arrayString = operation.Split('+');

            double total = arrayString.Sum(t => EvalMultiply(t));

            Console.WriteLine(s + " = " + total);
            return total;
        }

        public static string FiltreString(string s)
        {
            var operation = s;

            // remplace 1 + 2 par 1+2
            operation = Regex.Replace(operation, " ", string.Empty);

            // remplace 19.2 par 19,2
            operation = Regex.Replace(operation, @"\.", ",");

            // remplace +2+3 par 2+3
            operation = Regex.Replace(operation, @"^\+(.*)$", "$1");

            // remplace 2-3 par 2+-3
            operation = Regex.Replace(operation, @"(\d)-(\d)", "$1+-$2");

            // remplace 3/2 par 3*0.5
            var matches = Regex.Matches(operation, @"/(\-?[0-9,]+)");
            foreach (Match match in matches)
            {
                string nombre = match.Value.Substring(1, match.Value.Length - 1);
                double replace = 1 / double.Parse(nombre, CultureInfo.CurrentCulture);
                operation = operation.Replace(match.Value, "*" + replace);
            }

            Console.WriteLine("Apres filtre : " + operation);
            return operation;
        }

        public static double EvalMultiply(string s)
        {
            var operation = s;

            Console.Write("Evaluating " + operation);
            string[] result = operation.Split('*');

            double total = 1;
            for (int i = 0; i < result.Count(); ++i)
            {
                total *= double.Parse(result[i], CultureInfo.CurrentCulture);
            }
            Console.WriteLine(" result = " + total);
            return total;
        }

    }

}


