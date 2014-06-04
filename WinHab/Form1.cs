using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using WinHab.classes;

namespace WinHab
{
    public partial class Form1 : Form
    {
        cLZW LZW;
        public Form1()
        {
            InitializeComponent();
            LZW = new cLZW();
            Controlleur.getInstance().MainVue = this;
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(textBox1.Text))
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    WinHab.classes.Controlleur.getInstance().LienFileOutput = saveFileDialog1.FileName;
                }
                // todo : verifier que le fichier ouput existe ou pas.
                if (rd_huffman.Checked)
                {
                    //lecture simple du fichier
                    System.IO.StreamReader sr = new System.IO.StreamReader(textBox1.Text, Encoding.UTF8);
                    string content = sr.ReadToEnd();
                    sr.Close();
                    Controlleur.getInstance().LienFileInput = textBox1.Text;

                    Huffman FileHuffman = new Huffman(content);
                    Thread t = new Thread(() =>
                    {
                        FileHuffman.save();
                        MessageBox.Show("Terminé");
                    });
                    t.Start();
                }
                else if (rd_LZW.Checked)
                {
                    Thread t = new Thread(() =>
                    {
                        LZW.encryp(textBox1.Text);
                        MessageBox.Show("Terminé");
                    });
                    t.Start();
                }
            }
            else
            {
                MessageBox.Show("Veuillez renseigné un fichier à compresser.");
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dossier = new FolderBrowserDialog();
            if (dossier.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox2.Text = dossier.SelectedPath;
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(textBox2.Text))
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    WinHab.classes.Controlleur.getInstance().LienFileOutput = saveFileDialog1.FileName;
                }

                // todo : verifier que le fichier ouput existe ou pas.
                if (rd_huffman.Checked)
                {
                    //
                }
                else
                {
                    Thread t = new Thread(() =>
                    {
                        LZW.encrypFolfer(textBox2.Text);

                        MessageBox.Show("Terminé");
                    });
                }
            }
            else
            {
                MessageBox.Show("Veuillez renseigné un dossier à compresser.");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox4.Text = openFileDialog1.FileName;
            }
        
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(textBox4.Text))
            {
                Controlleur.getInstance().LienFileInput = textBox4.Text;
                if (rd_huffman.Checked)
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        WinHab.classes.Controlleur.getInstance().LienFileOutput = saveFileDialog1.FileName;
                    }
                    Huffman FileHuffman = new Huffman();
                    FileHuffman.decompresse();

                    MessageBox.Show("Terminé");
                }
                else if (rd_LZW.Checked)
                {
                    FolderBrowserDialog dossier = new FolderBrowserDialog();
                    if (dossier.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        LZW.decrypt(textBox4.Text, dossier.SelectedPath);

                        MessageBox.Show("Terminé");
                    }
                }
            }
             else
             {
                 MessageBox.Show("Veuillez renseigné un dossier à compresser.");
             }
        }

        private void rd_huffman_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rd_huffman.Checked)
            {
                // Huffman ne fait pas les dossier : on grise les cases en questions :
                this.textBox2.Enabled = false;
                this.button4.Enabled = false;
                this.button3.Enabled = false;
                this.label2.ForeColor = Color.Gray;

            }
        }

        private void rd_LZW_CheckedChanged(object sender, EventArgs e)
        {
            //On fait l'inverse, on active les items à dossier :
            if (this.rd_LZW.Checked)
            {
                // Huffman ne fait pas les dossier : on grise les cases en questions :
                this.textBox2.Enabled = true;
                this.button4.Enabled = true;
                this.button3.Enabled = true;
                this.label2.ForeColor = Color.Black;
            }

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
        public int getValueProgressBar()
        {
            return this.progressBar.Value;
        }
        #endregion




    }
}
