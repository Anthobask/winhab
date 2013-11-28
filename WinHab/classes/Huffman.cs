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
        public Huffman(string s)
        {
            chaine = s;
            leDictionnaire = new Dictionary<char, int>();
            // il faut construire le dictionnaire à partir de la chaine.
            char c;
            for (int i = 0; i < chaine.Length; i++)
            {
                if (char.TryParse(chaine[i].ToString(), out c))
                {
                    //on verifie si c existe déjà dans le dictionnaire.
                    if (leDictionnaire.ContainsKey(c))
                    {
                        // la valeur existe déjà, on incrémente juste la valeur
                        leDictionnaire[c]++;
                    }
                    else
                    {
                        leDictionnaire.Add(c, 1);
                    }
                }
                else Console.WriteLine("\n\nErreur\n\n");
            }
            //dictionnaire terminé, on doit créer l'arbre à partir de se dictionnaire
            Arbre huffmanArbre = new Arbre(leDictionnaire);
       }
    }
}
