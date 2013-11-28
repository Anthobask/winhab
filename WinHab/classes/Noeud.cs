using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinHab.classes
{
    class Noeud
    {
        /* 
            Un noeud c'est : un valeur obligatoire accompagné soit par : 
              - un caractere. Dans ce cas ce noeud sera une feuille.
              - deux autres noeuds, dans ce cas, ce noeud est un arbres.
        */
        int valeur = 0;
        Feuille feuilleG = null;
        Feuille feuilleD = null;
        Noeud noeudG = null;
        Noeud noeudD = null;

        public Noeud(Feuille fG, Feuille fD, int v)
        {
            valeur = v;
            feuilleG = fG;
            feuilleD = fD;
        }
        public Noeud(Feuille fG, Noeud nD, int v)
        {
            valeur = v;
            feuilleG = fG;
            noeudD = nD;
        }
        public Noeud(Noeud nG, Feuille fD, int v)
        {
            valeur = v;
            noeudG = nG;
            feuilleD = fD;
        }
        public Noeud(Noeud nG, Noeud nD, int v)
        {
            valeur = v;
            noeudD = nD;
            noeudG = nG;
        }

    }
}
