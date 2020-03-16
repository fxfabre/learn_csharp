using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace XML_Serialisation
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            string file1 = "../../Files/First.xml";
            string file2 = "../../Files/FirstOut.xml";

            // Déserialize - Serialize
            var compagny1 = new XmlCompagnies(file1);
            compagny1.Serialize(file2);

            Console.ReadLine();
        }
    }

      
}