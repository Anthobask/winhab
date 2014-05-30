using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinHab.classes
{
    class Controlleur
    {
        private static Controlleur _instance;
        static readonly object instanceLock = new object();

        private string lienFileInput;
        private string lienFileOutput;

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
    }
}
