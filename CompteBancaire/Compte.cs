using System;

namespace CompteBancaire
{
    public class Compte
    {
        private string accountName;
        private double[] operations;
        private int indexNewOperation;
        private int nbOperationPassees;

        private double accountSolde;
        public double Solde
        {
            get { return accountSolde; }
        }

        /*****************
         * Constructeurs *
         *****************/

        public Compte() : this("Compte par defaut")
        {
        }
        public Compte(string name)
        {
            this.accountName = name;
            this.accountSolde = 0.0;
            this.indexNewOperation = 0;
            this.nbOperationPassees = 0;
            this.operations = new double[20];
            for (int i = 0; i < 20; ++i) this.operations[i] = 0.0;
        }


        /************************
         *   Méthodes publiques *
         ************************/

        public double AddOperation(double somme)
        {
            this.operations[indexNewOperation] = somme;
            indexNewOperation = (indexNewOperation + 1) % 20;
            this.nbOperationPassees++;
            this.accountSolde += somme;
            return this.Solde;
        }

        public string DisplayOperations()
        {
            string outString = string.Empty;
            int j = 0;
            if (this.nbOperationPassees >= 20)
            {
                for (int i = this.indexNewOperation; i < 20; ++i)
                    outString += "Operation " + (++j) + " : " + this.operations[i] + "\n";
            }
            for (int i = 0; i < this.indexNewOperation; ++i)
            {
                outString += "Operation " + ++j + " : " + this.operations[i] + "\n";
            }
            return outString;
        }







    }
}
