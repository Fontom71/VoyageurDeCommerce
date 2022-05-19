using System.Collections.Generic;
using System.Diagnostics;
using VoyageurDeCommerce.modele.distances;
using VoyageurDeCommerce.modele.lieux;

namespace VoyageurDeCommerce.modele.algorithmes.realisations
{
    class AlgoPlusProcheVoisin : Algorithme
    {
        public override string Nom => "Plus proche voisin";

        private List<int> dist = new List<int>();


        public Lieu TrouverUsine(List<Lieu> liste)
        {
            Lieu usine = null;
            foreach (Lieu lieu in liste)
            {
                if (lieu.Type == TypeLieu.USINE) { usine = lieu;}
            }
            return usine;
        }
        
        public override void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            FloydWarshall.calculerDistances(listeLieux, listeRoute);

            int valeur = int.MaxValue;
            List<Lieu> nonVisiter = new List<Lieu>(listeLieux);
            Lieu voisin = null;
            Lieu lieuActuel = TrouverUsine(listeLieux);
            this.Tournee.Add(lieuActuel);
            nonVisiter.Remove(lieuActuel);

            sw.Stop();
            this.NotifyPropertyChanged("Tournee");
            sw.Start();

            for (int i = 0; i < listeLieux.Count-1; i++)
            {
                foreach (Lieu lieu in nonVisiter)
                {
                    int distance = FloydWarshall.Distance(lieuActuel, lieu);
                    if (distance < valeur)
                    {
                        voisin = lieu;
                        valeur = distance;
                    }
                }
                valeur = int.MaxValue;
                this.Tournee.Add(voisin);
                lieuActuel = voisin;
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
