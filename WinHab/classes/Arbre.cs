using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinHab.classes
{
    class Arbre
    {
        Dictionary<char, int> leDictionnaire;
        List<Feuille> lesFeuilles;

        public Arbre(Dictionary<char, int> dico)
        {
            leDictionnaire = dico;
            //chaque occurence du dictionnaire devient une feuille.

            //on trouve la plus grande occurence et on l'ajoute dans la liste.
            int tailleDico = leDictionnaire.Count;
            lesFeuilles = new List<Feuille>();
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
                lesFeuilles.Add(new Feuille(maxValue, occurenceMax));
                //on supprime cette ligne du dico pour le plus la rencontrer
                leDictionnaire.Remove(occurenceMax);
            }
            Console.WriteLine("toutes les feuilles sont créées.");
        }

    }
}
