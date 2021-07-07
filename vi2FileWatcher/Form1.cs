using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.eWatcherNotificationEvent += Form1_eWatcherNotificationEvent;
            this.eNewLogEntry += Form1_eNewLogEntry;
            this.eNewFileLogEntry += Form1_eNewFileLogEntry;
        }

        private void Form1_eNewFileLogEntry(string strLogEntry)
        {
            if (System.IO.File.Exists("vi2FileWatcher.log"))
                sr_LogFile = System.IO.File.AppendText("vi2FileWatcher.log");
            else
                sr_LogFile = new StreamWriter("vi2FileWatcher.log");

            sr_LogFile.WriteLine(DateTime.Now.Date.Day + "." + DateTime.Now.Date.Month + "." + DateTime.Now.Date.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + " - " + strLogEntry);
            sr_LogFile.Close();
        }

        private void Form1_eNewLogEntry(WatcherChangeTypes eChangeType, string strFile, string strLogEntry)
        {
            if (lstViewLog.InvokeRequired)
            {
                lstViewLog.Invoke(new MethodInvoker(() => { Form1_eNewLogEntry(eChangeType, strFile, strLogEntry); }));
                return;
            }

            ListViewItem lstVCurItem = lstViewLog.Items.Add(DateTime.Now.Date.Day + "." + DateTime.Now.Date.Month + "." + DateTime.Now.Date.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second);
            lstVCurItem.SubItems.Add(eChangeType.ToString());
            lstVCurItem.SubItems.Add(strFile);
            lstVCurItem.SubItems.Add(strLogEntry);
            if ( lstViewLog.Items.Count > 1 )
                lstViewLog.Items[lstViewLog.Items.Count - 1].EnsureVisible();

            lstViewLog.Refresh();
        }

        private void Form1_eWatcherNotificationEvent(WatcherChangeTypes eChangeType, string strFile)
        {
            string strMessage = "";
            string strDateTime = "";
            FileWatched NewFile = new FileWatched(strFile);

            if ( eChangeType != WatcherChangeTypes.Deleted )
            {
                FileAttributes attr = File.GetAttributes(strFile);
                if ((attr & FileAttributes.Directory) != FileAttributes.Directory)
                {
                    strDateTime = DateTime.Now.ToString();
                    if (m_lFiles.Contains(NewFile))
                        m_lFiles[m_lFiles.FindIndex(x => x.Filename == NewFile.Filename)].AddValue();
                    else
                        m_lFiles.Add(new FileWatched(strFile));
                }
                else
                    return;
            }

            int iIndex = m_lFiles.FindIndex(x => x.Filename == NewFile.Filename);
            this.eNewLogEntry(eChangeType, strFile, "New size is " + (m_lFiles[iIndex].LastValue > 1024 ? 
                                                                      (m_lFiles[iIndex].LastValue / 1024).ToString() + " Kbyte" : 
                                                                      m_lFiles[iIndex].LastValue.ToString() + " Byte"));
            this.eNewFileLogEntry(strFile + ": " + strMessage);
            // adaptGraph(m_lFiles[m_lFiles.FindIndex(x=> x.Filename == NewFile.Filename)]);

            p_strLastMessage = strMessage;
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
                chrtFileGraph.Series.Clear();
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

        private void adaptGraph(FileWatched p_FileWatched)
        {
            chrtFileGraph.Series.Clear();
            chrtFileGraph.Series.Add("<DEFAULT>");
            chrtFileGraph.Series[0].ChartType = SeriesChartType.Line;
            chrtFileGraph.Series[0].Color = Color.Red;
            chrtFileGraph.Series[0].IsValueShownAsLabel = true;
            chrtFileGraph.Series[0].XValueType = ChartValueType.Time;
            chrtFileGraph.Series[0].YValueType = ChartValueType.Double;

            DataPoint pNewPoint = new DataPoint();
            foreach (DateTime dtKey in p_FileWatched.Values.Keys)
            {
                string strTime = dtKey.Hour + ":" + dtKey.Minute + ":" + dtKey.Second;
                pNewPoint.SetValueXY(strTime, p_FileWatched.Values[dtKey] > 1024 ? p_FileWatched.Values[dtKey] / 1024 : p_FileWatched.Values[dtKey]);
                chrtFileGraph.Series[0].Points.Add(pNewPoint);
            }

            chrtFileGraph.Series[0].Name = p_FileWatched.Filename;
        }

        private void lstViewLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstViewLog.SelectedItems.Count > 0)
            {
                this.Width = 985;
                string strFile = lstViewLog.SelectedItems[0].SubItems[2].Text;
                int iIndex = m_lFiles.FindIndex(x => x.Fullname == strFile);
                if ( iIndex > -1 )
                    adaptGraph(m_lFiles[iIndex]);
            }
            else
                this.Width = 570;
        }
    }
}
