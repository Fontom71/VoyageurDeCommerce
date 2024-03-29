﻿using VoyageurDeCommerce.exception.realisations;
using VoyageurDeCommerce.modele.algorithmes.realisations;

namespace VoyageurDeCommerce.modele.algorithmes
{
    /// <summary> Fabrique des algorithmes </summary>
    public class FabriqueAlgorithme
    {
        /// <summary>
        /// Méthode de fabrication
        /// </summary>
        /// <param name="type">Type de l'algorithme à construire</param>
        /// <param name="listeLieux">Liste des lieux</param>
        /// <returns>L'algorithme créé</returns>
        public static Algorithme Creer(TypeAlgorithme type)
        {
            Algorithme algo;
            switch (type)
            {
                case TypeAlgorithme.ALGOEXEMPLE: algo = new AlgoExemple(); break;
                case TypeAlgorithme.CROISSANT: algo = new AlgorithmeCroissant(); break;
                case TypeAlgorithme.INSERTIONPROCHE: algo = new AlgoInsertionProche(); break;
                case TypeAlgorithme.VOISINPROCHE: algo = new AlgoPlusProcheVoisin(); break;
                case TypeAlgorithme.RECHERCHELOCALE: algo = new AlgoRechercheLocale(); break;
                case TypeAlgorithme.INSERTIONLOIN: algo = new AlgoInsertionLoin(); break;
                case TypeAlgorithme.RECHERCHELOCALEV2: algo = new AlgoRechercheLocaleV2(); break;

                default: throw new ExceptionAlgorithme("Vous n'avez pas modifié la fabrique des algorithmes !");
            }

            return algo;
        }
    }
}
