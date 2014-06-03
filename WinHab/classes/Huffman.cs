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
        public Huffman(string s, bool folder=false)
        {
            if (!folder)
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
            else
            {
                //on vérifie vite-fait si le dossier est compressable 
                if (this.checkFolder(s))
                {
                    /*on établis l'arborescence, et on collecte les sous-textes
                    Exemble d'encodage d'arborescence : 
                    "{1{2,a,b},a,b,c,d}" 
                    les caractères "," et "{" et "}" ne sont pas permis dans le nomage de fichiers. */

                    /* exemple d'encodage de la chaine :
                     Les chaines de fichiers seront respectifs aux fichiers d'arborescence (meme ordre)
                     Ils seront délimités par le caracted 0x04 (EOT, Utilisé sous Unix pour signaler une condition de fin de fichier (end-of-file)) */

                    string arborescence = "{";
                    string chaine = "";
                    parseFolder(s, ref arborescence, ref chaine);
                    arborescence += "}";
                }
                else
                {
                    // todo : Fichiers non-permis

                }

                
                

            }


        }

        private void parseFolder(string chemin, ref string arborescence, ref string chaine)
        {
            string arbo_temp = arborescence;
            string chaine_temp = chaine;

            bool firstFile = true;

            foreach (string sFileName in System.IO.Directory.GetDirectories(chemin))
            {
                arbo_temp += "{" + System.IO.Path.GetFileName(sFileName) + ",";
                parseFolder(sFileName, ref arbo_temp, ref chaine_temp);
                arbo_temp += "},";
            }
            foreach (string sFileName in System.IO.Directory.GetFiles(chemin))
            {
                // c'est un fichier
                if (!firstFile) arbo_temp += ",";
                else firstFile = false;

                arbo_temp += System.IO.Path.GetFileName(sFileName);
            }


            arborescence = arbo_temp;
            chaine = chaine_temp;
        }

        private bool checkFolder(string s)
        {
            List<string> extentionsAvailable = new List<String>();
            extentionsAvailable.Add(".txt");
            extentionsAvailable.Add(".csv");
            extentionsAvailable.Add(".xml");

            foreach (string sFileName in System.IO.Directory.GetFiles(s))
            {
                if (!extentionsAvailable.Contains(System.IO.Path.GetExtension(sFileName)))
                {
                    return false;
                }
            }
            foreach (string sFileName in System.IO.Directory.GetDirectories(s))
            {
                if (checkFolder(sFileName) == false)
                {
                    return false;
                }
            }
            return true;
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
            bw = new BinaryWriter(File.Create(Controlleur.getInstance().LienFileOutput));
            
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

            // Ecriture de l'offset de fin de chaine.
            int offsetend = 0x04 - endstring;
            bw.Write(Convert.ToByte(offsetend));


            //On démarre la bar de progression ici :
            Controlleur.getInstance().initProgressBar(0, chainebinaire.Count()-1);
            Controlleur.getInstance().setValueProgressBar(0);
            Controlleur.getInstance().setMessageProgressBar("Compression des données en bits (étape 1/2)");
            
            string chaineHex = "";
            for (int i = 0; i < chainebinaire.Count(); i += 4)
            {
                chaineHex += Convert.ToInt32(chainebinaire.Substring(i, 4), 2).ToString("X");
                Controlleur.getInstance().setValueProgressBar(i);
            }
            
            Controlleur.getInstance().initProgressBar(0, chaineHex.Count()-1);
            Controlleur.getInstance().setValueProgressBar(0);
            Controlleur.getInstance().setMessageProgressBar("Ecriture des données compressé (étape 2/2)");

            endstring = chaineHex.Count() % 2;
            if (endstring > 0) chaineHex += "0";
            for (int i = 0; i < chaineHex.Count(); i += 2)
            {
                bw.Write(Convert.ToByte(chaineHex.Substring(i, 2), 16));
                Controlleur.getInstance().setValueProgressBar(i);
            }

            //reset progress barre : 
            Controlleur.getInstance().initProgressBar(0,0);
            Controlleur.getInstance().setMessageProgressBar("Opération terminée.");
            


           bw.Close();
        }



        public  void decompresse()
        {
            BinaryReader br = new BinaryReader(File.OpenRead(this.Filehab));

            DicoResultat = new Dictionary<char, string>();

                       
            byte sizeCar = br.ReadByte();
            // dictionnaire :
            byte length = br.ReadByte();           
            while (length != 0x00)
            {
                // caractere ;
                int caractere = 0x00;
                for (int i = 0; i < sizeCar; i++)
                {
                    byte curB = br.ReadByte();
                    if (caractere == 0x00) caractere = (int)curB;
                    else caractere = caractere * 0x100 + (int)curB;
                }

                // Noeud arbre
                byte length_f;
                if (length % 2 != 0) length_f = (byte)(length + 0x01);
                else length_f = length;

                int noeud = 0x00;
                for (int i = 0; i < (length_f/2); i ++)
                {
                    byte curB = br.ReadByte();
                    if (noeud == 0x00) noeud = (int)curB;
                    else noeud = noeud * 0x100 + (int)curB;
                }

                string noeudS = noeud.ToString("X" + length_f).Substring(0, length);

                
                // Ajout dans le dictionnaire :
                DicoResultat.Add((char)caractere, noeudS);

                length = br.ReadByte();
            }

            //prochaine byte = offset de fin de chaine.
            int offsetend = br.ReadByte();

            //puis la phrase en elle-meme :
            byte byteNext;
            string chaineBinaire = "";



            while (br.BaseStream.Position !=br.BaseStream.Length)
            {
                byteNext = br.ReadByte();
                string chaine_tmp = Convert.ToString(byteNext, 16);
                if (byteNext <= 0x0f) chaine_tmp = "0" + chaine_tmp;
                string chaine1 = Convert.ToString(Convert.ToInt32(chaine_tmp.Substring(0,1), 16), 2);
                string chaine2 = Convert.ToString(Convert.ToInt32(chaine_tmp.Substring(1,1), 16), 2);

                //On ajoute les potentiel zero au debut de chaques chaines :
                chaine1 = "000" + chaine1;
                chaine1 = chaine1.Substring(chaine1.Length - 4, 4); // et on prend les 4 derniers
                chaine2 = "000" + chaine2;
                chaine2 = chaine2.Substring(chaine2.Length - 4, 4);

                chaineBinaire += chaine1 + chaine2;
            }

            // On enleve l'offset de fin : 
            chaineBinaire = chaineBinaire.Substring(0, chaineBinaire.Length - offsetend +1);

            //Puis on décode la chaine avec le dictionnaire :
            string chaine_temp = "";
            string chaine_final = "";


            System.IO.StreamWriter sw = new System.IO.StreamWriter(Controlleur.getInstance().LienFileInput + "_old");

            for (int i = 0; i < chaineBinaire.Length - 1; i++)
            {
                chaine_temp += chaineBinaire[i];
                if (DicoResultat.ContainsValue(chaine_temp))
                {
                    foreach (var item in DicoResultat)
                    {
                        if (item.Value == chaine_temp)
                        {
                            chaine_final += item.Key.ToString();
                            chaine_temp = "";
                        }
                    }

                }
            }


            sw.Write(chaine_final);
            sw.Close();
            Console.WriteLine("ok cool");


        }
    }

    
   

}
