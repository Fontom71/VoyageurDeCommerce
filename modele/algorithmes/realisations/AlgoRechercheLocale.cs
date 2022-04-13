using System.Collections.Generic;
using VoyageurDeCommerce.modele.distances;
using VoyageurDeCommerce.modele.lieux;

namespace VoyageurDeCommerce.modele.algorithmes.realisations
{
    class AlgoRechercheLocale : Algorithme
    {
        public override string Nom => "Recherche locale";

        public override void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            FloydWarshall.calculerDistances(listeLieux, listeRoute);
            int valeur = 10000000;
            Lieu usine = null;
            Lieu lieu2 = null;
            List<Lieu> resultat = new List<Lieu>();
            List<Lieu> nonVisiter = new List<Lieu>();

            foreach (Lieu lieu in listeLieux)
            {
                nonVisiter.Add(lieu);
            }
            foreach (Lieu lieu in listeLieux)
            {
                if (lieu.Type == TypeLieu.USINE) { usine = lieu; };
            }
            Lieu lieu1 = usine;
            resultat.Add(usine);
            nonVisiter.Remove(usine);


            for (int i = 0; i < listeLieux.Count - 1; i++)
            {
                foreach (Lieu lieu in nonVisiter)
                {
                    int distance = FloydWarshall.Distance(lieu1, lieu);
                    if (distance < valeur)
                    {
                        lieu2 = lieu;
                        valeur = distance;
                    }
                }
                valeur = 1000000000;
                resultat.Add(lieu2);
                lieu1 = lieu2;
                nonVisiter.Remove(lieu1);

            }

            List<Lieu> tourneeOptimal = new List<Lieu>();

            int result = 0;
            for (int j = 0; j < listeLieux.Count - 1; j++)
            {
                result += FloydWarshall.Distance(resultat[j], resultat[(j + 1) % resultat.Count]);
            }
            tourneeOptimal = resultat;


            for (int i = 1; i < resultat.Count; i++)
            {
                List<Lieu> liste = new List<Lieu>();
                int resultatIntermediaire = 0;
                Lieu tempo;
                foreach (Lieu lieu in resultat)
                {
                    liste.Add(lieu);
                }
                if (i == resultat.Count - 1)
                {
                    tempo = resultat[i];
                    liste[i] = liste[1];
                    liste[1] = tempo;
                }
                else
                {
                    tempo = resultat[i];
                    liste[i] = liste[i + 1];
                    liste[i + 1] = tempo;
                }

                for (int j = 0; j < listeLieux.Count - 1; j++)
                {
                    resultatIntermediaire += FloydWarshall.Distance(liste[j], liste[(j + 1) % liste.Count]);
                }
                if (resultatIntermediaire < result)
                {
                    result = resultatIntermediaire;
                    tourneeOptimal = liste;
                }

            }
            foreach (Lieu lieu in tourneeOptimal)
            {
                this.Tournee.Add(lieu);
                this.NotifyPropertyChanged("Tournee");
            }
        }
    }
}
