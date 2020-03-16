using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace CompteBancaire
{
    public static class MenuCompte
    {
        private static readonly Dictionary<int, Func<AccountUser, bool>> userMenuActions;

        private static readonly Dictionary<int, Func<List<AccountUser>, bool>> mainMenuActions;

        static MenuCompte()
        {
            userMenuActions = new Dictionary<int, Func<AccountUser, bool>>();
            userMenuActions.Add(1, UserActionCreateAccount);
            userMenuActions.Add(2, UserActionDebiterAccount);
            userMenuActions.Add(3, UserActionCrediterAccount);
            userMenuActions.Add(4, UserActionDisplayAccount);
            userMenuActions.Add(5, _ => false);

            mainMenuActions = new Dictionary<int, Func<List<AccountUser>, bool>>();
            mainMenuActions.Add(1, CreerUser);
            mainMenuActions.Add(2, MainMenuDisplayUsers);
            mainMenuActions.Add(3, MainMenuDisplayUser);
            mainMenuActions.Add(4, MainMenuDeleteUser);
            mainMenuActions.Add(5, MainMenuManageUser);
            mainMenuActions.Add(9, _ => false);
        }

        public static string DisplayMenu()
        {
            return "*** Menu de selection des opérations *** \n"
                   + "1. Creer utilisateur \n"
                   + "2. Afficher les utilisateurs existants \n"
                   + "3. Afficher un utilisateur \n"
                   + "4. Supprimer user \n"
                   + "5. Manage user account \n"
                   + "9. Exit \n"
                   + "****************************************\n";
        }

        public static string DisplayUserMenu()
        {
            return "*** Menu de selection des opérations *** \n"
                   + "1. Creer compte    \n"
                   + "2. Débiter compte  \n"
                   + "3. Crediter compte \n"
                   + "4. Afficher solde  \n"
                   + "5. Level up ^^ \n"
                   + "****************************************\n";
        }

        public static int ReadNumber()
        {
            return ReadNumber(-1);
        }

        public static int ReadNumber(int defaultvalue)
        {
            int userResponseInt;
            string userResponseString = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userResponseString))
            {
                userResponseInt = defaultvalue;
            }
            else
            {
                try
                {
                    userResponseInt = int.Parse(userResponseString, CultureInfo.CurrentCulture);
                }
                catch (Exception)
                {
                    Console.WriteLine("Merci d'entrer un nombre entier correct");
                    userResponseInt = defaultvalue;
                }
            }

            return userResponseInt;
        }

        public static string ReadString(string message)
        {
            Console.Write(message);
            string content = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(content))
            {
                return string.Empty;
            }
            return content.Trim();
        }

        public static string DisplayUsers(IList listUsers)
        {
            string outString = "*** Liste des utilisateurs : ***\n";
            for (int i = 0; i < listUsers.Count; ++i)
            {
                outString += string.Format("Client {0} :\n", i);
                outString += listUsers[i].ToString();
            }
            outString += "********************************\n";
            return outString;
        }

        public static bool CreerUser(IList listUsers)
        {
            string nom = string.Empty;
            string prenom = string.Empty;

            while (string.IsNullOrWhiteSpace(nom))
            {
                nom = ReadString("Prenom de l'utilisateur ?   ");
            }
            while (string.IsNullOrWhiteSpace(prenom))
            {
                prenom = ReadString("Nom de l'utilisateur ?      ");
            }
            string adresse = ReadString("Adresse de l'utilisateur ?  ");

            return CreerUser(listUsers, nom, prenom, adresse);
        }

        public static bool CreerUser(IList listUsers,
                                     string nom, string prenom, string adresse)
        {
            if (string.IsNullOrWhiteSpace(nom) || string.IsNullOrWhiteSpace(prenom))
            {
                Console.WriteLine("Invalid strings : ");
                Console.WriteLine("    Nom    : " + nom);
                Console.WriteLine("    Prenom : " + prenom);
            }
            else
            {
                listUsers.Add(new AccountUser(prenom.Trim(), nom.Trim(), adresse.Trim()));
                Console.WriteLine("Nouvel utilisateur ajouté à l'incide " + listUsers.Count + "\n");
            }
            return true;
        }

        public static void SupprimerUser(IList<AccountUser> listUsers, int indexUser)
        {
            Console.WriteLine("Suppression de l'utilisateur ");
            Console.WriteLine(listUsers.ElementAt(indexUser).ToString());
            listUsers.RemoveAt(indexUser);
        }

        public static int ChoixUser(IList listUsers)
        {
            int numeroUser = 0;
            bool stayInLoop = true;
            while (stayInLoop)
            {
                stayInLoop = false;
                Console.Write("Numero de l'utilisateur : ");
                string userChoice = Console.ReadLine() ?? string.Empty;

                try
                {
                    numeroUser = int.Parse(userChoice, CultureInfo.CurrentCulture);
                }
                catch (Exception)
                {
                    Console.WriteLine("Merci d'entrer un nombre entier ou -1 pour sortir");
                    stayInLoop = true;
                }
            }

            if ((numeroUser < 0) || (numeroUser >= listUsers.Count))
            {
                Console.WriteLine("Index out of bounds");
                numeroUser = -1;
            }
            return numeroUser;
        }

        public static void ManageUserAccount(AccountUser currentUser)
        {
            bool stayInLoop = true;
            while (stayInLoop)
            {
                Console.WriteLine(DisplayUserMenu());
                int userResponseInt = ReadNumber();

                if (userMenuActions.ContainsKey(userResponseInt))
                {
                    stayInLoop = userMenuActions[userResponseInt](currentUser);
                }
                else
                {
                    Console.WriteLine("Unknown option");
                }
            }
        }

        private static bool UserActionCreateAccount(AccountUser currentUser)
        {
            Console.Write("Nom du compte ? ");
            string accountName = Console.ReadLine();
            currentUser.CreateAccount(accountName);
            return true;
        }

        private static bool UserActionDebiterAccount(AccountUser currentUser)
        {
            Console.WriteLine("Montant à débiter du compte ?");
            int montant = ReadNumber(0);
            currentUser.DebiterCompte(montant);
            return true;
        }

        private static bool UserActionCrediterAccount(AccountUser currentUser)
        {
            Console.WriteLine("Montant à créditer sur le compte ?");
            int montant = ReadNumber(0);
            currentUser.CrediterCompte(montant);
            return true;
        }

        private static bool UserActionDisplayAccount(AccountUser currentUser)
        {
            Console.WriteLine("Solde du compte : " + currentUser.SoldeCompte());
            return true;
        }

        public static void MainMenu()
        {
            bool stayInLoop = true;
            var listUsers = new List<AccountUser>();

            while (stayInLoop)
            {
                Console.WriteLine(DisplayMenu());

                int userResponseInt = ReadNumber();

                if (mainMenuActions.ContainsKey(userResponseInt))
                {
                    stayInLoop = mainMenuActions[userResponseInt](listUsers);
                }
                else
                {
                    Console.WriteLine("Option non valide");
                }
            }
        }

        private static bool MainMenuDisplayUsers(List<AccountUser> listUsers)
        {
            Console.WriteLine(DisplayUsers(listUsers));
            return true;
        }

        private static bool MainMenuDisplayUser(List<AccountUser> listUsers)
        {
            int indexUser = ChoixUser(listUsers);
            if (indexUser >= 0)
            {
                Console.WriteLine("Utilisateur choisi : \n");
                Console.WriteLine(listUsers.ElementAt(indexUser).ToString());
            }
            return true;
        }

        private static bool MainMenuDeleteUser(List<AccountUser> listUsers)
        {
            int indexUser = ChoixUser(listUsers);
            if (indexUser >= 0)
            {
                SupprimerUser(listUsers, indexUser);
            }
            return true;
        }

        private static bool MainMenuManageUser(List<AccountUser> listUsers)
        {
            int indexUser = ChoixUser(listUsers);
            if (indexUser >= 0)
            {
                ManageUserAccount(listUsers.ElementAt(indexUser));
            }
            return true;
        }
    }
}