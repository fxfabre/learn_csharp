using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GenericChainee
{
    public class GenericSimplementChainee<T> : IEnumerable<GenericSimplementChainee<T>.Maillon<T>>
    {
        #region fields

        private Maillon<T> first;
        private Maillon<T> last;
        private int nbElementListe;

        public int NbElement
        {
            get { return nbElementListe; }
        }

        #endregion

        #region Constructeur

        public GenericSimplementChainee()
        {
            first = null;
            last = null;
        }

        #endregion

        #region Ajout en tete

        /**
         * Ajout d'un élement au début de la liste chainée
         */

        public void AddFirst(T n)
        {
            AddFirst(new Maillon<T>(n));
        }

        public void AddFirst(Maillon<T> maillon)
        {
            if (first == null)
            {
                first = maillon;
                last = maillon;
            }
            else
            {
                // Add node at the beginning of the list
                maillon.Next = first;
                first.Previous = maillon;
                first = maillon;
            }

            nbElementListe += 1;
        }

        #endregion

        #region Ajout a la fin

        /**
         * Ajout d'un élement à la fin de la liste
         */

        public void AddLast(T n)
        {
            var maillon = new Maillon<T>(n);

            if (first == null)
            {
                AddFirst(maillon);
            }
            else
            {
                last.Next = maillon;
                maillon.Previous = last;
                last = maillon;
                nbElementListe += 1;
            }
        }

        #endregion

        #region Suppression d'un élément

        /**
         * Suppression d'un élement ayant la meme value
         */

        public bool SupprimerMaillon(T n)
        {
            return SupprimerMaillon(new Maillon<T>(n));
        }

        private bool SupprimerMaillon(Maillon<T> maillon)
        {
            bool maillonDeleted = false;

            IEnumerable<Maillon<T>> liste = this.Where(e => e.Equals(maillon));
            foreach (var currentMaillon in liste)
            {
                if (currentMaillon.Previous == null)
                {
                    first = currentMaillon.Next;
                    if (currentMaillon.Next == null)
                    {
                        last = null;
                    }
                }
                else if (currentMaillon.Next == null)
                {
                    last = currentMaillon.Previous;
                    currentMaillon.Previous = null;
                }
                else
                {
                    currentMaillon.Previous.Next = currentMaillon.Next;
                    currentMaillon.Next.Previous = currentMaillon.Previous;
                }

                nbElementListe -= 1;
                maillonDeleted = true;
            }
            return maillonDeleted;
        }

        #endregion

        #region

        /*
         * Affichage du contenu des maillons, un par ligne
         */
        public void DisplayChaine()
        {
            foreach (var node in this)
            {
                Console.WriteLine(node.ToString());
            }
        }

        /*
         * réinitialise la liste
         */

        public void Reset()
        {
            // question : est ce que mettre First et Last à null permet au GB de nettoyer la liste ?
            first = null;
            last = null;
            nbElementListe = 0;
        }

        /*
         * Inversion de l'ordre des éléments de la liste
         */

        public void Inverser()
        {
            Maillon<T> currentMaillon = this.first;
            this.Reset();

            while (currentMaillon != null)
            {
                Maillon<T> nextMaillon = currentMaillon.Next;
                currentMaillon.Next = null;

                this.AddFirst(currentMaillon);
                currentMaillon = nextMaillon;
            }
        }

        #endregion

        #region Inhedited methods from IEnumerable

        public IEnumerator<Maillon<T>> GetEnumerator()
        {
            Maillon<T> currentMaillon = first;
            while (currentMaillon != null)
            {
                yield return currentMaillon;
                currentMaillon = currentMaillon.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public class Maillon<Tm> : IEquatable<Maillon<Tm>>
        {
            #region Fields & Attributes

            private readonly Tm value;
            public Maillon<Tm> Next { get; set; }
            public Maillon<Tm> Previous { get; set; }

            #endregion

            #region Constructeurs

            public Maillon()
                : this(default(Tm))
            {
            }

            public Maillon(Tm n)
            {
                value = n;
                Previous = null;
                Next = null;
            }

            #endregion

            #region Equals

            /*
             * Comparaison sur l'ID du maillon
             */

            public bool Equals(Maillon<Tm> other)
            {
                return Equals(value, other.value);
            }

            public bool Equals(Tm n)
            {
                return Equals(value, n);
            }

            #endregion

            #region Display

            /*
             * Affichage du Nombre de la classe + celui de la classe suivante
             */

            public override string ToString()
            {
                if (Next == null) return "(" + value + ", null) ";
                return string.Format("({0}, {1}) ", value, Next.value);
            }

            #endregion
        }
    }
}