using System;
using NUnit.Framework;

namespace ListeChainee.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestGetSomme()
        {
            // Test empty function !
            Program.Main();

            ListeSimplementChainee chaine = new ListeSimplementChainee();

            Assert.AreEqual(0, chaine.GetSomme());
            Assert.AreEqual(chaine.NbElement, 0);
        }

        [Test]
        public void TestAddMaillons()
        {
            ListeSimplementChainee chaine = new ListeSimplementChainee();

            chaine.Add(3);
            chaine.Add(1);
            chaine.Add(42);
            chaine.Add(12);
            chaine.AddEnTete();
            chaine.Add();
            Assert.AreEqual(58, chaine.GetSomme());
            Assert.AreEqual(chaine.NbElement, 6);
        }

        [Test]
        public void TestAddEnTete()
        {
            ListeSimplementChainee chaine = new ListeSimplementChainee();

            chaine.AddEnTete(3);
            chaine.AddEnTete(1);
            chaine.AddEnTete(42);
            chaine.AddEnTete(12);

            Assert.AreEqual(58, chaine.GetSomme());
            Assert.AreEqual(chaine.NbElement, 4);
        }

        [TestCase(new int[]{3, 42, 12, 1}, new int[]{36, 0, 39}, 58, 4)]    // remove void
        [TestCase(new int[]{3, 1, 42, 12}, new int[]{36, 0, 3 }, 55, 3)]    // remove en tete
        [TestCase(new int[]{37, 3, 1, 12}, new int[]{0 , 0, 3 }, 50, 3)]    // remove au milieu
        [TestCase(new int[]{5, 10, 12, 4}, new int[]{0 , 0, 12}, 19, 3)]    // remove avant dernier
        [TestCase(new int[]{3, 1, 42, 12}, new int[]{0 , 0, 12}, 46, 3)]    // remove dernier element
        public void TestremoveArray(int[] add, int[] remove, int total, int nbElem)
        {
            ListeSimplementChainee chaine = new ListeSimplementChainee();

            chaine.Add(add[0]);
            chaine.SupprimerMaillon(remove[0]);
            chaine.Add(add[1]);
            chaine.Add(add[2]);
            chaine.SupprimerMaillon(remove[1]);
            chaine.Add(add[3]);
            chaine.SupprimerMaillon(remove[2]);

            Assert.AreEqual(total,  chaine.GetSomme());
            Assert.AreEqual(nbElem, chaine.NbElement );
        }
        
        [Test]
        public void TestInverserListe()
        {
            ListeSimplementChainee chaine = new ListeSimplementChainee();

            chaine.Add(2);
            chaine.Inverser();

            chaine.Add(1);
            chaine.Inverser();

            chaine.Add(3);
            chaine.Add(4);
            chaine.Inverser();

            chaine.DisplayChaine();

            Assert.AreEqual(10, chaine.GetSomme());
            Assert.AreEqual(chaine.NbElement, 4);
        }

    }
}
