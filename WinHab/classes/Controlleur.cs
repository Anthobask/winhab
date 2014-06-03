using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinHab.windows;

namespace WinHab.classes
{
    class Controlleur
    {
        private static Controlleur _instance;
        static readonly object instanceLock = new object();

        private string lienFileInput;
        private string lienFileOutput;

        private Form1 mainVue;

        public Form1 MainVue
        {
            get { return mainVue; }
            set { mainVue = value; }
        }

        public string LienFileOutput
        {
            get { return lienFileOutput; }
            set { lienFileOutput = value; }
        }
        public string LienFileInput
        {
            get { return lienFileInput; }
            set { 
                lienFileInput = value;
                lienFileOutput = value + ".hab";
            }
        }

        private Controlleur()
        {

        }

        public static Controlleur getInstance()
        {
            if (_instance == null) //Les locks prennent du temps, il est préférable de vérifier d'abord la nullité de l'instance.
            {
                lock (instanceLock)
                {
                    if (_instance == null) //on vérifie encore, au cas où l'instance aurait été créée entretemps.
                        _instance = new Controlleur();
                }
            }
            return _instance;
        }

        //gestion bar de progression :
        public void initProgressBar(int min, int max)
        {
            this.mainVue.setMinProgressBar(min);
            this.mainVue.setMaxProgressBar(max);
            this.mainVue.setValueProgressBar(min);
        }
        public void setValueProgressBar(int value)
        {
            this.mainVue.setValueProgressBar(value);
        }
        public void setMessageProgressBar(string chaine)
        {
            this.mainVue.setMessageProgressBar(chaine);
        }
    }
}


