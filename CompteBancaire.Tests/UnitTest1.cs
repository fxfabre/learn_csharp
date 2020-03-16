using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace CompteBancaire.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        [TestCase(500, -500)]
        [TestCase(-500, 0)]
        public void TestDebiterCompte(int somme, int result)
        {
            AccountUser myUser = new AccountUser("FX", "Fabre", "chez moi");
            myUser.CreateAccount("compte 1");
            myUser.DebiterCompte(0);
            myUser.DebiterCompte(somme);

            Assert.AreEqual(result, myUser.SoldeCompte());
        }

        [TestCase(500, 500)]
        [TestCase(-500, 0)]
        public void TestCrediterCompte(int somme, int result)
        {
            AccountUser myUser = new AccountUser("FX", "Fabre", "chez moi");
            myUser.CreateAccount("compte 1");
            myUser.CrediterCompte(somme);

            Assert.AreEqual(result, myUser.SoldeCompte());
        }
        
        [Test]
        public void TestDisplayOperations()
        {
            AccountUser myUser = new AccountUser("FX", "Fabre", "chez moi");
            myUser.CreateAccount("compte 1");

            for (int i = 1; i <= 30; ++i)
            {
                myUser.DebiterCompte(i);
            }
            
            Console.WriteLine(myUser.DisplayOperations());
            Assert.AreEqual(-15 * 31, myUser.SoldeCompte());
        }

        [Test]
        public void TestMenu()
        {
            var myList = new List<AccountUser>();

            MenuCompte.DisplayMenu();
            MenuCompte.DisplayUserMenu();
            MenuCompte.ReadNumber();

            MenuCompte.CreerUser(myList, "nom1", "prenom1", "adresse1");
            MenuCompte.CreerUser(myList, string.Empty, string.Empty, "adresse");
            MenuCompte.CreerUser(myList, "nom2", "prenom2", "adresse2");
            MenuCompte.DisplayUsers( myList );
            MenuCompte.SupprimerUser( myList, 0);

            Program.Main();
        }

        [Test]
        public void TestUi()
        {
            // Set User Input
            string[] lines =
            {
                "1", "Prenom1", "Nom1", "Adresse1",     // Create user 0
                "1", "Prenom2", "Nom2", "Adresse2",     // Create user 1
                "2",                                    // Display users
                "3", "-1", "3", "12", "3", "1",         // Display user 1
                "4", "-1", "4", "12", "4", "1",         // Delete user 1
                "5", "0", "4",                          // Manage user 0 and display account
                "0", "Compte1",                         // Create compte Compte0
                "2", "10", "3", "50",
                "2", "-20", "3", "-520",
                "5", "9"
            };
            StringReader input = new StringReader(String.Join(Environment.NewLine, lines));
            Console.SetIn(input);

            // Launch the menu
            MenuCompte.MainMenu();

        }

    }
}
