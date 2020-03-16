using NUnit.Framework;
namespace GenericChainee.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestEmptyChaineObject()
        {
            Program.Main(new string[2]);
            GenericSimplementChainee<object> chaine = new GenericSimplementChainee<object>();
            chaine.AddFirst(1);
            chaine.SupprimerMaillon(1);

            Assert.AreEqual(chaine.NbElement, 0);
        }

        [Test]
        public void TestAddInt()
        {
            GenericSimplementChainee<int> myChaineInt = new GenericSimplementChainee<int>();

            myChaineInt.AddLast(1337);
            myChaineInt.AddLast(42);
            myChaineInt.AddLast(3);
            myChaineInt.AddLast(1);
            myChaineInt.AddLast(12);

            Assert.AreEqual(myChaineInt.NbElement, 5);
        }
        
        [Test]
        public void TestAddString()
        {
            GenericSimplementChainee<string> myChaineString = new GenericSimplementChainee<string>();

            myChaineString.AddLast("Riri");
            myChaineString.AddLast("Fifi");
            myChaineString.AddLast("Loulou");
            myChaineString.DisplayChaine();

            Assert.AreEqual(myChaineString.NbElement, 3);
        }
        
        [Test]
        public void TestAddObject()
        {
            GenericSimplementChainee<object> myChaineObject = new GenericSimplementChainee<object>();

            myChaineObject.AddFirst("Riri");
            myChaineObject.AddLast(32);
            myChaineObject.AddFirst(12.53);

            Assert.AreEqual(myChaineObject.NbElement, 3);
        }
        
        [Test]
        public void TestAddEnTete()
        {
            GenericSimplementChainee<int> chaine = new GenericSimplementChainee<int>();

            chaine.AddFirst(3);
            chaine.AddFirst(1);
            chaine.AddFirst(42);
            chaine.AddFirst(12);
            chaine.DisplayChaine();

            Assert.AreEqual(chaine.NbElement, 4);
        }

        [TestCase(new int[]{ 3, 1, 42, 12 }, new int[]{39, -1}, 4)]     // remove void
        [TestCase(new int[]{ 3, 1, 42, 12 }, new int[]{36, 3 }, 3)]     // remove en tete
        [TestCase(new int[]{ 37, 3, 1, 12 }, new int[]{ 0, 3 }, 3)]     // remove milieu
        [TestCase(new int[]{ 5, 10, 12, 4 }, new int[]{-1, 12}, 3)]     // remove avant dernier
        [TestCase(new int[]{ 3, 1, 42, 12 }, new int[]{-1, 12}, 3)]     // remove fin
        public void Testremove(int[] add, int[] remove, int nbElem)
        {
            GenericSimplementChainee<int> chaine = new GenericSimplementChainee<int>();

            chaine.SupprimerMaillon(36);
            chaine.AddLast( add[0] );
            chaine.SupprimerMaillon( remove[0] );
            chaine.AddLast( add[1] );
            chaine.AddLast( add[2] );
            chaine.AddLast( add[3] );
            chaine.SupprimerMaillon( remove[1] );

            Assert.AreEqual(chaine.NbElement, nbElem);
        }

        [Test]
        public void TestInverserListe()
        {
            GenericSimplementChainee<int> chaine = new GenericSimplementChainee<int>();

            chaine.AddLast(2);
            chaine.Inverser();
            chaine.DisplayChaine();

            chaine.AddLast(1);
            chaine.Inverser();
            chaine.DisplayChaine();

            chaine.AddLast(3);
            chaine.AddLast(4);
            chaine.Inverser();
            chaine.DisplayChaine();

            Assert.AreEqual(4, chaine.NbElement);
        }
    }
}
