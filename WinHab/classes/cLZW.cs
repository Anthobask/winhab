using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using WinHab.classes;

namespace WinHab
{
    class cLZW
    {
        Hashtable Dico = new Hashtable();
        private string ReturnFolder;
        private string ReturnFile;
        private string folderList = "";
        private string filesLZW = "";
        private int filePass = 0;
        private string[] FilesList;

        private static byte[] ConvertToBinary(string str)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(str);
        }


        //creation de la chaine d'encryptage des dossier
        public void encrypFolfer(string folderePath,string returnF)
        {
            ReturnFile = returnF + ".LZW";
            string[] folders = folderePath.Split('\\');
            folderList = "\\"+folders[folders.Length-1];
            ListNewFile(folderePath);
            folders = Directory.GetDirectories(folderePath);
            folderList =folderList+ "|"+Directory.GetFiles(folderePath).Length.ToString();
            for (int i = 0; i < folders.Length; i++)
            {
               folderList = folderList+"+"+lastFolder(folders[i]);
            }
            folderePath = Directory.GetParent(folderePath).FullName;
            folderList = folderList.Replace(folderePath, "");

            string folderLZW = LZW(folderList.ToCharArray());
            CreateFileLab(folderLZW+"|/folder|" + filesLZW);
            int a = 0;
            Controlleur.getInstance().setValueProgressBar(200);
            Controlleur.getInstance().setValueProgressBar(0);
        }

        //Création de l'arborecence de dossier (chaine d'entrée sous la forme chemin|nbr de ficher+chemin|nbr de fichier....)
        private void folderCreate(string Listfolders)
        {
            string[] folders;
            string[] fileFolder;
            string[] folderDetail;
            string SendFolder;
            folders = Listfolders.Split('+');
            for (int i = 0; i < folders.Length; i++)
            {
                if (!folders[i].Equals(""))
                {
                    fileFolder = folders[i].Split('|');
                    var separ = new string[] { "\\" };
                    folderDetail = fileFolder[0].Split(separ, StringSplitOptions.RemoveEmptyEntries);

                    for (int j = 1; j < fileFolder.Length ; j++)
                    {
                         SendFolder = "";
                         for (int l = 0; l < folderDetail.Length - j + 1; l++)
                             SendFolder = SendFolder + "\\" + folderDetail[l];
                        if (!Directory.Exists(ReturnFolder + SendFolder))
                        {
                            Directory.CreateDirectory(ReturnFolder + SendFolder);
                            Controlleur.getInstance().setValueProgressBar(Controlleur.getInstance().getValueProgressBar() + 13);
                            addFile(ReturnFolder + SendFolder, fileFolder[j]);// le fichier va s'ecrre dans le denier sous dossier...
                        }
                    }
                }
            }      
        }

        //ajout de fichier au dossier selectionné
        private void addFile(string Path,string nbFile)
        {

            for (int i = 0; i < Convert.ToInt32(nbFile); i++)
            {
                decryptFiles(FilesList[filePass], Path);
                filePass++;
            }





        }

        //creation de la chaine pour creer les odssier selon un arbo
        private string lastFolder(string folderePath)
        {
            Controlleur.getInstance().initProgressBar(0, 200);
            Controlleur.getInstance().setValueProgressBar(0);
            Controlleur.getInstance().setMessageProgressBar("Compression des l'arborescence");
            string result="";
            string[] folders = Directory.GetDirectories(folderePath);
            if (folders.Length == 0)
            {
                ListNewFile(folderePath);
                return folderePath + "|" + Directory.GetFiles(folderePath).Length.ToString();
            }
            else
            {
                for (int i = 0; i < folders.Length; i++)
                {
                    result = result + "+" + lastFolder(folders[i]) + "|" + Directory.GetFiles(folderePath).Length.ToString();
                }
                ListNewFile(folderePath);
                return result;
            }           
        }

        private void ListNewFile(string Path)
        {
            string[] listOfFiles = Directory.GetFiles(Path);
            for (int i = 0; i < listOfFiles.Length; i++)
            {
                filesLZW = filesLZW + stringForLZW(listOfFiles[i]);
            }

        }


        private string extractFile(string path)
        {
            string result = "";
            BinaryReader br = new BinaryReader(File.OpenRead(path));
            while (br.BaseStream.Position != br.BaseStream.Length)
            {
                result = result + ((char)br.ReadByte()).ToString();
            }
            return result;
        }

        //verification de la presence du terme dans le dictionnaire
        private bool OkDico(string text)
        {
            int hash = 256;
            for (int i = 0; i < Dico.Count; i++)
            {
                if (text.Equals(Dico[hash]))
                    return true;
                else
                    hash++;

            }
            return false;
        }

        //export du dictionnaire
        private char[] ExportDictionary()
        {
            string BufferDico;
            BufferDico = Dico.Count.ToString() + "|/Size|";
            for (int i = 0; i < Dico.Count; i++)
            {
                BufferDico = BufferDico + Dico[256 + i].ToString() + "|;|";
            }
            BufferDico = BufferDico + "|/Dico|";
           return BufferDico.ToCharArray();
        }

        //création du fichier de sortie .LZW
        private void CreateFileLab(string LZW)
        {
            //buffer = jointByteArray(ExportDictionary(),ConvertToBinary(LZW));

            BinaryWriter bw = new BinaryWriter(File.Create(ReturnFile));
            char[] dictionnaire = ExportDictionary();
            char[] LZWChar = LZW.ToCharArray();
            for (int i = 0; i < dictionnaire.Length; i++)
                bw.Write(Convert.ToByte(dictionnaire[i]));
            for (int i = 0; i < LZWChar.Length; i++)
                bw.Write(Convert.ToByte(LZWChar[i]));
            bw.Close();
        }


        private byte[] etractLZWCode()
        {
            string BufferLZW = "";
            return ConvertToBinary(BufferLZW);
        }

        private byte[] jointByteArray(byte[] a, byte[] b)
        {
            byte[] c = new byte[a.Length + b.Length];
            System.Array.Copy(a, 0, c, 0, a.Length);
            System.Array.Copy(b, 0, c, a.Length, b.Length);

            return c;
        }

        //encodage en LZW
        private string LZW(char[] text)
        {
            
            string enCours = "";
            string sortie = "";
            string ECTXT;

            for (int i = 0; i < text.Length; i++)
            {
                Controlleur.getInstance().setValueProgressBar(Controlleur.getInstance().getValueProgressBar()+1);
                if (!OkDico(enCours + text[i]) && (enCours + text[i]).Length > 1)
                {
                    ECTXT = enCours + text[i];
                   if ((enCours + text[i]).Length < 5)
                        Dico.Add((256 + Dico.Count), enCours + text[i]);
                    if (enCours.Length > 1)
                        sortie = sortie + searchDico(enCours) + ";";
                    else
                        sortie = sortie + ((int)enCours[0]).ToString() + ";";
                    enCours = text[i].ToString();
                }
                else
                    enCours = enCours + text[i];
            }

            if (enCours.Length == 0)
                sortie = "";
            else if (enCours.Length > 1)
                sortie = sortie + searchDico(enCours) + ";";
            else
                sortie = sortie + ((int)enCours[0]).ToString() + ";";

            return sortie;
        }

        //fonction d'encryptage (appuie sur le bouton)
        public bool encryp(string filePath,string returnF)
        {
            ReturnFile = returnF + ".LZW";
            CreateFileLab(stringForLZW(filePath));
            return true;
        }

        //renvoi la chaine a encoder dans le fichier LZW
        private string stringForLZW(string filePath)
        {
            char[] title = Path.GetFileName(filePath).ToCharArray();
            char[] text = extractFile(filePath).ToCharArray();

            Controlleur.getInstance().initProgressBar(0, title.Length+text.Length);
            Controlleur.getInstance().setValueProgressBar(0);
            Controlleur.getInstance().setMessageProgressBar("Compression des données en bits (étape 1/2)");

            string titleL = LZW(title) + "|/FileN|";
            string file = LZW(text) + "|/File|";

            return titleL + file;
        }

        //retourne le caractère selon le numero
        private string searchDico(string req)
        {
            for (int i = 0; i < Dico.Count - 1; i++)
            {
                if (Dico[256+i].Equals(req))
                   return Convert.ToString(256+i);
            }
            return null;
            

        }
        //fonction de decryptage (appuie sur le bouton)
         public bool decrypt(string filePath,string folder)
        {

            ReturnFolder = folder;
            string file = extractFile(filePath);
            //string file = "";
            var separ = new string[] { "|/Size|", "|/Dico|" };
            string[] words = file.Split(separ, StringSplitOptions.RemoveEmptyEntries);
            Controlleur.getInstance().initProgressBar(0, Convert.ToInt32(words[0]) );
            Controlleur.getInstance().setMessageProgressBar("Décompression du dictionnaire  (1/2)");
            Controlleur.getInstance().setValueProgressBar(0);
            putInDico(words[1]);
            if (file.Contains("|/folder|"))
                decryptFolder(words[2]);
            else
            {
                decryptFiles(words[2],ReturnFolder);
            }
            return false;

        }

        //creation du fichier decrypté
         private void decryptFiles(string chaine,string Folder)
         {

                 var separ = new string[] { "|/FileN|", "|/File|" };
                 string[] words = chaine.Split(separ, StringSplitOptions.RemoveEmptyEntries);
                 File.WriteAllBytes(Folder +"\\"+ decriptOneFile(words[0]), ConvertToBinary(decriptOneFile(words[1])));
             
         }

         private void decryptFolder(string chaine)
         {
             int a = 0;
             var separ = new string[] { "|/folder|" };
             string[] FolderFiles = chaine.Split(separ, StringSplitOptions.RemoveEmptyEntries);
             separ = new string[] { "|/File|" };
             FilesList = FolderFiles[1].Split(separ, StringSplitOptions.RemoveEmptyEntries);
             Controlleur.getInstance().initProgressBar(0, FolderFiles[1].Length);
             Controlleur.getInstance().setMessageProgressBar("Décompression des fichiers");
             Controlleur.getInstance().setValueProgressBar(0);
             for (int i = 0; i < FilesList.Length; i++)
             {
                 FilesList[i] = FilesList[i] + "|File|";
             }
             folderCreate(decriptOneFile(FolderFiles[0]));
         }

         //decryptage d'un fichier a l'aide du dictionaire
         private string decriptOneFile(string file)
        {
            string text="";
            int car;
            string[] LZW = file.Split(';');


            for (int i = 0; i < LZW.Length-1; i++)
            {
                if (LZW[i] != "")
                {
                    Controlleur.getInstance().setValueProgressBar(Controlleur.getInstance().getValueProgressBar() + 1);
                    car = Convert.ToInt32(LZW[i]);
                    if (Convert.ToInt32(LZW[i]) < 255)
                        text = text + ((char)car).ToString();
                    else
                        text = text + Dico[car];
                }
            }
            return text;
            
        }

        //decryptage du dictionnaire du fichier LZW et mise en place dans le dictionnaire de decryptage
        private bool putInDico(string dico)
         {
             var separ = new string[] { "|;|" };
             string[] dicoAdd = dico.Split(separ, StringSplitOptions.RemoveEmptyEntries);
             Dico.Clear();
             for (int i = 0; i < dicoAdd.Length - 1; i++)
             {
                 Controlleur.getInstance().setValueProgressBar(Controlleur.getInstance().getValueProgressBar() + 1);
                 Dico.Add((256+i), dicoAdd[i]);
             }
             return true;
         }


        
    }
}
