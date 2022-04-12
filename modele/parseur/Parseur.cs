using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using VoyageurDeCommerce.modele.lieux;

namespace VoyageurDeCommerce.modele.parseur
{
    /// <summary>Parseur de fichier de graphe</summary>
    public class Parseur
    {
        /// <summary>Propriétés nécessaires</summary>
        private Dictionary<string, Lieu> listeLieux;
        public Dictionary<string, Lieu> ListeLieux => listeLieux;
        private List<Route> listeRoutes;
        public List<Route> ListeRoutes => listeRoutes;

        private string adresseFichier;

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="nomDuFichier">Nom du fichier à parser</param>
        public Parseur(String nomDuFichier)
        {
            this.listeLieux = new Dictionary<string, Lieu>();
            this.listeRoutes = new List<Route>();

            this.adresseFichier = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\ressources\" +nomDuFichier;

        }

        /// <summary>
        /// Parsage du fichier
        /// </summary>
        public void Parser()
        {
            //Ouverture du fichier
            using (StreamReader stream = new StreamReader(this.adresseFichier))
            {
                string ligne;
                while ((ligne = stream.ReadLine()) != null)
                {
                    //Lecture d'une ligne
                    string[] morceaux = ligne.Split();
                    if(morceaux[0] == "ROUTE")
                    {
                        var mr = MonteurRoute.Creer(morceaux, ListeLieux);
                        listeRoutes.Add(mr);
                    } else
                    {
                        var ml = MonteurLieu.Creer(morceaux);
                        listeLieux.Add(ml.Nom, ml);
                    }
                }
            }

            /*var l1 = new Lieu(TypeLieu.USINE, "1", 0, 0);
            var l2 = new Lieu(TypeLieu.MAGASIN, "2", 2, 0);
            var l3 = new Lieu(TypeLieu.MAGASIN, "3", -2, 2);
            var l4 = new Lieu(TypeLieu.MAGASIN, "4", 4, 2);
            var l5 = new Lieu(TypeLieu.MAGASIN, "5", 1, 4);
            ListeLieux[l1.Nom] = l1;
            ListeLieux.Add(l2.Nom, l2);
            ListeLieux.Add(l3.Nom, l3);
            ListeLieux.Add(l4.Nom, l4);
            ListeLieux.Add(l5.Nom, l5);

            ListeRoutes.Add(new Route(l1, l2, 2));
            ListeRoutes.Add(new Route(l1, l3, 3));
            ListeRoutes.Add(new Route(l1, l5, 6));
            ListeRoutes.Add(new Route(l2, l4, 1));
            ListeRoutes.Add(new Route(l2, l5, 3));
            ListeRoutes.Add(new Route(l3, l5, 4));
            ListeRoutes.Add(new Route(l4, l5, 1));*/
        }
    }
}
