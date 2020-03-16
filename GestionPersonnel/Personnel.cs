using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using NLog;

namespace GestionPersonnel
{
    public class Personnel
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
           
        [XmlElement("Personne")]
        public List<Personne> ListPersonnes { get; private set; }


        public Personnel()
        {
            logger.Info("Default ctor");
            this.ListPersonnes = new List<Personne>();
        }

        public Personnel(string fileName) : this()
        {
            logger.Info("Constructor with fileName");
            this.ImportXml(fileName);
        }

        public Personnel(XmlNode xmlNode) : this()
        {
            logger.Info("ctor from XmlNode");
            logger.Trace(xmlNode.ToString());

            XmlNodeList nodePersonne = xmlNode.SelectNodes("Personne");
            this.AddPerson(nodePersonne);
        }


        public void ImportXml(string fileName)
        {
            logger.Info("Importing Personne from file " + fileName);

            XmlDocument xmlDocument = null;
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

            if (xmlDocument != null)
            {
                this.Deserialize(xmlDocument);
            }
        }

        public void ExportXml(string fileName)
        {
            logger.Info("Exporting Person in XML file");

            // serialize
            var xs = new XmlSerializer(typeof (Personnel));
            using (var wr = new StreamWriter(fileName))
            {
                xs.Serialize(wr, this);
            }
        }

        public void Deserialize(XmlDocument xmlDocument)
        {
            logger.Info("Importing xmlDocument");

            bool getError = false;

            if (xmlDocument == null)
            {
                return;
            }
            logger.Trace("Full XML document : " + xmlDocument.ToString());

            XmlElement root = xmlDocument.DocumentElement;
            if (root == null)
            {
                logger.Error("Cannot get root element from XML document");
                logger.Error("Cancelling operation");
                getError = true;
            }

            if (! getError)
            {
                XmlNodeList nodeList = root.SelectNodes("Personne");

                if (nodeList == null)
                {
                    logger.Warn("Empty file");
                }
                else
                {
                    this.AddPerson(nodeList);
                    logger.Info("Adding person");
                    logger.Trace(nodeList.ToString());
                }
            }
        }

        public bool AddPerson(XmlNodeList listNode)
        {
            logger.Info("Adding person from XmlNodeList");

            bool returnvalue = true;

            if (listNode == null)
            {
                logger.Info("Node null. No one to import");
                return true; // OK
            }

            foreach (XmlNode xmlNode in listNode)
            {
                logger.Info("Adding node to ListPerson");
                returnvalue &= this.AddPerson(xmlNode);
            }
            logger.Info("return value fonction AddPerson(XmlNodeList) = " + returnvalue);
            return returnvalue;
        }

        public bool AddPerson(XmlNode xmlNode)
        {
            logger.Info("Adding person from XmlNode");
            var personne = new Personne(xmlNode);
            return this.AddPerson(personne);
        }

        public bool AddPerson(Personne person)
        {
            logger.Info("Adding person to list " + person.ToString());
            bool returnValue = false;

            if (! this.ListPersonnes.Contains(person))
            {
                this.ListPersonnes.Add(person);
                returnValue = true;
            }
            else
            {
                logger.Warn("Person already exist in list");
            }
            return returnValue;
        }

        public bool RemovePerson(Personne person)
        {
            logger.Info("Removing person " + person.ToString());
            bool returnValue = false;

            if (this.ListPersonnes.Contains(person))
            {
                returnValue = this.ListPersonnes.Remove(person);
            }
            logger.Info("Person successfully succeed : " + returnValue);
            return returnValue;
        }

    }
}