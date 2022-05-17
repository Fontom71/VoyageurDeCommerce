﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoyageurDeCommerce.modele.distances;
using VoyageurDeCommerce.modele.lieux;

namespace VoyageurDeCommerce.modele.algorithmes.realisations
{
    class AlgoRechercheLocaleV2 : Algorithme
    {
        public override string Nom => "recheche local v2";

        public override void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            FloydWarshall.calculerDistances(listeLieux, listeRoute);
            int valeur = int.MaxValue;
            Lieu usine = null;
            Lieu lieu2 = null;
            List<Lieu> listeDonner = new List<Lieu>();
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
            listeDonner.Add(usine);
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
                valeur = int.MaxValue;
                listeDonner.Add(lieu2);
                lieu1 = lieu2;
                nonVisiter.Remove(lieu1);

            }

            List<Lieu> tourneeOptimal = new List<Lieu>(listeDonner);
            int valeurMin = AlgoRechercheLocale.calculeDistance(listeDonner);
            int valeurPre = 0;
            while (valeurMin != valeurPre)
            {
                for (int i = 1; i < listeDonner.Count; i++)
                {
                    valeurPre = valeurMin;
                    List<Lieu> liste = new List<Lieu>(listeDonner);
                    int resultatIntermediaire = 0;
                    Lieu tempo;
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

                    resultatIntermediaire = calculeDistance(tourneeOptimal);

                    if (resultatIntermediaire < valeurMin)
                    {
                        valeurMin = resultatIntermediaire;
                        tourneeOptimal = liste;
                    }

                }
                listeDonner = tourneeOptimal;
            }
            foreach (Lieu lieu in tourneeOptimal)
            {
                this.Tournee.Add(lieu);
                this.NotifyPropertyChanged("Tournee");
            }


        }
    }
}