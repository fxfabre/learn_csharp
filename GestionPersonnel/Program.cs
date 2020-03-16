using System;

namespace GestionPersonnel
{
    public static class Program
    {
        static void Main()
        {
            string xmlFile = "../../Personnel.xml";

            // Test serialisation
            var personnel = new Personnel();

            personnel.AddPerson(new Personne
            {
                Nom = "Nom1",
                Prenom = "Prenom1",
                Adresse = "Adresse1",
                Email = "lala@chezmoicamarche.fr"
            });

            personnel.AddPerson(new Personne
            {
                Nom = "Nom2",
                Prenom = "Prenom2",
                Adresse = "Adresse2",
                Email = "lala2@chezmoicamarche.fr"
            });
            Console.WriteLine(personnel.ToString());

            // serialize
            personnel.ExportXml(xmlFile);


            // Deserialisation
            Console.WriteLine("Chargement du personnel");
            var personnel2 = new Personnel(xmlFile);
            Console.WriteLine(personnel2.ToString());


            Console.Read();
        }
    }
}
