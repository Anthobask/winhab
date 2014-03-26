using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinHab.classes
{
    class Noeud
    {
        int valeur = 0;

        
        char caractere;

        
        Noeud noeudD = null;


        Noeud noeudG = null;


        public char Caractere
        {
            get { return caractere; }
            set { caractere = value; }
        }
        public int Valeur
        {
            get { return valeur; }
            set { valeur = value; }
        }

        // c'est une feuille
        public Noeud(int v, char c)
        {
            valeur = v;
            caractere = c;
        }
        // c'est un noeud
        public Noeud(int v, Noeud ng, Noeud nd)
        {
            valeur = v;
            noeudD = nd;
            noeudG = ng;
        }


    }
}
