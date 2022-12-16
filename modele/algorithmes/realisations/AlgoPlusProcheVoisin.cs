using System.Collections.Generic;
using System.Diagnostics;
using VoyageurDeCommerce.modele.distances;
using VoyageurDeCommerce.modele.lieux;

namespace VoyageurDeCommerce.modele.algorithmes.realisations
{
    class AlgoPlusProcheVoisin : Algorithme
    {
        public override string Nom => "Plus proche voisin";

        /// <summary>
        /// Methode qui identifie l'usine dans la liste de lieux
        /// </summary>
        /// <param name="liste">la liste des lieux</param>
        /// <returns></returns>
        public Lieu TrouverUsine(List<Lieu> liste)
        {
            Lieu usine = null;
            // je parcour ma liste de lieu
            foreach (Lieu lieu in liste)
            {
                // si le lieu est de tyoe usine alors je le retient
                if (lieu.Type == TypeLieu.USINE) { usine = lieu; }
            }
            return usine;
        }
        /// <summary>
        /// algo plus proche voisin : pour chaque mouvement on recherche a savoir quel est le voisin le plus proche puis en s'y rend
        /// </summary>
        /// <param name="listeLieux">la liste de lieux</param>
        /// <param name="listeRoute">route qui relie les lieux</param>
        public override void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            FloydWarshall.calculerDistances(listeLieux, listeRoute);

            //Je copie ma liste de lieux dans une liste nonvisiter
            List<Lieu> nonVisiter = new List<Lieu>(listeLieux);
            //Je recherche l'usine
            Lieu lieuActuel = TrouverUsine(listeLieux);
            //je met l'usine dans la trouner puis je l'enleve de la liste non visiter
            this.Tournee.Add(lieuActuel);
            nonVisiter.Remove(lieuActuel);
            sw.Stop();
            this.NotifyPropertyChanged("Tournee");
            sw.Start();

            for (int i = 0; i < listeLieux.Count - 1; i++)
            {
                //distance minimal entre le point de départ et le voisin
                int distanceMin = int.MaxValue;
                //Stock le voisin le plus proche
                Lieu voisinPlusProche = null;
                //je parcours m'a liste non visiter
                foreach (Lieu lieu in nonVisiter)
                {
                    //je calcule popur chaque lieu dans la liste non visiter la distance avec le point de départ
                    int distance = FloydWarshall.Distance(lieuActuel, lieu);
                    //si la distance est inférieur a la distanceMin du point le plus proche le lieu devient le lieu le plus proche et on retient la distance
                    if (distance < distanceMin)
                    {
                        voisinPlusProche = lieu;
                        distanceMin = distance;
                    }
                }
                //on ajoute le lieu le plus proche a la tournee et on l'enlève a la liste non visiter
                this.Tournee.Add(voisinPlusProche);
                lieuActuel = voisinPlusProche;
                nonVisiter.Remove(lieuActuel);
                sw.Stop();
                this.NotifyPropertyChanged("Tournee");
                sw.Start();
            }
            sw.Stop();
            this.TempsExecution = sw.ElapsedMilliseconds;

        }
    }
}
