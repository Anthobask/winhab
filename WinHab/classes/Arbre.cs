using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinHab.classes
{
    class Arbre
    {
        Dictionary<char, int> leDictionnaire;
        List<Noeud> lesNoeuds;

        public Arbre(Dictionary<char, int> dico)
        {
            leDictionnaire = dico;

            // Création au départ d'une liste de feuille :
            this.initFeuillage();

            // Puis on crée un arbre de noeud :

            while (lesNoeuds.Count() > 1)
            {
                // premier noeud = plus leger :
                Noeud noeudTmp = new Noeud(lesNoeuds[0].Valeur + lesNoeuds[1].Valeur, lesNoeuds[0], lesNoeuds[1]);
                lesNoeuds.Remove(lesNoeuds[1]);
                lesNoeuds.Remove(lesNoeuds[0]);
                lesNoeuds.Add(noeudTmp);
                lesNoeuds = this.TriListNoeud(lesNoeuds);
            }
            Console.WriteLine("coucou");
        }

        private void initFeuillage()
        {
            //on trouve la plus grande occurence et on l'ajoute dans la liste.
            int tailleDico = leDictionnaire.Count;
            lesNoeuds = new List<Noeud>();
            for (int i = 0; i < tailleDico; i++)
            {
                int maxValue = 0;
                char occurenceMax = (char)0; //ASCII
                foreach (KeyValuePair<char, int> ligne in leDictionnaire)
                {
                    if (maxValue < ligne.Value)
                    {
                        occurenceMax = ligne.Key;
                        maxValue = ligne.Value;
                    }
                }
                //création de la feuille
                lesNoeuds.Add(new Noeud(maxValue, occurenceMax));
                //on supprime cette ligne du dico pour le plus la rencontrer
                leDictionnaire.Remove(occurenceMax);
            }
            // de la plus petite occurrence à la plus grande :
            lesNoeuds.Reverse();
            Console.WriteLine("toutes les feuilles sont créées.");
        }

        private List<Noeud> TriListNoeud(List<Noeud> listeATrier)
        {
            List<Noeud> listeReturn = new List<Noeud>();
            // on parse la liste a trier pour trouver la plus petit occurrence
            while (listeATrier.Count > 0)
            {
                int tmp_val = -1;
                int tmp_idx = 0;
                foreach (Noeud leNoeud in listeATrier)
                {
                    if (tmp_val < 0)
                    {
                        tmp_val = leNoeud.Valeur;
                    }
                    else if (leNoeud.Valeur < tmp_val)
                    {
                        tmp_val = leNoeud.Valeur;
                    }
                }
                listeReturn.Add(listeATrier[tmp_idx]);
                listeATrier.Remove(listeATrier[tmp_idx]);
            }
            return listeReturn;
        }

    }
}
