using System;
using Calculatrice;
using NUnit.Framework;

namespace Calculatrice.Tests
{
    [TestFixture]
    public class UnitTest1
    {
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
        [TestCase("1+-2", -1.0)]
        [TestCase("1/-2", -0.5)]
        // Additions / soustractions sur reels
        [TestCase("0-1.2", -1.2)]
        [TestCase("2.5-5.5", -3.0)]
        [TestCase("2-4+3.9-5", -3.1)]
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
        [TestCase("-11/4", -2.75)]
        [TestCase("-17/-25", 0.68)]
        #endregion
        public void TestCalculatrice(string s, double r)
        {
            // test empty function !
            ProgramCalculatrice.Main();

            // test the calculator
            double result = Calculette.Eval(s);
            Assert.AreEqual(r, result);
        }



    }
}