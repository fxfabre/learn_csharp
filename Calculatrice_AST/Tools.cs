using System.Collections.Generic;

namespace Calculatrice_AST
{
    public static class Tools
    {
        public static int Factorielle(int n)
        {
            int total = 1;
            for (int i = 2; i <= n; ++i)
            {
                total *= i;
            }
            return total;
        }

        public static int Fibonacci(int n)
        {
            int valueMoins2 = 0;
            int valueMoins1 = 0;
            int currentValue = 1;
            for (int i = 1; i <= n; ++i)
            {
                valueMoins2 = valueMoins1;
                valueMoins1 = currentValue;
                currentValue = valueMoins1 + valueMoins2;
            }
            return currentValue;
        }


    }
}