using System;

namespace ListeChainee
{
    public class ListeSimplementChainee
    {
        private Maillon tete;
        private int nbElementListe;
        public int NbElement
        {
            get { return nbElementListe; }
        }
        
        public ListeSimplementChainee()
        {
            this.tete = null;
        }
        
        /**
         * Ajout d'un élement au début de la liste chainée
         */
        public void AddEnTete()
        {
            this.AddEnTete( new Maillon() );
        }
        public void AddEnTete(int n)
        {
            this.AddEnTete(new Maillon(n));
        }
        private void AddEnTete(Maillon maillon)
        {
            maillon.NextMaillon = this.tete;
            this.tete = maillon;
            ++nbElementListe;
        }

        /**
         * Ajout d'un élement à la fin de la liste
         */
        public void Add()
        {
            this.Add( new Maillon() );
        }
        public void Add(int n)
        {
            this.Add(new Maillon(n));
        }
        private void Add(Maillon maillon)
        {
            if (this.tete == null)
            {
                this.tete = maillon;
                ++nbElementListe;
                return;
            }

            Maillon currentMaillon = tete;
            while (currentMaillon.NextMaillon != null)
            {
                currentMaillon = currentMaillon.NextMaillon;
            }
            currentMaillon.NextMaillon = maillon;
            ++nbElementListe;
        }

        /**
         * Suppression d'un élement ayant le meme ID + value
         */
        public bool SupprimerMaillon(int n)
        {
            return this.SupprimerMaillon(new Maillon(n));
        }
        private bool SupprimerMaillon(Maillon maillon)
        {
            if (this.tete == null) return false;

            if (this.tete.Equals(maillon))
            {
                this.tete = this.tete.NextMaillon;
                --nbElementListe;
                return true;
            }

            Maillon currentMaillon = this.tete;
            while (currentMaillon.NextMaillon != null)
            {
                if (currentMaillon.NextMaillon.Equals(maillon))
                {
                    currentMaillon.NextMaillon = currentMaillon.NextMaillon.NextMaillon;
                    --nbElementListe;
                    return true;
                }
                currentMaillon = currentMaillon.NextMaillon;
            }
            return false;
        }
    
        /*
         * Affichage du contenu des maillons, un par ligne
         */
        public void DisplayChaine()
        {
            Maillon currentMaillon = this.tete;

            while (currentMaillon != null)
            {
                Console.WriteLine(currentMaillon.ToString());

                currentMaillon = currentMaillon.NextMaillon;
            }
            Console.WriteLine();
        }

        /*
         * Calcul de la somme des value de chaque noeuds
         */
        public int GetSomme()
        {
            int total = 0;
            Maillon currentMaillon = this.tete;
            while (currentMaillon != null)
            {
                total += currentMaillon.Nombre;
                currentMaillon = currentMaillon.NextMaillon;
            }
            return total;
        }

        /*
         * réinitialise la liste
         */
        public void Reset()
        {
            this.tete = null;
            this.nbElementListe = 0;
        }

        /*
         * Inversion de l'ordre des éléments de la liste
         */
        public void Inverser()
        {
            Maillon currentMaillon = this.tete;
            this.Reset();
            while (currentMaillon != null)
            {
                Maillon nextMaillon = currentMaillon.NextMaillon;
                currentMaillon.NextMaillon = null;

                this.AddEnTete(currentMaillon);
                currentMaillon = nextMaillon;
            }
        }



        public class Maillon : IEquatable<Maillon>
        {
            /**************************
             * Attributs de la classe *
             **************************/
            private int inNombre;
            public int Nombre
            {
                get { return inNombre; }
            }

            public Maillon NextMaillon { get; set; }


            /**************************
             * Constructeurs          *
             **************************/
            public Maillon() : this(0, null) { }
            public Maillon(int n) : this(n, null) { }
            public Maillon(int n, Maillon suivant)
            {
                this.inNombre = n;
                if (suivant != null) suivant.NextMaillon = null;
                this.NextMaillon = suivant;
            }

            /*
             * Comparaison sur l'ID du maillon
             */
            public bool Equals(Maillon maillon)
            {
                return this.Nombre == maillon.Nombre;
            }

            /*
             * Affichage du Nombre de la classe + celui de la classe suivante
             */
            public override string ToString()
            {
                if (this.NextMaillon == null) return "(" + this.Nombre + ", null) ";
                return "(" + this.Nombre + ", " + this.NextMaillon.Nombre + ") ";
            }

        }

    }
}
