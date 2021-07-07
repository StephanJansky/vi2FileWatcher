
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.btnWatch = new System.Windows.Forms.Button();
            this.txtFileToWatch = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkInclSubDirs = new System.Windows.Forms.CheckBox();
            this.lstViewLog = new System.Windows.Forms.ListView();
            this.colLogEntry = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.colLogEntry});
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
            // 
            // colLogEntry
            // 
            this.colLogEntry.Text = "Log Entry";
            this.colLogEntry.Width = 532;
            // 
            // chrtFileGraph
            // 
            this.chrtFileGraph.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.Name = "ChartArea1";
            this.chrtFileGraph.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chrtFileGraph.Legends.Add(legend1);
            this.chrtFileGraph.Location = new System.Drawing.Point(558, 12);
            this.chrtFileGraph.Name = "chrtFileGraph";
            this.chrtFileGraph.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Grayscale;
            series1.BackSecondaryColor = System.Drawing.Color.Black;
            series1.BorderColor = System.Drawing.Color.Black;
            series1.BorderWidth = 3;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.Red;
            series1.IsValueShownAsLabel = true;
            series1.Legend = "Legend1";
            series1.Name = "<DEFAULT>";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.chrtFileGraph.Series.Add(series1);
            this.chrtFileGraph.Size = new System.Drawing.Size(399, 426);
            this.chrtFileGraph.TabIndex = 8;
            title1.Name = "Standard";
            this.chrtFileGraph.Titles.Add(title1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 450);
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
        private System.Windows.Forms.ColumnHeader colLogEntry;
        private System.Windows.Forms.DataVisualization.Charting.Chart chrtFileGraph;
    }
}

