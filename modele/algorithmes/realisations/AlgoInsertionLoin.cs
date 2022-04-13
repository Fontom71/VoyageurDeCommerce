using System.Collections.Generic;
using System.Diagnostics;
using VoyageurDeCommerce.modele.distances;
using VoyageurDeCommerce.modele.lieux;

namespace VoyageurDeCommerce.modele.algorithmes.realisations
{
    class AlgoInsertionLoin : Algorithme
    {
        public override string Nom => "Insertion loin";

        public override void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            FloydWarshall.calculerDistances(listeLieux, listeRoute);

            List<Lieu> nonVisiter = listeLieux;
            List<Lieu> visiter = new List<Lieu>();
            int valeur = 0;
            Lieu lePlusLoin = null;
            Lieu usine = null;
            // cherche l'usine dans la liste des lieux et l'enregistre dans usine
            foreach (Lieu lieu in listeLieux)
            {
                if (lieu.Type == TypeLieu.USINE) { usine = lieu; };
            }
            // cherche le point le plus éloingné 
            foreach (Lieu lieu in listeLieux)
            {
                int distance = FloydWarshall.Distance(usine, lieu);
                if (distance > valeur)
                {
                    lePlusLoin = lieu;
                    valeur = distance;
                }
            }
            // Donne la route vers le chemin le plus éloingner
            foreach (Route route in FloydWarshall.Chemin(usine, lePlusLoin))
            {
                nonVisiter.Remove(route.Depart);
                nonVisiter.Remove(route.Arrivee);

                visiter.Add(route.Depart);
                visiter.Add(route.Arrivee);


            }
            // chercher des détourts les plus faible
            foreach (Lieu lieuNV in nonVisiter)
            {
                int r = 0;
                int valeurMIN = 0;
                Lieu lieu1 = null;
                Lieu lieu2 = null;

                for (int i = 0; i < visiter.Count; i++)
                {
                    for (int j = 0; j < visiter.Count; j++)
                    {
                        if (j != i)
                        {
                            r -= FloydWarshall.Distance(lieuNV, visiter[i]);
                            r -= FloydWarshall.Distance(lieuNV, visiter[j]);
                            r += FloydWarshall.Distance(visiter[i], visiter[j]);
                            if (r < valeurMIN)
                            {
                                valeurMIN = r;
                                lieu1 = visiter[i];
                                lieu2 = visiter[j];
                            }
                        }
                    }
                    foreach (Lieu lieu in visiter)
                    {
                        if (lieu1 == lieu)
                        {
                            this.Tournee.Add(lieuNV);
                            sw.Stop();
                            this.NotifyPropertyChanged("Tournee");
                            sw.Start();
                        }
                        this.Tournee.Add(lieu);
                        sw.Stop();
                        this.NotifyPropertyChanged("Tournee");
                        sw.Start();
                    }
                }
            }
            sw.Stop();
            this.TempsExecution = sw.ElapsedMilliseconds;
        }
    }
}
