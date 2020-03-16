using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace MyWhereProject.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(20, 10)]
        [TestCase(21, 11)]
        [TestCase(22, 11)]
        public void TestWhere(int N, int memberCount)
        {
            // Test main function (empty !!)
            Program.Main(new string[3]);

            // Test MyWhere function
            LinkedList<int> myList = new LinkedList<int>();
            for (int i = 0; i < N; ++i)
            {
                myList.AddFirst(i);
            }

            var res = myList.MyWhere(e => e%2 == 0).Count();

            Assert.AreEqual(memberCount, res);
        }
    }
}
