
namespace vi2FileWatcher
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnWatch = new System.Windows.Forms.Button();
            this.txtFileToWatch = new System.Windows.Forms.TextBox();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkInclSubDirs = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnWatch
            // 
            this.btnWatch.Location = new System.Drawing.Point(476, 38);
            this.btnWatch.Name = "btnWatch";
            this.btnWatch.Size = new System.Drawing.Size(75, 20);
            this.btnWatch.TabIndex = 0;
            this.btnWatch.Text = "Watch";
            this.btnWatch.UseVisualStyleBackColor = true;
            this.btnWatch.Click += new System.EventHandler(this.btnWatch_Click);
            this.btnWatch.Leave += new System.EventHandler(this.btnWatch_Leave);
            // 
            // txtFileToWatch
            // 
            this.txtFileToWatch.Location = new System.Drawing.Point(12, 12);
            this.txtFileToWatch.Name = "txtFileToWatch";
            this.txtFileToWatch.ReadOnly = true;
            this.txtFileToWatch.Size = new System.Drawing.Size(458, 20);
            this.txtFileToWatch.TabIndex = 1;
            this.txtFileToWatch.Text = "C:\\";
            // 
            // lstLog
            // 
            this.lstLog.FormattingEnabled = true;
            this.lstLog.Location = new System.Drawing.Point(12, 64);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(539, 381);
            this.lstLog.TabIndex = 2;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(476, 12);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 20);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(312, 38);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(158, 20);
            this.txtFilter.TabIndex = 4;
            this.txtFilter.Text = "*.*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(277, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Filter";
            // 
            // chkInclSubDirs
            // 
            this.chkInclSubDirs.AutoSize = true;
            this.chkInclSubDirs.Location = new System.Drawing.Point(12, 38);
            this.chkInclSubDirs.Name = "chkInclSubDirs";
            this.chkInclSubDirs.Size = new System.Drawing.Size(131, 17);
            this.chkInclSubDirs.TabIndex = 6;
            this.chkInclSubDirs.Text = "Include Subdirectories";
            this.chkInclSubDirs.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 450);
            this.Controls.Add(this.chkInclSubDirs);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.lstLog);
            this.Controls.Add(this.txtFileToWatch);
            this.Controls.Add(this.btnWatch);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnWatch;
        private System.Windows.Forms.TextBox txtFileToWatch;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkInclSubDirs;
    }
}

