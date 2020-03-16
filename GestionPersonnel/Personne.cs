using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using NLog;

namespace GestionPersonnel
{
    public class Personne
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Adresse { get; set; }
        public string Email { get; set; }

        public Personne()
        {
            logger.Info("Default ctor");
            this.Nom = string.Empty;
            this.Prenom = string.Empty;
            this.Adresse = string.Empty;
            this.Email = string.Empty;
        }

        public Personne(string nom, string prenom, string adress, string email)
        {
            logger.Info("ctor with string parameters");
            this.Nom = nom;
            this.Prenom = prenom;
            this.Adresse = adress;
            this.Email = email;
        }

        public Personne(XmlNode xmlNode)
        {
            logger.Info("Building object Personne from XML node");
            if (xmlNode == null) return;

            logger.Trace(xmlNode.ToString);
            XmlNode node;

            node = xmlNode.SelectSingleNode("Nom");
            Nom = node == null ? string.Empty : node.InnerText;
            logger.Info("Person name = " + Nom);

            node = xmlNode.SelectSingleNode("Prenom");
            Prenom = node == null ? string.Empty : node.InnerText;
            logger.Info("Person first name = " + Prenom);

            node = xmlNode.SelectSingleNode("Adresse");
            Adresse = node == null ? string.Empty : node.InnerText;
            logger.Info("Person address = " + Adresse);

            node = xmlNode.SelectSingleNode("Email");
            Email = node == null ? string.Empty : node.InnerText;
            logger.Info("Person email = " + Email);
        }

        public string AsShortString()
        {
            logger.Info("Getting name from personne : " + Prenom + " " + Nom);
            return Prenom + " " + Nom;
        }

        public override string ToString()
        {
            logger.Info("Person.ToString");
            return Prenom + " " + Nom + "\n" +
                "    " + Adresse + "\n" +
                "    " + Email + "\n";
        }
    }
}
