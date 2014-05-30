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

namespace WinHab.windows
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
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
                FileHuffman.save();
                
                MessageBox.Show("Terminé");
            }

            // 
            
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
    }
}
