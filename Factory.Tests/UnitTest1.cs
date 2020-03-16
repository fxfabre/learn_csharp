using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace Factory.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestFullUI()
        {
            // Set User Input
            string[] lines =
            {
                "int",
                "System.Int32",
                "2", "1",                   //remove void
                "1", "12",
                "",
                "1", "42",
                "1", "55",
                "4",
                "2", "42",
                "System.Int32",
                "5",
                "", "", "", ""
            };
            StringReader input = new StringReader(String.Join(Environment.NewLine, lines));
            Console.SetIn(input);

            // Launch the menu
            var factory = new MyFactory();
            factory.MainMenu();
        }
    }
}
