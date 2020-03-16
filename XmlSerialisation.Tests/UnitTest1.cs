using System.IO;
using System.Xml;
using NUnit.Framework;
using XML_Serialisation;

namespace XmlSerialisation.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestAdress()
        {
            var adress = new Adress();
            adress.BuildFromNode();
        }

        [Test]
        public void TestCompagny()
        {
            var comp = new Compagny();

            // test xmlNode = null
            comp.BuildFromNode();

            XmlNodeList node = null;
            comp.AddAddress(node);
            comp.AddFilliales(node);
        }

        [Test]
        public void TestMethod1()
        {
            string file1 = "../../First.xml";
            string file2 = "../../FirstOut.xml";
            string file3 = "../../SecondOut.xml";

            // Déserialize - Serialize
            var compagny1 = new XmlCompagnies(file1);
            compagny1.Serialize(file2);

            var compagny2 = new XmlCompagnies(file2);
            compagny2.Serialize(file3);

            string text1 = File.ReadAllText(file2);
            string text2 = File.ReadAllText(file3);

            Assert.AreEqual(text1, text2);
        }

        [Test]
        public void TestXmlCompagnies()
        {
            var compagnies = new XmlCompagnies();

            // Catch exception
            var compagnies2 = new XmlCompagnies("lala");

            compagnies.Deserialize();

            compagnies.xmlDocument = new XmlDocument();
            compagnies.Deserialize();
        }
    }
}