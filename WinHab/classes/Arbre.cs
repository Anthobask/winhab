using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinHab.classes
{

    class Arbre
    {
        private Dictionary<char, int> leDictionnaire;
        private List<Noeud> lesNoeuds;

        internal List<Noeud> LesNoeuds
        {
            get { return lesNoeuds; }
            set { lesNoeuds = value; }
        }

        public Arbre(Dictionary<char, int> dico)
        {
            // Le dictionnaire d'entrée est trié du plus léger au plus lourd :
            this.leDictionnaire = sortDicoByValue(dico);
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

                lesNoeuds = sortListNoeudsByValue(lesNoeuds);

                
            }
            Console.WriteLine("coucou");
        }

        private void initFeuillage()
        {
            this.lesNoeuds = new List<Noeud>();
            // On créer la liste des noeuds de la plus petite à la plus grande :
            foreach (var item in this.leDictionnaire.OrderBy(i => i.Value))
            {
                Noeud unNoeud = new Noeud(item.Value, item.Key);
                lesNoeuds.Add(unNoeud);
            }
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


        private Dictionary<char, int> sortDicoByValue(Dictionary<char, int> unDictionnaire)
        {
            Dictionary<char, int> dicoSorted = new Dictionary<char, int>();
            foreach (var item in unDictionnaire.OrderBy(i => i.Value))
            {
                dicoSorted.Add(item.Key, item.Value);
            }
            return dicoSorted;
        }

        private List<Noeud> sortListNoeudsByValue(List<Noeud> listeNoeuds)
        {
            List<Noeud> listeSorted = new List<Noeud>();
            foreach (Noeud item in listeNoeuds.OrderBy(i => i.Valeur))
            {
                listeSorted.Add(item);
            }
            return listeSorted;
        }


    }
}
