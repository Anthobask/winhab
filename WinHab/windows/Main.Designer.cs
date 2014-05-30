namespace WinHab.windows
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bt_SelectFile = new System.Windows.Forms.Button();
            this.bt_SelectFile2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bt_SelectFile
            // 
            this.bt_SelectFile.Location = new System.Drawing.Point(12, 12);
            this.bt_SelectFile.Name = "bt_SelectFile";
            this.bt_SelectFile.Size = new System.Drawing.Size(104, 64);
            this.bt_SelectFile.TabIndex = 0;
            this.bt_SelectFile.Text = "Compresser un fichier";
            this.bt_SelectFile.UseVisualStyleBackColor = true;
            this.bt_SelectFile.Click += new System.EventHandler(this.bt_SelectFile_Click);
            // 
            // bt_SelectFile2
            // 
            this.bt_SelectFile2.Location = new System.Drawing.Point(122, 12);
            this.bt_SelectFile2.Name = "bt_SelectFile2";
            this.bt_SelectFile2.Size = new System.Drawing.Size(104, 64);
            this.bt_SelectFile2.TabIndex = 1;
            this.bt_SelectFile2.Text = "Décompresser un fichier";
            this.bt_SelectFile2.UseVisualStyleBackColor = true;
            this.bt_SelectFile2.Click += new System.EventHandler(this.bt_SelectFile2_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 262);
            this.Controls.Add(this.bt_SelectFile2);
            this.Controls.Add(this.bt_SelectFile);
            this.Name = "Main";
            this.Text = "Main";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bt_SelectFile;
        private System.Windows.Forms.Button bt_SelectFile2;
    }
}