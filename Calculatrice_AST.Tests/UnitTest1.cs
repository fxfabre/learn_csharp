using System;
using System.IO;
using NUnit.Framework;

namespace Calculatrice_AST.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        /***********************************
         * Tests for the processing engine *
         ***********************************/
        #region Tests basiques
        // Nombres seuls
        [TestCase("0", 0.0)]
        [TestCase("2", 2.0)]
        [TestCase("-3", -3.0)]
        [TestCase("12.5", 12.5)]
        [TestCase("-59.3", -59.3)]
        [TestCase("12,5", 12.5)]
        [TestCase("-59,3", -59.3)]
        // Additions / soustractions sur entiers
        [TestCase("0-1", -1.0)]
        [TestCase("2-5", -3.0)]
        [TestCase("2-4+3-5", -4.0)]
        [TestCase("2+4-3+5", 8.0)]
        [TestCase("-1+0", -1.0)]
        [TestCase("-2+5", 3.0)]
        [TestCase("-5-4", -9.0)]
        [TestCase("+5-4", 1.0)]
        [TestCase("1-+2", -1.0)]
        [TestCase("1--2", 3.0)]
        [TestCase("1++2", 3.0)]
        [TestCase("1+-2", -1.0)]
        [TestCase("1*+2", 2.0)]
        [TestCase("1*-2", -2.0)]
        [TestCase("1/+2", 0.5)]
        [TestCase("1/-2", -0.5)]
        [TestCase("1++2+-3-+4--5/+1+5/-5+1*+5+2*-3", -1)]
        // Additions / soustractions sur reels
        [TestCase("0-1.2", -1.2)]
        [TestCase("2.5-5.5", -3.0)]
        [TestCase("2-4+3.9-5", -3.1)]
        [TestCase("2.1+4-3.2+5", 7.9)]
        [TestCase("-2.2+5", 2.8)]
        [TestCase("-5.4-4.6", -10.0)]
        // Additions / multiplications sur entiers
        [TestCase("0+0", 0.0)]
        [TestCase("2+3", 5.0)]
        [TestCase("2+3+4", 9.0)]
        [TestCase("2*1", 2.0)]
        [TestCase("2*3", 6.0)]
        [TestCase("2*3*4", 24.0)]
        [TestCase("-2*3*4*5", -120.0)]
        [TestCase("2*3*1*-4*5", -120.0)]
        [TestCase("2*-3*1*-4*5", 120.0)]
        // divisions avec entiers / reels
        [TestCase("4/2", 2.0)]
        [TestCase("22/7", 3.1428571429)]
        [TestCase("-11/4", -2.75)]
        [TestCase("-17/-25", 0.68)]
        #endregion
        #region Fonctions complexes
        [TestCase("fibo(2)", 2.0)]
        [TestCase("3!", 6.0)]
        [TestCase("3^4", 81.0)]
        [TestCase("2*(3+5)", 16.0)]
        [TestCase("(2*3*fibo(5+2)*(2+5))*3!", 5292.0)]
        [TestCase("fibo(2*(3+5))", 1597.0)]
        #endregion
        public void TestCalculatriceAst(string s, double r)
        {
            StringOperations myOper = new StringOperations(s);
            myOper.DisplayList();

            double? resultNullable = myOper.EvalTree();

            Assert.NotNull(resultNullable);

            double result = resultNullable.Value;
            Console.WriteLine("resultat : " + result);
            Assert.IsTrue(Math.Abs(r - result) < 1e-8);
        }


        /************************************
         * Errors for the processing engine *
         ************************************/
        #region Test erreurs basiques
        [TestCase("aa")]
        [TestCase("*2+1")]
        [TestCase("/12")]
        [TestCase("6+lala+15")]
        [TestCase("3*xx-*2")]
        [TestCase("12+-*/3628")]
        [TestCase("1+*2")]
        [TestCase("1+/2")]
        [TestCase("1-*2")]
        [TestCase("1-/2")]
        [TestCase("1**2")]
        [TestCase("1*/2")]
        [TestCase("1/*2")]
        [TestCase("1//2")]
        #endregion
        #region Erreurs fonctions complexes
        [TestCase("2*)3+4(-5")]
        #endregion
        public void ErrorCalculatriceAst(string s)
        {
            StringOperations myOper = new StringOperations(s);
            myOper.DisplayList();

            double? result = myOper.EvalTree();
            Assert.IsNull(result);

        }

        

        [TestCase(0, 1)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 6)]
        [TestCase(4, 24)]
        [TestCase(5, 120)]
        [TestCase(6, 720)]
        public void TestFactorielle(int n, int expected)
        {
            // Test empty function !
            Program.Main();

            // Test Factorielle
            int result = Tools.Factorielle(n);
            Assert.AreEqual(expected, result);
        }

        
        [TestCase(0, 1)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        [TestCase(4, 5)]
        [TestCase(5, 8)]
        [TestCase(6, 13)]
        [TestCase(7, 21)]
        [TestCase(8, 34)]
        [TestCase(9, 55)]
        public void TestFibonacci(int n, int expected)
        {
            int result = Tools.Fibonacci(n);
            Assert.AreEqual(expected, result);
        }


    }
}