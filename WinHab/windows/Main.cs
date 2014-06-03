using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using WinHab.classes;
using System.Threading;

namespace WinHab.windows
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            //Controlleur.getInstance().MainVue = this;
        }

        private void bt_SelectFile_Click(object sender, EventArgs e)
        {
            // Create an instance of the open file dialog box.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                   System.IO.StreamReader(openFileDialog1.FileName, Encoding.UTF8);
                string content = sr.ReadToEnd();
                sr.Close();
                Controlleur.getInstance().LienFileInput = openFileDialog1.FileName;
                Huffman FileHuffman = new Huffman(content);
                Thread t = new Thread(() =>
                {
                    FileHuffman.save();
                    MessageBox.Show("Terminé");
                });
                t.Start();                
            }            
        }

        private void bt_SelectFile2_Click(object sender, EventArgs e)
        {
            // Create an instance of the open file dialog box.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
               Controlleur.getInstance().LienFileInput = openFileDialog1.FileName;

               Huffman FileHuffman = new Huffman();
               FileHuffman.decompresse();
                

                MessageBox.Show("Terminé");
            }

            // 
        }


        #region ManageProgressBar
        public void setMinProgressBar(int min)
        {
            //this.progressBar1.Minimum = min;
            Invoke(new Action(() => this.progressBar.Minimum = min));
        }
        public void setMaxProgressBar(int max)
        {
            //this.progressBar1.Maximum = max;
            Invoke(new Action(() => this.progressBar.Maximum = max));
        }
        public void setValueProgressBar(int val, int sleep = 0)
        {
            

            Invoke(new Action(() =>
            {
                if (val >= this.progressBar.Maximum) val = progressBar.Maximum;
                this.progressBar.Value = val;
                this.progressBar.Update();
            }));
            if (this.progressBar.Value == this.progressBar.Maximum)
            {
                Thread.Sleep(1000);
                Invoke(new Action(() => this.progressBar.Value = 0));
            }
        }

        public void setMessageProgressBar(string message)
        {
            Invoke(new Action(() =>
            {
                this.lb_progressbar.Text = message;
            }));
        }
        #endregion

        private void bt_compressFolder_Click(object sender, EventArgs e)
        {
            // Create an instance of the open file dialog box.
            FolderBrowserDialog dossier = new FolderBrowserDialog();

            if (dossier.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Controlleur.getInstance().LienFileInput = dossier.SelectedPath;

                Huffman FileHuffman = new Huffman(dossier.SelectedPath, true);

                MessageBox.Show("Terminé");
            }

            //
        }

    }
}
