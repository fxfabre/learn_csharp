using System;
using System.Collections.Generic;
using System.Linq;
using NLog;

namespace TestLinq
{
    public class RunRequest
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();


        public enum RequestId
        {
            Request2,
            Request3,
            Request4,
            Request5,
            Request6
        }

        private static readonly Dictionary<RequestId, Func<TutosEntities, IEnumerable<object>>> listActions =
            new Dictionary<RequestId, Func<TutosEntities, IEnumerable<object>>>
            {
                {   RequestId.Request2, Requete2    },
                {   RequestId.Request3, Requete3    },
                {   RequestId.Request4, Requete4    },
                {   RequestId.Request5, Requete5    },
                {   RequestId.Request6, Requete6    }
            };


        public IEnumerable<object> Run(RequestId request)
        {
            logger.Debug("Creating object TutosEntities");
            var db = new TutosEntities();

            IEnumerable<object> result = new List<object>();

            if (listActions.ContainsKey(request))
            {
                logger.Debug("Dictionnary contains " + request);
                try
                {
                    logger.Info("trying to run request " + request);
                    result = listActions[request](db);

                    foreach (var item in result)
                    {
                        logger.Trace(item.ToString());
                    }
                }
                catch (Exception e)
                {
                    logger.Error("Exception : " + e.Message);
                }
                finally
                {
                    logger.Info("Release ressource " + db.GetType());
                    db.Dispose();
                }
            }
            else
            {
                logger.Error("Action not defined : " + request);
            }

            return result;
        }


        // combien de tuples contiennent chaque table
        private static IEnumerable<object> Requete2(TutosEntities db)
        {
            logger.Debug("Running request 2");

            var returnResult = new List<object>
            {
                new
                {
                    Table = "Ventes",
                    NombreElements = db.Ventes.Count()
                },
                new
                {
                    Table = "Couleurs",
                    NombreElements = db.Couleurs.Count()
                },
                new
                {
                    Table = "Marques",
                    NombreElements = db.Marques.Count()
                }
            };

            return returnResult;
        }

        // Afficher les ventes depuis les 90 derniers jours
        private static IEnumerable<object> Requete3(TutosEntities db)
        {
            logger.Debug("Running request 3");

            DateTime date = DateTime.Now.AddDays(-90);
            logger.Debug("Current date - 90 days = " + date);

            var data =
                from vente in db.Ventes
                join couleur in db.Couleurs on vente.couleurid equals couleur.Id
                join marque in db.Marques on vente.marqueid equals marque.Id
                where (date < vente.date)
                select vente;
            var result = data.ToList();

            logger.Debug("Found " + result.Count() + " elements");
            logger.Debug("Expected : 57.671 results");

            return result;
        }

        // Quels sont les 10 marques les plus vendues depuis l’ouverture du magasin
        private static IEnumerable<object> Requete4(TutosEntities db)
        {
            logger.Debug("Running request 4");

            var requestVentesParMarque =
                from vente in db.Ventes
                join marque in db.Marques on vente.marqueid equals marque.Id
                group marque by marque.Name
                into name
                orderby name.Count() descending
                select new { Marque = name.Key, nb = name.Count() };
            var topVentes = requestVentesParMarque.ToList().Take(10);

            logger.Trace("nb Ventes | Marques");
            foreach (var val in topVentes)
            {
                logger.Trace("{0,7}   | {1}", val.nb, val.Marque);
            }

            logger.Trace("\n");
            logger.Trace("Expected :             \n");
            logger.Trace("7200  Gram             \n");
            logger.Trace("6752  Ca Shott         \n");
            logger.Trace("6752  Jo Ghost         \n");
            logger.Trace("6752  Lollipops        \n");
            logger.Trace("5824  Diadora          \n");
            logger.Trace("5440  Bundgaard        \n");
            logger.Trace("4992  Little Mary      \n");
            logger.Trace("4976  Chie Mihara      \n");
            logger.Trace("4976  Joyks            \n");
            logger.Trace("4576  Lou Villon Paris \n");

            return topVentes;
        }

        // Quelle est la couleur la plus vendue chaque année ?
        private static IEnumerable<object> Requete5(TutosEntities db)
        {
            logger.Debug("Running request 5");

            var queryColorByYear =
                from vente in db.Ventes
                join couleur in db.Couleurs on vente.couleurid equals couleur.Id
                group vente by new { couleur.name, vente.date.Year }
                into res
                select new { Year = res.Key, SommeVentes = res.Sum(e => e.valeur) };
            var resultColorByYear = queryColorByYear.ToList();

            var queryMaxColorByYear =
                from data in resultColorByYear
                group data by data.Year.Year
                into subData
                select subData.OrderByDescending(e => e.SommeVentes).First();
            var resultMaxColorByYear = queryMaxColorByYear.ToList();
            var result = resultMaxColorByYear.OrderBy(e => e.Year.Year);

            foreach (var val in result)
            {
                logger.Trace("{0}    {1,-15} {2,-8}", val.Year.Year, val.Year.name, val.SommeVentes);
            }
            logger.Trace("Found " + result.Count() + " elements\n\n");
            
            logger.Trace("Expected :");
            logger.Trace("2010 OU Crimson Red  1727627");
            logger.Trace("2011 Aero blue       2709960");
            logger.Trace("2012 Orange-red      2399843");
            logger.Trace("2013 Nyanza          2652519");
            logger.Trace("2014 Gray-blue       493371");
            
            return result;
        }

        // Toutes les marques dont la somme des ventes
        // sur les 90 derniers jours a rapporté plus de 500K €
        private static IEnumerable<object> Requete6(TutosEntities db)
        {
            logger.Debug("Running request 6");

            var query =
                from vente in db.Ventes
                join marque in db.Marques on vente.marqueid equals marque.Id
                group vente by marque.Name
                into a
                where a.Sum(e => e.valeur) > 500000
                orderby a.Sum(e => e.valeur)
                select a;
            var result = query.ToList();

            foreach (var val in result)
            {
                logger.Trace(val.ToString());
            }

            logger.Trace("Found " + result.Count() + " elements");
            logger.Trace("Expected : 476 results");

            return result;
        }
    }
}