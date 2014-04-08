using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinHab.classes
{
    class Huffman
    {
        // Permet de créer l'arbre à partir d'une chaine de caractere

        string chaine;
        Dictionary<char, int> leDictionnaire;
        Dictionary<char, string> DicoResultat;

        public Huffman(string s)
        {
            chaine = s;
            leDictionnaire = new Dictionary<char, int>();
            // il faut construire le dictionnaire à partir de la chaine.
            char c;
            // parse la chaine caractere par caratere :
            for (int i = 0; i < chaine.Length; i++)
            {
                if (char.TryParse(chaine[i].ToString(), out c))
                {
                    //on verifie si le caractere existe déjà dans le dictionnaire.
                    if (leDictionnaire.ContainsKey(c))
                    {
                        // la valeur existe déjà, on incrémente juste la valeur
                        leDictionnaire[c]++;
                    }
                    else
                    {
                        // on ajoute la valeur dans le dictionnaire :
                        leDictionnaire.Add(c, 1);
                    }
                }
                else Console.WriteLine("\n\nErreur\n\n");
            }            

            //dictionnaire terminé, on doit créer l'arbre à partir du dictionnaire
            Arbre huffmanArbre = new Arbre(leDictionnaire);
            //Puis on ecrit ce dictionnaire.
            DicoResultat = new Dictionary<char, string>();
            this.buildDico(huffmanArbre.LesNoeuds[0],"");

       }

        private void buildDico(Noeud unNoeud, string p)
        {
           // Noeud ou feuille
            if (unNoeud.isLeaf())
            {
                this.DicoResultat.Add(unNoeud.Caractere, p);
            }
            else
            {
                buildDico(unNoeud.NoeudG, p + "0");
                buildDico(unNoeud.NoeudD, p + "1");
            }
        }
    }
}
