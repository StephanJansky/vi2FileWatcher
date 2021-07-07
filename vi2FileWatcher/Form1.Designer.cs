
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.btnWatch = new System.Windows.Forms.Button();
            this.txtFileToWatch = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkInclSubDirs = new System.Windows.Forms.CheckBox();
            this.lstViewLog = new System.Windows.Forms.ListView();
            this.colDateTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colChgType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colChgFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colChgMsg = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chrtFileGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chrtFileGraph)).BeginInit();
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
            // lstViewLog
            // 
            this.lstViewLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDateTime,
            this.colChgType,
            this.colChgFile,
            this.colChgMsg});
            this.lstViewLog.FullRowSelect = true;
            this.lstViewLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstViewLog.HideSelection = false;
            this.lstViewLog.Location = new System.Drawing.Point(12, 64);
            this.lstViewLog.MultiSelect = false;
            this.lstViewLog.Name = "lstViewLog";
            this.lstViewLog.Size = new System.Drawing.Size(536, 374);
            this.lstViewLog.TabIndex = 7;
            this.lstViewLog.UseCompatibleStateImageBehavior = false;
            this.lstViewLog.View = System.Windows.Forms.View.Details;
            this.lstViewLog.SelectedIndexChanged += new System.EventHandler(this.lstViewLog_SelectedIndexChanged);
            // 
            // colDateTime
            // 
            this.colDateTime.Text = "Date/Time";
            this.colDateTime.Width = 113;
            // 
            // colChgType
            // 
            this.colChgType.Text = "ChangeType";
            this.colChgType.Width = 84;
            // 
            // colChgFile
            // 
            this.colChgFile.Text = "File";
            this.colChgFile.Width = 84;
            // 
            // colChgMsg
            // 
            this.colChgMsg.Text = "Message";
            this.colChgMsg.Width = 250;
            // 
            // chrtFileGraph
            // 
            this.chrtFileGraph.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea3.Name = "ChartArea1";
            this.chrtFileGraph.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chrtFileGraph.Legends.Add(legend3);
            this.chrtFileGraph.Location = new System.Drawing.Point(558, 12);
            this.chrtFileGraph.Name = "chrtFileGraph";
            this.chrtFileGraph.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Grayscale;
            this.chrtFileGraph.Size = new System.Drawing.Size(399, 426);
            this.chrtFileGraph.TabIndex = 8;
            title3.Name = "Standard";
            this.chrtFileGraph.Titles.Add(title3);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 450);
            this.Controls.Add(this.chrtFileGraph);
            this.Controls.Add(this.lstViewLog);
            this.Controls.Add(this.chkInclSubDirs);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtFileToWatch);
            this.Controls.Add(this.btnWatch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "vi2FileWatcher";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Leave += new System.EventHandler(this.Form1_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.chrtFileGraph)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnWatch;
        private System.Windows.Forms.TextBox txtFileToWatch;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkInclSubDirs;
        private System.Windows.Forms.ListView lstViewLog;
        private System.Windows.Forms.ColumnHeader colDateTime;
        private System.Windows.Forms.DataVisualization.Charting.Chart chrtFileGraph;
        private System.Windows.Forms.ColumnHeader colChgType;
        private System.Windows.Forms.ColumnHeader colChgFile;
        private System.Windows.Forms.ColumnHeader colChgMsg;
    }
}

