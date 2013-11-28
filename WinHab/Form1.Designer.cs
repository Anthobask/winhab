namespace WinHab
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.tb_input = new System.Windows.Forms.TextBox();
            this.tb_ouput = new System.Windows.Forms.TextBox();
            this.lb1 = new System.Windows.Forms.Label();
            this.lb_tailletxt = new System.Windows.Forms.Label();
            this.lb_tailleTesultat = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tb_input
            // 
            this.tb_input.Location = new System.Drawing.Point(12, 10);
            this.tb_input.Multiline = true;
            this.tb_input.Name = "tb_input";
            this.tb_input.Size = new System.Drawing.Size(632, 100);
            this.tb_input.TabIndex = 0;
            this.tb_input.TextChanged += new System.EventHandler(this.tb_input_TextChanged);
            // 
            // tb_ouput
            // 
            this.tb_ouput.Location = new System.Drawing.Point(11, 135);
            this.tb_ouput.Multiline = true;
            this.tb_ouput.Name = "tb_ouput";
            this.tb_ouput.ReadOnly = true;
            this.tb_ouput.Size = new System.Drawing.Size(633, 98);
            this.tb_ouput.TabIndex = 2;
            // 
            // lb1
            // 
            this.lb1.AutoSize = true;
            this.lb1.Location = new System.Drawing.Point(652, 13);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(76, 13);
            this.lb1.TabIndex = 3;
            this.lb1.Text = "Taille tu texte :";
            // 
            // lb_tailletxt
            // 
            this.lb_tailletxt.AutoSize = true;
            this.lb_tailletxt.Location = new System.Drawing.Point(668, 57);
            this.lb_tailletxt.Name = "lb_tailletxt";
            this.lb_tailletxt.Size = new System.Drawing.Size(40, 13);
            this.lb_tailletxt.TabIndex = 4;
            this.lb_tailletxt.Text = "0 otect";
            // 
            // lb_tailleTesultat
            // 
            this.lb_tailleTesultat.AutoSize = true;
            this.lb_tailleTesultat.Location = new System.Drawing.Point(668, 182);
            this.lb_tailleTesultat.Name = "lb_tailleTesultat";
            this.lb_tailleTesultat.Size = new System.Drawing.Size(40, 13);
            this.lb_tailleTesultat.TabIndex = 6;
            this.lb_tailleTesultat.Text = "0 otect";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(652, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Taille du resultat :";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(658, 102);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 251);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lb_tailleTesultat);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lb_tailletxt);
            this.Controls.Add(this.lb1);
            this.Controls.Add(this.tb_ouput);
            this.Controls.Add(this.tb_input);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_input;
        private System.Windows.Forms.TextBox tb_ouput;
        private System.Windows.Forms.Label lb1;
        private System.Windows.Forms.Label lb_tailletxt;
        private System.Windows.Forms.Label lb_tailleTesultat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
    }
}

