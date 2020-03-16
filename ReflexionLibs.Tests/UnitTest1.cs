using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ReflexionLibs.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMain()
        {
            ReflexionLibs.Program.Main();
        }


        [Test]
        public void TestGetAssemblyFromFileName()
        {
            AssemblyInfo.GetCurrentassembly();

            string filepath = @"C:\unknown\file.dll";
            AssemblyInfo.GetAssemblyFromFilePath(filepath);

            List<Type> assemblyType = AssemblyInfo.GetAssemblyFromFileName("ReflexionLibs.Tests");
            
            Assert.IsTrue(assemblyType.Count(e => e.Name == "UnitTest1") > 0);
            Assert.IsTrue(assemblyType.Count(e => e.Name == "TestClass") > 0);

            Type type = assemblyType[0];
            var fields = AssemblyInfo.GetAllItems(type);

            Assert.IsTrue(fields.Count(e => e.Name == "privateField") == 1);
            Assert.IsTrue(fields.Count(e => e.Name == "ProtectedField") == 1);
            Assert.IsTrue(fields.Count(e => e.Name == "PublicField") == 1);

            Assert.IsTrue(fields.Count(e => e.Name == "PublicMethod") == 2);
            Assert.IsTrue(fields.Count(e => e.Name == "PrivateMethod") == 2);
        }
        
    }
}
