using System.Collections.Generic;
using VoyageurDeCommerce.modele.distances;
using VoyageurDeCommerce.modele.lieux;

namespace VoyageurDeCommerce.modele.algorithmes.realisations
{
    class AlgoInsertionProche : Algorithme
    {
        public override string Nom => "Insertion proche";

        /// <summary>
        /// calule la distance du détour
        /// </summary>
        /// <param name="detour">lieu voisin nonvisiter</param>
        /// <param name="depart">lieu de départ</param>
        /// <param name="arrivee">lieu d'arriver</param>
        /// <returns>cout du détour</returns>
        private int Distance(Lieu detour, Lieu depart, Lieu arrivee)
        {
            return FloydWarshall.Distance(depart, detour) + FloydWarshall.Distance(detour, arrivee) - FloydWarshall.Distance(depart, arrivee);
        }

        /// <summary>
        /// Algo insertion proche
        /// </summary>
        /// <param name="listeLieux">liste de sommet</param>
        /// <param name="listeRoute">liste de route</param>
        public override void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            //enregistre la tournee
            List<Lieu> listeFinal = new List<Lieu>();
            FloydWarshall.calculerDistances(listeLieux, listeRoute);
            //liste des lieux visiter
            List<Lieu> NonVisiter = new List<Lieu>(listeLieux);
            // Les deux lieu les plus eloigner
            Lieu depart = null;
            Lieu lePlusLoin = null;
            int maximum = 0;
            // Recherche les lieux les plus eloigner
            foreach (Lieu lieu1 in listeLieux)
            {
                foreach (Lieu lieu2 in listeLieux)
                {
                    //calule la distance
                    int distance = FloydWarshall.Distance(lieu1, lieu2);
                    //si distance suppérieur a maximum regitre ses nouveaux sommets
                    if (distance > maximum)
                    {
                        maximum = distance;
                        depart = lieu1;
                        lePlusLoin = lieu2;
                    }

                }
            }
            //on ajoute ses deux lieux dans la tournee
            listeFinal.Add(depart);
            listeFinal.Add(lePlusLoin);
            NonVisiter.Remove(depart);
            NonVisiter.Remove(lePlusLoin);

            //tand que il reste des lieux nonvisiter
            while (NonVisiter.Count > 0)
            {
                //voisin
                Lieu lieuProche = null;
                //index du lieux precedant dans la liste
                int lieuPrecedentPosition = 0;
                int distanceMini = int.MaxValue;
                //je parcours dans la liste nonvisiter
                foreach (Lieu lieu in NonVisiter)
                {
                    int position = -1;
                    int max = int.MaxValue;

                    //je parcours ma liste final
                    for (int i = 0; i < listeFinal.Count; i++)
                    {
                        //je recupere deux lieux
                        Lieu avant = listeFinal[i];
                        Lieu apres = listeFinal[(i + 1) % listeFinal.Count];
                        //calcule du detour
                        int distance = Distance(lieu, avant, apres);
                        //distance inférieur ou pas de valeur
                        if (position == -1 || distance < max)
                        {
                            max = distance;
                            position = i;
                        }
                    }
                    // distance inferieur ou pas de valeur
                    if (lieuProche == null || max < distanceMini)
                    {
                        lieuProche = lieu;
                        lieuPrecedentPosition = position;
                        distanceMini = max;
                    }

                }

                //on la rajoute dans la liste final et on l'nlève de la liste non visiter
                listeFinal.Insert(lieuPrecedentPosition + 1, lieuProche);
                NonVisiter.Remove(lieuProche);


            }


            List<Lieu> tempo = new List<Lieu>();
            bool usineTrouver = false;
            //remet l'usine dans la premiere position
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