using System.Collections.Generic;
using System.Diagnostics;
using VoyageurDeCommerce.modele.distances;
using VoyageurDeCommerce.modele.lieux;

namespace VoyageurDeCommerce.modele.algorithmes.realisations
{
    class AlgoVoisinProche : Algorithme
    {
        public override string Nom => "Plus proche voisin";

        private List<int> dist = new List<int>();

        public override void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            FloydWarshall.calculerDistances(listeLieux, listeRoute);
            int valeur = 10000000;
            Lieu usine = listeLieux[0];

            Lieu lieu2 = null;
            List<Lieu> nonVisiter = new List<Lieu>(listeLieux);
            foreach (Lieu lieu in listeLieux)
            {
                if (lieu.Type == TypeLieu.USINE) { usine = lieu; };
            }
            Lieu lieu1 = usine;
            this.Tournee.Add(usine);
            nonVisiter.Remove(usine);
            sw.Stop();
            this.NotifyPropertyChanged("Tournee");
            sw.Start();

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
                this.Tournee.Add(lieu2);
                lieu1 = lieu2;
                nonVisiter.Remove(lieu1);
                sw.Stop();
                this.NotifyPropertyChanged("Tournee");
                sw.Start();
            }
            sw.Stop();
            this.TempsExecution = sw.ElapsedMilliseconds;
        }
    }
}
