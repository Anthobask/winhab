using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace WinHab.classes
{
    class Huffman
    {
        // Permet de créer l'arbre à partir d'une chaine de caractere

        private string chaine;
        private Noeud ArbreHuffman;
        private Dictionary<char, string> DicoResultat;
        private string Filehab;

        public Huffman()
        {
            this.Filehab = Controlleur.getInstance().LienFileInput;
        }
        public Huffman(string s)
        {
            chaine = s;
            Dictionary<char, int> leDictionnaire = new Dictionary<char, int>();
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
            ArbreHuffman = huffmanArbre.LesNoeuds[0];
            //Puis on ecrit ce dictionnaire.
            DicoResultat = new Dictionary<char, string>();
            this.buildDico(ArbreHuffman, "");

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

        public void save()
        {
           // On parcour le dictionnaire une premiere fois pour définir la taille max de l'encodage des caractère

            int max = 0xFF;
            int size_car = 0x01;
            foreach (var item in DicoResultat)
            {
                if ((int)item.Key > max)
                {
                    size_car++;
                    max = max * 0x0100 + 0xFF;
                }
            }


            int endstring;
            BinaryWriter bw = null;
            bw = new BinaryWriter(File.Create(Controlleur.getInstance().LienFileOutput + "_byte"));
            bw.Write(Convert.ToByte(size_car));
            foreach (var item in DicoResultat)
            {
                int hexSize = item.Value.Count(); 
                bw.Write(Convert.ToByte(hexSize));

                int offset = 0xFF;
                int size_car_max = max;
                while (offset < max)
                {
                    if ((int)item.Key <= offset)
                    {
                        bw.Write(Convert.ToByte(0x00));
                    }
                    offset = offset * 0x0100 + 0xFF;
                }
                if ((int)item.Key > 0xFF)
                {
                   int value = (int)item.Key;
                   int[] test = new int[size_car];
                   int i=0x00;
                   while(i < size_car)
                   {
                        test[i] = value - (value / 0x100)*0x100;
                        value = (value / 0x100);
                        i++;
                   }

                   for (i = test.Count() - 1; i >= 0; i--)
                   {
                       bw.Write(Convert.ToByte(test[i]));
                   }
                }
                else
                {
                    bw.Write(Convert.ToByte(item.Key));
                }
               
                //ecrivons le string binaire en DURE (en byte code) dans le fichier :

                endstring = item.Value.Count() % 2;
                string chaineBit;
                if (endstring > 0) chaineBit = item.Value + "0";
                else chaineBit = item.Value;

                for (int i = 0; i < chaineBit.Count(); i += 2)
                {
                    bw.Write(Convert.ToByte(chaineBit.Substring(i, 2), 16)); // 16 : on lui faire croire que c'est 10 est de l'hex, sinon il écrit 02.
                }
            }
            // On marque la fin du dictionnaire :
            bw.Write(Convert.ToByte("00", 16));
            // On démarre l'ecriture du texte :    



            string chainebinaire = "";
            foreach (char c in chaine)
            {
                foreach (var item in DicoResultat)
                {
                    if (item.Key == c)
                    {
                        chainebinaire += item.Value;
                        break;
                    }
                }
            }


            endstring = chainebinaire.Count() % 4;
            if (endstring > 0) for (int i = 0; i < (4 - endstring); i++) chainebinaire += "0";
            string chaineHex = "";
            for (int i = 0; i < chainebinaire.Count(); i += 4)
            {
                chaineHex += Convert.ToInt32(chainebinaire.Substring(i, 4), 2).ToString("X");
            }
            
            endstring = chaineHex.Count() % 2;
            if (endstring > 0) chaineHex += "0";
            for (int i = 0; i < chaineHex.Count(); i += 2)
            {
                bw.Write(Convert.ToByte(chaineHex.Substring(i, 2), 16));
            }


           bw.Close();
        }



        public  void decompresse()
        {
            BinaryReader br = new BinaryReader(File.OpenRead(this.Filehab));
                       
            byte sizeCar = br.ReadByte();
            // dictionnaire :
            byte length = br.ReadByte();
           
            while (length != 0x00)
            {
                // caractere ;
                byte caractere = 0x00;
                for (int i = 0; i < sizeCar; i++)
                {
                    byte curB = br.ReadByte();
                    if (caractere == 0x00)
                    {
                        caractere = curB;
                    }
                    else
                    {
                        caractere = (byte)(caractere * 0x100 + curB);
                    }
                }

                // Noeud arbre
                byte length_f;
                string noeud;
                if (length % 2 != 0) length_f = (byte)(length + 0x01);
                else length_f = length;
                int z = 0;
                for (int i = 0; i < length; i += 2)
                {
                    //noeud += br.ReadByte().ToString("X");
                }


            }



        }
    }

    
   

}
