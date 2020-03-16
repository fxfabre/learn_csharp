using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using NLog;

namespace XML_Serialisation
{
    public class Compagny
    {
        #region Private fields
        private static readonly Logger logger = LogManager.GetLogger("Compagny");
        private readonly XmlNode xmlNode;
        #endregion


        #region public properties
        [XmlElement("Address")]
        public List<Adress> Adresses { get; set; }

        [XmlArray("Filliales")]
        [XmlArrayItem("Nom")]
        public List<string> Filiales { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }
        #endregion


        #region Constructors
        public Compagny()
        {
            logger.Info("Building object Compagny with default parameters");
            this.xmlNode = null;
            this.Adresses = new List<Adress>();
            this.Filiales = new List<string>();
        }

        public Compagny(XmlNode node)
        {
            logger.Info("Building object Compagny from a node");

            xmlNode = node;
            BuildFromNode();

            logger.Info("Compagny " + Name + " created !");
        }
        #endregion


        public void BuildFromNode()
        {
            logger.Info("Building object Compagny from node");

            if (xmlNode == null)
            {
                Console.WriteLine("Null node !");
                logger.Error("Can't build object from node : null node");
                return;
            }

            Adresses = new List<Adress>();
            Filiales = new List<string>();

            XmlAttributeCollection attributeZip = xmlNode.Attributes;
            Name = attributeZip == null ? string.Empty : attributeZip.GetNamedItem("Name").InnerText;
            logger.Info("Compagny Name : " + Name);

            XmlNodeList nodeAdresses = xmlNode.SelectNodes("Address");
            this.AddAddress(nodeAdresses);

            XmlNodeList nodeFilliales = xmlNode.SelectNodes("Filliales/Nom");
            this.AddFilliales(nodeFilliales);
        }

        public void AddAddress(XmlNodeList nodeAdresses)
        {
            if (nodeAdresses != null)
            {
                foreach (XmlNode nodeAdress in nodeAdresses)
                {
                    var adress = new Adress(nodeAdress);
                    Adresses.Add(adress);
                    logger.Info("New Adress added to compagny, " + adress.Street);
                }
            }
            else
            {
                logger.Warn("can't find node Address");
            }
        }

        public void AddFilliales(XmlNodeList nodeFilliales)
        {
            if (nodeFilliales != null)
            {
                foreach (XmlNode nodeFiliale in nodeFilliales)
                {
                    Filiales.Add(nodeFiliale.InnerText);
                    logger.Info("Adding filiale Name : " + nodeFiliale.InnerText);
                }
            }
            else
            {
                logger.Warn("can't find node Filliales");
            }
        }

    }
}