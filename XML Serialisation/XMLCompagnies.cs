using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using NLog;

namespace XML_Serialisation
{
    [XmlRoot("Companies")]
    public class XmlCompagnies
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        [XmlIgnore]
        public XmlDocument xmlDocument { get; set; }

        [XmlElement("Company")]
        public List<Compagny> Compagnies { get; set; }


        public XmlCompagnies()
        {
            logger.Info("Creating object XmlCompagnies with default parameters");
            xmlDocument = null;
            Compagnies = null;
        }

        public XmlCompagnies(string fileName)
        {
            logger.Info("Creating object XmlCompagnies from XML file : " + fileName);

            this.Compagnies = new List<Compagny>();

            try
            {
                string text = File.ReadAllText(fileName);
                xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(text);
            }
            catch (Exception e)
            {
                logger.Error("Error message : " + e.Message);
                Console.WriteLine(e.Message);
                xmlDocument = null;
            }

            logger.Info("Object built");
            this.Deserialize();
        }


        // XML -> Compagnies
        public void Deserialize()
        {
            bool getError = false;
            XmlNodeList nodeList = null;

            logger.Info("Deserializing XmlNode");
            if (xmlDocument == null)
            {
                Console.WriteLine("can't déserialize empty file");
                logger.Error("can't déserialize empty file");
                return;
            }
            
            XmlElement root = xmlDocument.DocumentElement;
            if (root == null)
            {
                logger.Error("Cannot get root element from XML document");
                logger.Error("Cancelling operation");
                getError = true;
            }

            if (! getError)
            {
                nodeList = root.SelectNodes("Company");

                if (nodeList == null)
                {
                    getError = false;
                    logger.Warn("Empty file");
                }
            }

            if (! getError)
            {
                // For each compagny
                foreach (XmlNode node in nodeList)
                {
                    var compagny = new Compagny(node);
                    Compagnies.Add(compagny);
                    logger.Info("New compagny " + compagny.Name + " added");
                }
            }
        }

        // Compagnies -> XML
        public void Serialize(string fileName)
        {
            logger.Info("Serializing object XmlCompagnies");

            // serialize
            var xs = new XmlSerializer(typeof (XmlCompagnies));
            using (var wr = new StreamWriter(fileName))
            {
                xs.Serialize(wr, this);
            }
            logger.Info("Serializing done");
        }
    }
}