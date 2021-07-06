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

namespace vi2FileWatcher
{
    public partial class Form1 : Form
    {
        private FileSystemWatcher fs_Watcher = null;
        public delegate void dWatcherNotificationEvent(WatcherChangeTypes eChangeType, string strFile);
        public event dWatcherNotificationEvent eWatcherNotificationEvent;
        public delegate void dNewLogEntry(string strLogEntry);
        public event dNewLogEntry eNewLogEntry;
        public delegate void dNewFileLogEntry(string strLogEntry);
        public event dNewFileLogEntry eNewFileLogEntry;
        public StreamWriter sr_LogFile = null;

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

        private void Form1_eNewLogEntry(string strLogEntry)
        {
            if (lstViewLog.InvokeRequired)
            {
                lstViewLog.Invoke(new MethodInvoker(() => { Form1_eNewLogEntry(strLogEntry); }));
                return;
            }

            lstViewLog.Items.Add(DateTime.Now.Date.Day + "." + DateTime.Now.Date.Month + "." + DateTime.Now.Date.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + " - " + strLogEntry);
            if ( lstViewLog.Items.Count > 1 )
                lstViewLog.Items[lstViewLog.Items.Count - 1].EnsureVisible();
        }

        private void Form1_eWatcherNotificationEvent(WatcherChangeTypes eChangeType, string strFile)
        {
            string strMessage = "";
            strMessage += eChangeType.ToString() + " - " + strFile;

            if ( eChangeType != WatcherChangeTypes.Deleted )
            {
                FileAttributes attr = File.GetAttributes(strFile);
                if ((attr & FileAttributes.Directory) != FileAttributes.Directory)
                {
                    FileInfo fsInfo = new FileInfo(strFile);
                    strMessage += " (" + (fsInfo.Length > 1024 ? fsInfo.Length / 1024 + " KByte" : fsInfo.Length + " Byte") + ")";
                }
                else
                    return;
            }

            if (strMessage != p_strLastMessage)
            {
                this.eNewLogEntry(strMessage);
                this.eNewFileLogEntry(strMessage);
            }

            p_strLastMessage = strMessage;
        }

        private void Fs_Watcher_Error(object sender, ErrorEventArgs e)
        {
            this.eWatcherNotificationEvent(WatcherChangeTypes.All, e.GetException().Message);
        }

        private void Fs_Watcher_Created(object sender, FileSystemEventArgs e)
        {
            this.eWatcherNotificationEvent(e.ChangeType, e.FullPath);
        }

        private void Fs_Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            this.eWatcherNotificationEvent(e.ChangeType, e.FullPath);
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
                    this.eNewLogEntry("Folder to watch is EMPTY!");
                    return;
                }

                this.eNewFileLogEntry("***Start: " + DateTime.Now + "***");
                if (chkInclSubDirs.Checked)
                {
                    this.eNewFileLogEntry("Watching Directory and Subdirectories with Filter (" + txtFilter.Text + "): " + txtFileToWatch.Text);
                    this.eNewLogEntry("Watching Directory and Subdirectories with Filter (" + txtFilter.Text + "): " + txtFileToWatch.Text);
                }
                else
                {
                    this.eNewFileLogEntry("Watching Directory with Filter (" + txtFilter.Text + "): " + txtFileToWatch.Text);
                    this.eNewLogEntry("Watching Directory with Filter (" + txtFilter.Text + "): " + txtFileToWatch.Text);
                }

                foreach (string strFile in System.IO.Directory.GetFiles(txtFileToWatch.Text) )
                    this.eWatcherNotificationEvent(WatcherChangeTypes.Changed, strFile);

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
                this.eNewLogEntry("Stopped watching.");
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
    }
}
