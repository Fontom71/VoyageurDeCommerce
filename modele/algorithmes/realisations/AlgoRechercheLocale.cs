using System.Collections.Generic;
using VoyageurDeCommerce.modele.distances;
using VoyageurDeCommerce.modele.lieux;

namespace VoyageurDeCommerce.modele.algorithmes.realisations
{
    class AlgoRechercheLocale : Algorithme
    {
        public override string Nom => "Recherche locale";

        /// <summary>
        /// calcule la distance de la tournne
        /// </summary>
        /// <param name="lieus">tournee</param>
        /// <returns></returns>
        public int calculeDistance(List<Lieu> lieus)
        {
            int result = 0;
            for (int i = 0; i < lieus.Count; i++)
            {
                result += FloydWarshall.Distance(lieus[i], lieus[(i + 1) % lieus.Count]);
            }
            return result;
        }

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

        public override void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            //
            //                                      ALGO PLUS PROCHE VOISIN
            //

            List<Lieu> listeDonner = new List<Lieu>();
            FloydWarshall.calculerDistances(listeLieux, listeRoute);

            //Je copie ma liste de lieux dans une liste nonvisiter
            List<Lieu> nonVisiter = new List<Lieu>(listeLieux);
            //Je recherche l'usine
            Lieu lieuActuel = TrouverUsine(listeLieux);
            //je met l'usine dans la trouner puis je l'enleve de la liste non visiter
            listeDonner.Add(lieuActuel);
            nonVisiter.Remove(lieuActuel);
         
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
                listeDonner.Add(voisinPlusProche);
                lieuActuel = voisinPlusProche;
                nonVisiter.Remove(lieuActuel);
               
            }



            //
            //                                      RECHERCHE LOCAL
            //

            // liste donner par l'algo plus proche voisin
            List<Lieu> tourneeOptimal = new List<Lieu>(listeDonner);
            // je calcule la distance de la tourner dans le but d'initialiser valeurMin de la tourner
            int valeurMin = calculeDistance(listeDonner);
            // valeur à au debut de la boucle de valeurMin
            int valeurPre = 0;
            // tend que l'algo trouve mieux
            while (valeurMin != valeurPre) 
            {
                valeurPre = valeurMin;
                for (int i = 1; i < listeDonner.Count; i++)
                {
                    //je copie la liste donner dans le but de faire des modification dessus
                    List<Lieu> liste = new List<Lieu>(listeDonner);
                    // liste temporaire qui stock la tournee la plus courte
                    Lieu tempo;
                    // inverse deux sommets dans la tourner dans la cas où nous somme a la fin de la tourner inverse avec l'index un
                    if (i == listeDonner.Count - 1)
                    {
                        tempo = listeDonner[i];
                        liste[i] = liste[1];
                        liste[1] = tempo;
                    }
                    else
                    {
                        tempo = listeDonner[i];
                        liste[i] = liste[i + 1];
                        liste[i + 1] = tempo;
                    }
                    // calcule la distance de la nouvelle tournee
                    int resultatIntermediaire = calculeDistance(liste);
                    // dans le cas ou la nouvelle tournee est plus court en registre sa distance et la tournee
                    if (resultatIntermediaire < valeurMin)
                    {
                        valeurMin = resultatIntermediaire;
                        tourneeOptimal = liste;
                    }

                }
                // je redonne la nouvelle liste pour continuer les modifications
                listeDonner = tourneeOptimal;
            }
            
            // Je met ma optimal trouver par mon algo dans tournee
            foreach (Lieu lieu in tourneeOptimal)
            {
                this.Tournee.Add(lieu);
                this.NotifyPropertyChanged("Tournee");
            }
            
           
        }
    }
}
