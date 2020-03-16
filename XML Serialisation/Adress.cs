using System;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;
using NLog;

namespace XML_Serialisation
{
    public class Adress
    {
        #region private fields
        private static readonly Logger logger = LogManager.GetLogger("Adress");
        private readonly XmlNode xmlNode;
        #endregion


        #region public properties
        [XmlAttribute("PostalCode")]
        public string PostalCode { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
        #endregion


        #region Constructors
        public Adress()
        {
            this.PostalCode = string.Empty;
            this.Street = string.Empty;
            this.City = string.Empty;
            this.Country = string.Empty;
        }

        public Adress(XmlNode node)
        {
            PostalCode = string.Empty;
            Street = string.Empty;
            City = string.Empty;
            Country = string.Empty;

            xmlNode = node;
            BuildFromNode();
            logger.Info(string.Format("Create Adress ({0}, {1}, {2})", Street, City, Country));
        }
        #endregion


        public void BuildFromNode()
        {
            if (xmlNode == null)
            {
                Console.WriteLine("Can't create Adress with empty node");
                logger.Error("BuildFromNode : Can't create Adress with empty node"); 
                return;
            }

            XmlAttributeCollection attributeZip = xmlNode.Attributes;
            PostalCode = attributeZip.GetNamedItem("PostalCode").InnerText;
            logger.Info("Zip code : " + PostalCode);

            XmlNode nodeAdresses = xmlNode.SelectSingleNode("Street");
            Street = nodeAdresses == null ? string.Empty : nodeAdresses.InnerText;
            logger.Info("Street " + Street);

            XmlNode nodeCity = xmlNode.SelectSingleNode("City");
            City = nodeCity == null ? string.Empty : nodeCity.InnerText;
            logger.Info("City : " + City);

            XmlNode nodeCountry = xmlNode.SelectSingleNode("Country");
            Country = nodeCountry == null ? string.Empty : nodeCountry.InnerText;
            logger.Info("Country : " + Country);
        }

    }
}