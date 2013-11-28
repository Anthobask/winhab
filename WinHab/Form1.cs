using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinHab.classes;

namespace WinHab
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tb_input_TextChanged(object sender, EventArgs e)
        {
            //Ici, on récupere le texte pour construire l'arbre
            string texte = tb_input.Text.ToString();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string texte = tb_input.Text.ToString();
            Huffman algo = new Huffman(texte);
        }
    }
}
