using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VoyageurDeCommerce.modele.distances;
using VoyageurDeCommerce.modele.lieux;

namespace VoyageurDeCommerce.modele.algorithmes.realisations
{
    class AlgorithmeCroissant : Algorithme
    {
        public override string Nom => "Tournée croissante";

        public override void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            DateTimeOffset timeStart = DateTime.Now;
            FloydWarshall.calculerDistances(listeLieux, listeRoute);
            foreach (Lieu lieu in listeLieux)
            {
                this.Tournee.Add(lieu);
                this.NotifyPropertyChanged("Tournee");
            }
            DateTimeOffset timeStop = DateTime.Now;
            int time = (int)(timeStop.ToUnixTimeMilliseconds() - timeStart.ToUnixTimeMilliseconds());
            Console.WriteLine(time);
            MessageBox.Show("Latence: " + time.ToString() + " ms", "Latence de traitement", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
