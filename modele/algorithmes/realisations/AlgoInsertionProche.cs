using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoyageurDeCommerce.modele.distances;
using VoyageurDeCommerce.modele.lieux;

namespace VoyageurDeCommerce.modele.algorithmes.realisations
{
  class AlgoInsertionProche : Algorithme
  {
    public override string Nom => "Insertion proche";

    private List<int> dist = new List<int>();
    private int t;

    public override void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
    {
      FloydWarshall.calculerDistances(listeLieux, listeRoute);
      for (int i = 0; i < listeLieux.Count - 1; i++)
      {
        t = FloydWarshall.Distance(listeLieux[i], listeLieux[i + 1]);
        dist.Add(t);
      }
      this.Tournee.Add(listeLieux[0]);
      for (int i = 0; i < dist.Count; i++)
      {
        for (int j = 0; j < listeLieux.Count - 1; j++)
        {
          if (dist[i] == FloydWarshall.Distance(listeLieux[0], listeLieux[j + 1]))
          {
            this.Tournee.Add(listeLieux[j]);
          }
        }
      }
    }
  }
}
