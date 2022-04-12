using System;
using System.Collections.Generic;
using VoyageurDeCommerce.modele.distances;
using VoyageurDeCommerce.modele.lieux;

namespace VoyageurDeCommerce.modele.algorithmes.realisations
{
    /// <summary>
    /// Exemple de classe Algorithme, à ne pas garder
    /// </summary>
    public class AlgoExemple : Algorithme
    {

        public override string Nom => "Algorithme exemple";

        public override void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            //Lancer FloydWarshall
            FloydWarshall.calculerDistances(listeLieux, listeRoute);

            //Faire quelque chose ici
            Console.WriteLine(FloydWarshall.Distance(listeLieux[1], listeLieux[2]));
            foreach(Lieu lieu in listeLieux)
            {
                Console.WriteLine(lieu.X);
            }
        }
    }
}
