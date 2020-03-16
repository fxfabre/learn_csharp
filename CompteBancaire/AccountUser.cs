using System;

namespace CompteBancaire
{
    public class AccountUser
    {
        private static int newId = 1;

        private int id;
        private readonly string firstName;
        private readonly string lastName;
        private string adress;
        private Compte account;


        /*****************
         * Constructeurs *
         *****************/
        public AccountUser(string prenom, string nom)
            : this(prenom, nom, string.Empty)
        {

        }

        public AccountUser(string prenom, string nom, string adresse)
        {
            this.id = newId++;
            this.firstName = prenom;
            this.lastName = nom;
            this.adress = adresse;
            this.account = null;
        }

        /******************
         *   Méthodes     *
         ******************/

        public double SoldeCompte()
        {
            double result = 0.0;
            if (this.account == null)
            {
                Console.WriteLine("Account not created yet !");
            }
            else
            {
                result = this.account.Solde;
            }
            return result;
        }

        public void ModifierAdresse(string adresse)
        {
            this.adress = adresse;
        }


        public void CreateAccount(string accountName)
        {
            if (this.account == null)
            {
                this.account = new Compte(accountName);
            }
            else
            {
                Console.WriteLine("Compte deja créé");
            }
        }
        public void CreateAccount()
        {
            this.CreateAccount("Compte de " + this.lastName);
        }


        public double DebiterCompte(double amount)
        {
            if (this.account == null) this.CreateAccount();
            if (this.CheckValuePositive(amount))
            {
                return this.account.Solde;
            }
            return account.AddOperation(-amount);
        }
        public double CrediterCompte(double amount)
        {
            if (this.account == null) this.CreateAccount();
            if (this.CheckValuePositive(amount))
            {
                return this.account.Solde;
            }
            return account.AddOperation(amount);
        }

        private bool CheckValuePositive(double n)
        {
            if (n < 0.0)
            {
                Console.WriteLine("Negative value");
                Console.WriteLine("Cancelling operation");
                return true;
            }
            return false;
        }

        /************************
         *   Méthodes affichage *
         ************************/

        public override string ToString()
        {
            string outString = string.Empty;
            outString += "    Nom     : " + this.lastName + "\n";
            outString += "    Prenom  : " + this.firstName + "\n";
            outString += "    Adresse : " + this.adress + "\n";
            return outString;
        }

        public string DisplayOperations()
        {
            return this.account.DisplayOperations();
        }

    }
}



