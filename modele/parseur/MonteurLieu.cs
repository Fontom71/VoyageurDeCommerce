using System;
using VoyageurDeCommerce.modele.lieux;

namespace VoyageurDeCommerce.modele.parseur
{
    /// <summary>Monteur de lieu </summary>
    public class MonteurLieu
    {
        /// <summary>
        /// Crée un lieu à partir du tableau de string obtenu en lisant le fichier 
        /// </summary>
        /// <param name="morceaux">Les 4 morceaux de la ligne correspondant à la ligne</param>
        /// <returns>Le lieu créé</returns>
        public static Lieu Creer(String[] morceaux)
        {
            TypeLieu type;
            switch (morceaux[0])
            {
                case "MAGASIN": type = TypeLieu.MAGASIN; break;
                case "USINE": type = TypeLieu.USINE; break;
                default: throw new InvalidOperationException();
            }
            Lieu l = new Lieu(type, morceaux[1], Int32.Parse(morceaux[2]), Int32.Parse(morceaux[3]));
            return l;
        }
    }
}
