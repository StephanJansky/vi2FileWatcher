using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace vi2FileWatcher
{
    public partial class Form1 : Form
    {
        private FileSystemWatcher fs_Watcher = null;
        public delegate void dWatcherNotificationEvent(WatcherChangeTypes eChangeType, string strFile);
        public event dWatcherNotificationEvent eWatcherNotificationEvent;
        public delegate void dNewLogEntry(WatcherChangeTypes eChangeType, string strFile, string strLogEntry);
        public event dNewLogEntry eNewLogEntry;
        public delegate void dNewFileLogEntry(string strLogEntry);
        public event dNewFileLogEntry eNewFileLogEntry;
        public StreamWriter sr_LogFile = null;
        private List<FileWatched> m_lFiles = new List<FileWatched>();

        static private string p_strLastMessage = "";
        static private string p_strCurFileShown = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                this.eWatcherNotificationEvent += Form1_eWatcherNotificationEvent;
                this.eNewLogEntry += Form1_eNewLogEntry;
                this.eNewFileLogEntry += Form1_eNewFileLogEntry;
            }
            catch (Exception ex)
            {
                logException(ex.Message);
            }
        }

        private void Form1_eNewFileLogEntry(string strLogEntry)
        {
            try
            {
                if (System.IO.File.Exists("vi2FileWatcher.log"))
                    sr_LogFile = System.IO.File.AppendText("vi2FileWatcher.log");
                else
                    sr_LogFile = new StreamWriter("vi2FileWatcher.log");

                sr_LogFile.WriteLine(getFormattedDateTime() + " - " + strLogEntry);
                sr_LogFile.Close();
            }
            catch (Exception ex)
            {
                logException(ex.Message);
            }
        }

        private void Form1_eNewLogEntry(WatcherChangeTypes eChangeType, string strFile, string strLogEntry)
        {
            if (lstViewLog.InvokeRequired)
            {
                lstViewLog.Invoke(new MethodInvoker(() => { Form1_eNewLogEntry(eChangeType, strFile, strLogEntry); }));
                return;
            }

            try
            {
                string strDateTime = getFormattedDateTime();
                ListViewItem lstVCurItem = lstViewLog.Items.Add(strDateTime);
                lstVCurItem.SubItems.Add(eChangeType.ToString());
                lstVCurItem.SubItems.Add(strFile.Replace(txtFileToWatch.Text, "..."));
                lstVCurItem.SubItems.Add(strLogEntry);
                if (lstViewLog.Items.Count > 1)
                    lstViewLog.Items[lstViewLog.Items.Count - 1].EnsureVisible();

            }
            catch (Exception ex)
            {
                logException(ex.Message);
            }

            lstViewLog.Refresh();
        }

        private void Form1_eWatcherNotificationEvent(WatcherChangeTypes eChangeType, string strFile)
        {
            try
            {
                string strMessage = "";
                string strDateTime = "";
                FileWatched NewFile = new FileWatched(strFile);
                int iIndex = -1;

                if (eChangeType != WatcherChangeTypes.Deleted)
                {
                    FileAttributes attr = File.GetAttributes(strFile);
                    if ((attr & FileAttributes.Directory) != FileAttributes.Directory)
                    {
                        strDateTime = DateTime.Now.ToString();
                        iIndex = m_lFiles.FindIndex(x => x.Filename == NewFile.Filename);
                        if (iIndex > -1)
                            m_lFiles[iIndex].AddValue();
                        else
                            m_lFiles.Add(new FileWatched(strFile));
                    }
                    else
                        return;
                }

                strMessage = "New size is " + convertSize(m_lFiles[iIndex].LastValue);
                iIndex = m_lFiles.FindIndex(x => x.Filename == NewFile.Filename);
                this.eNewLogEntry(eChangeType, strFile, strMessage);
                this.eNewFileLogEntry(strFile + ": " + strMessage);
                if (NewFile.Fullname == p_strCurFileShown)
                    adaptGraph(m_lFiles[m_lFiles.FindIndex(x => x.Filename == NewFile.Filename)], false);

                p_strLastMessage = strMessage;
            }
            catch (Exception ex)
            {
                logException(ex.Message);
            }
        }

        private void Fs_Watcher_Error(object sender, ErrorEventArgs e)
        {
            this.eWatcherNotificationEvent(WatcherChangeTypes.All, e.GetException().Message);
        }

        private void Fs_Watcher_Created(object sender, FileSystemEventArgs e)
        {
            m_lFiles.Add(new FileWatched(e.FullPath));
            this.eWatcherNotificationEvent(e.ChangeType, e.FullPath);
        }

        private void Fs_Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            this.eWatcherNotificationEvent(e.ChangeType, e.FullPath);
            int iIndex = m_lFiles.FindIndex(x => x.Filename == e.Name);
            m_lFiles.RemoveAt(iIndex);
        }

        private void Fs_Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            this.eWatcherNotificationEvent(e.ChangeType, e.FullPath);
        }

        private void Fs_Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            this.eWatcherNotificationEvent(e.ChangeType, e.FullPath);
        }

        private void btnWatch_Click(object sender, EventArgs e)
        {
            if (btnWatch.Text == "Watch")
            {
                if (String.IsNullOrEmpty(txtFileToWatch.Text))
                {
                    this.eNewLogEntry(WatcherChangeTypes.All, "<NONE>", "Folder to watch is EMPTY!");
                    return;
                }

                m_lFiles.Clear();
                this.eNewFileLogEntry("***Start: " + DateTime.Now + "***");
                if (chkInclSubDirs.Checked)
                {
                    this.eNewFileLogEntry("Watching Directory " + txtFileToWatch.Text + " and Subdirectories with Filter (" + txtFilter.Text + ")");
                    this.eNewLogEntry(WatcherChangeTypes.All, "<NONE>", "Watching Directory " + txtFileToWatch.Text + " and Subdirectories with Filter (" + txtFilter.Text + ")");
                }
                else
                {
                    this.eNewFileLogEntry("Watching Directory " + txtFileToWatch.Text + " with Filter (" + txtFilter.Text + ")");
                    this.eNewLogEntry(WatcherChangeTypes.All, "<NONE>", "Watching Directory " + txtFileToWatch.Text + " with Filter (" + txtFilter.Text + ")");
                }

                foreach (string strFile in System.IO.Directory.GetFiles(txtFileToWatch.Text))
                {
                    m_lFiles.Add(new FileWatched(strFile));
                    this.eWatcherNotificationEvent(WatcherChangeTypes.Changed, strFile);
                }

                fs_Watcher = new FileSystemWatcher(txtFileToWatch.Text);
                fs_Watcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Security
                                        | NotifyFilters.Size;
                fs_Watcher.IncludeSubdirectories = chkInclSubDirs.Checked;
                fs_Watcher.EnableRaisingEvents = true;
                fs_Watcher.Filter = txtFilter.Text;
                fs_Watcher.Changed += Fs_Watcher_Changed;
                fs_Watcher.Renamed += Fs_Watcher_Renamed;
                fs_Watcher.Deleted += Fs_Watcher_Deleted;
                fs_Watcher.Created += Fs_Watcher_Created;
                fs_Watcher.Error += Fs_Watcher_Error;

                btnWatch.Text = "STOP";
            }
            else
            {
                fs_Watcher.Dispose();
                fs_Watcher = null;
                btnWatch.Text = "Watch";
                this.eNewLogEntry(WatcherChangeTypes.All, "<NONE>", "Stopped watching.");
                this.eNewFileLogEntry("***END: " + DateTime.Now + "***");
                sr_LogFile = null;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult drBrowse = folderBrowserDialog1.ShowDialog();
            if (drBrowse != DialogResult.OK)
                return;

            txtFileToWatch.Text = folderBrowserDialog1.SelectedPath;
        }

        private void Form1_Leave(object sender, EventArgs e)
        {
            if (sr_LogFile != null)
                this.eNewFileLogEntry("***END: " + DateTime.Now + "***");
        }

        private void adaptGraph(FileWatched p_FileWatched, bool bInit)
        {
            try
            {
                if (bInit)
                {
                    chrtFileGraph.Series.Clear();
                    chrtFileGraph.Series.Add("<DEFAULT>");
                    chrtFileGraph.Series[0].ChartType = SeriesChartType.Line;
                    chrtFileGraph.Series[0].Color = Color.Red;
                    chrtFileGraph.Series[0].IsValueShownAsLabel = false;
                    chrtFileGraph.Series[0].IsVisibleInLegend = false;
                    chrtFileGraph.Series[0].XValueType = ChartValueType.Time;
                    chrtFileGraph.Series[0].YValueType = ChartValueType.Double;
                }

                foreach (DateTime dtKey in p_FileWatched.Values.Keys)
                {
                    DataPoint pNewPoint = new DataPoint();
                    string strTime = dtKey.Hour + ":" + dtKey.Minute + ":" + dtKey.Second;
                    pNewPoint.SetValueXY(strTime, p_FileWatched.Values[dtKey]);
                    if ( !chrtFileGraph.Series[0].Points.Contains(pNewPoint) )
                        chrtFileGraph.Series[0].Points.Add(pNewPoint);
                }

                chrtFileGraph.Series[0].Name = p_FileWatched.Filename;
                p_strCurFileShown = p_FileWatched.Fullname;
            }
            catch (Exception ex)
            {
                logException(ex.Message);
            }
        }

        private void lstViewLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lstViewLog.SelectedItems.Count > 0)
                {
                    this.Width = 985;
                    string strFile = lstViewLog.SelectedItems[0].SubItems[2].Text.Replace("...", "");
                    strFile = strFile.Replace("\\", "");
                    int iIndex = m_lFiles.FindIndex(x => x.Filename == strFile);
                    if (iIndex > -1)
                        adaptGraph(m_lFiles[iIndex], true);
                    else
                        this.Width = 570;
                }
                else
                    this.Width = 570;
            }
            catch (Exception ex)
            {
                logException(ex.Message);
            }
        }

        private string getFormattedDateTime()
        {
            string strDate = "";
            string strTime = "";

            try
            {
                strDate = (DateTime.Now.Date.Day.ToString().Length == 1 ? "0" + DateTime.Now.Date.Day.ToString() : DateTime.Now.Date.Day.ToString())
                  + "." + (DateTime.Now.Date.Month.ToString().Length == 1 ? "0" + DateTime.Now.Date.Month.ToString() : DateTime.Now.Date.Month.ToString())
                   + "." + DateTime.Now.Date.Year.ToString();
                strTime = (DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString())
                  + ":" + (DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString())
                  + ":" + (DateTime.Now.Second.ToString().Length == 1 ? "0" + DateTime.Now.Second.ToString() : DateTime.Now.Second.ToString());
            }
            catch (Exception ex)
            {
                logException(ex.Message);
            }

            return (strDate + " " + strTime);
        }

        private string convertSize(double dSize)
        {
            string strConvertedSize = dSize.ToString();

            try
            {
                string strSizeType = "Byte";
                double dNewSize = dSize > 1024 ? dSize / 1024 : dSize;
                if (dSize != dNewSize)
                {
                    strSizeType = "KByte";
                    dSize = dNewSize;
                }
                dNewSize = dNewSize > 1024 ? dNewSize / 1024 : dNewSize;
                if (dSize != dNewSize)
                {
                    strSizeType = "MByte";
                    dSize = dNewSize;
                }
                dNewSize = dNewSize > 1024 ? dNewSize / 1024 : dNewSize;
                if (dSize != dNewSize)
                {
                    strSizeType = "GByte";
                    dSize = dNewSize;
                }
                strConvertedSize = dNewSize + " " + strSizeType;
            }
            catch (Exception ex)
            {
                logException(ex.Message);
            }

            return (strConvertedSize);
        }

        private void logException(string strExMessage)
        {
            try
            {
                ListViewItem lstItem = lstViewLog.Items.Add(DateTime.Now.ToString());
                lstItem.SubItems.Add("ERROR");
                lstItem.SubItems.Add("<NONE>");
                lstItem.SubItems.Add(strExMessage);

                eNewFileLogEntry(strExMessage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
