using System.Collections.Generic;
using System.Diagnostics;
using VoyageurDeCommerce.modele.distances;
using VoyageurDeCommerce.modele.lieux;

namespace VoyageurDeCommerce.modele.algorithmes.realisations
{
    class AlgorithmeCroissant : Algorithme
    {
        public override string Nom => "Tournée croissante";

        public override void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            FloydWarshall.calculerDistances(listeLieux, listeRoute);
            foreach (Lieu lieu in listeLieux)
            {
                this.Tournee.Add(lieu);
                sw.Stop();
                this.NotifyPropertyChanged("Tournee");
                sw.Start();
            }
            sw.Stop();
            this.TempsExecution = sw.ElapsedMilliseconds;
        }
    }
}
