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
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lb_progressbar = new System.Windows.Forms.Label();
            this.bt_compressFolder = new System.Windows.Forms.Button();
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
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 227);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(671, 23);
            this.progressBar.TabIndex = 2;
            // 
            // lb_progressbar
            // 
            this.lb_progressbar.AutoSize = true;
            this.lb_progressbar.Location = new System.Drawing.Point(346, 201);
            this.lb_progressbar.Name = "lb_progressbar";
            this.lb_progressbar.Size = new System.Drawing.Size(0, 13);
            this.lb_progressbar.TabIndex = 3;
            // 
            // bt_compressFolder
            // 
            this.bt_compressFolder.Location = new System.Drawing.Point(12, 82);
            this.bt_compressFolder.Name = "bt_compressFolder";
            this.bt_compressFolder.Size = new System.Drawing.Size(104, 64);
            this.bt_compressFolder.TabIndex = 4;
            this.bt_compressFolder.Text = "Compresser un dossier";
            this.bt_compressFolder.UseVisualStyleBackColor = true;
            this.bt_compressFolder.Click += new System.EventHandler(this.bt_compressFolder_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 262);
            this.Controls.Add(this.bt_compressFolder);
            this.Controls.Add(this.lb_progressbar);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.bt_SelectFile2);
            this.Controls.Add(this.bt_SelectFile);
            this.Name = "Main";
            this.Text = "Main";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_SelectFile;
        private System.Windows.Forms.Button bt_SelectFile2;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lb_progressbar;
        private System.Windows.Forms.Button bt_compressFolder;
    }
}