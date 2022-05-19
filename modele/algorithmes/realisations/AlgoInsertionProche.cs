using System.Collections.Generic;
using System.Diagnostics;
using VoyageurDeCommerce.modele.distances;
using VoyageurDeCommerce.modele.lieux;

namespace VoyageurDeCommerce.modele.algorithmes.realisations
{
    class AlgoInsertionProche : Algorithme
    {
        public override string Nom => "Insertion proche";

        private int Distance(Lieu detour, Lieu depart, Lieu arrivee) => FloydWarshall.Distance(depart, detour) + FloydWarshall.Distance(detour, arrivee) - FloydWarshall.Distance(depart, arrivee);


        public override void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            List<Lieu> listeFinal = new List<Lieu>();
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
            listeFinal.Add(depart);
            listeFinal.Add(lePlusLoin);
            NonVisiter.Remove(depart);
            NonVisiter.Remove(lePlusLoin);


            while (NonVisiter.Count > 0)
            {
                Lieu lieuProche = null;
                int lieuPrecedentPosition = 0;
                int distanceMini = int.MaxValue;
                foreach (Lieu lieu in NonVisiter)
                {
                    int index = -1;
                    int max = int.MaxValue;


                    for (int i = 0; i < listeFinal.Count; i++)
                    {
                        Lieu avant = listeFinal[i];
                        Lieu apres = listeFinal[(i + 1) % listeFinal.Count];
                        int distance = Distance(lieu, avant, apres);
                        if (index == -1 || distance < max)
                        {
                            max = distance;
                            index = i;
                        }
                    }
                    if (lieuProche == null || max < distanceMini)
                    {
                        lieuProche = lieu;
                        lieuPrecedentPosition = index;
                        distanceMini = max;
                    }

                }


                listeFinal.Insert(lieuPrecedentPosition + 1, lieuProche);
                NonVisiter.Remove(lieuProche);


            }
            List<Lieu> tempo = new List<Lieu>();
            bool usineTrouver = false;
            foreach (Lieu lieu in listeFinal)
            {

                if (lieu.Type == TypeLieu.USINE)
                {
                    this.Tournee.Add(lieu);
                    usineTrouver = true;
                    this.NotifyPropertyChanged("Tournee");
                }
                else if (usineTrouver == false)
                {
                    tempo.Add(lieu);
                }
                else
                {
                    this.Tournee.Add(lieu);
                    this.NotifyPropertyChanged("Tournee");
                }
            }
            foreach (Lieu lieu in tempo)
            {
                this.Tournee.Add(lieu);
                this.NotifyPropertyChanged("Tournee");
            }
        }
    }
}