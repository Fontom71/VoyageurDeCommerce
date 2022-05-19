using System.Collections.Generic;
using System.Diagnostics;
using VoyageurDeCommerce.modele.distances;
using VoyageurDeCommerce.modele.lieux;

namespace VoyageurDeCommerce.modele.algorithmes.realisations
{
    class AlgoInsertionLoin : Algorithme
    {
        public override string Nom => "Insertion loin";

        private int Distance(Lieu detour, Lieu depart, Lieu arrivee) => FloydWarshall.Distance(depart, detour) + FloydWarshall.Distance(detour, arrivee) - FloydWarshall.Distance(depart, arrivee);

        public override void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            FloydWarshall.calculerDistances(listeLieux, listeRoute);
            List<Lieu> NonVisiter = new List<Lieu>(listeLieux);
            Lieu depart = null;
            Lieu lePlusLoin = null;
            int maximum = 0;
            foreach (Lieu lieu1 in listeLieux)
            {
                foreach (Lieu lieu2 in listeLieux)
                {
                    int distance = FloydWarshall.Distance(lieu1, lieu2);
                    if (distance > maximum)
                    {
                        maximum = distance;
                        depart = lieu1;
                        lePlusLoin = lieu2;
                    }

                }
            }
            Tournee.Add(depart);
            Tournee.Add(lePlusLoin);
            NonVisiter.Remove(depart);
            NonVisiter.Remove(lePlusLoin);
            NotifyPropertyChanged("Tournee");

            while (NonVisiter.Count > 0)
            {
                Lieu lieuProche = null;
                int indexLieuPrecedent = 0;
                int distanceMinimale = int.MaxValue;
                foreach (Lieu lieu in NonVisiter)
                {
                    int index = -1;
                    int distanceMax = int.MaxValue;


                    for (int i = 0; i < Tournee.ListeLieux.Count; i++)
                    {
                        Lieu avant = Tournee.ListeLieux[i];
                        Lieu apres = Tournee.ListeLieux[(i + 1) % Tournee.ListeLieux.Count];
                        int distance = Distance(lieu, avant, apres);
                        if (index == -1 || distance < distanceMax)
                        {
                            distanceMax = distance;
                            index = i;
                        }
                    }
                    if (lieuProche == null || distanceMax > distanceMinimale)
                    {
                        lieuProche = lieu;
                        indexLieuPrecedent = index;
                        distanceMinimale = distanceMax;
                    }

                }
                Tournee.ListeLieux.Insert(indexLieuPrecedent + 1, lieuProche);
                NonVisiter.Remove(lieuProche);

                NotifyPropertyChanged("Tournee");
            }
        }
    }
}