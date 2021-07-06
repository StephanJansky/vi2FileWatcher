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
        public delegate void dWatcherNotificationEvent(string strMessage);
        public event dWatcherNotificationEvent eWatcherNotificationEvent;
        public StreamWriter sr_LogFile = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.eWatcherNotificationEvent += Form1_eWatcherNotificationEvent;
        }

        private void Form1_eWatcherNotificationEvent(string strMessage)
        {
            if (lstLog.InvokeRequired)
            {
                lstLog.Invoke(new MethodInvoker(() => { Form1_eWatcherNotificationEvent(strMessage); }));
                return;
            }

            lstLog.Items.Add(strMessage);
            sr_LogFile.WriteLine(strMessage);
        }

        private void Fs_Watcher_Error(object sender, ErrorEventArgs e)
        {
            this.eWatcherNotificationEvent(DateTime.Now.TimeOfDay + " - File Error: " + e.GetException().Message);
        }

        private void Fs_Watcher_Created(object sender, FileSystemEventArgs e)
        {
            FileInfo fsInfo = new FileInfo(e.FullPath);
            this.eWatcherNotificationEvent(DateTime.Now.TimeOfDay + " - " + e.FullPath + " was CREATED!");
            this.eWatcherNotificationEvent(DateTime.Now.TimeOfDay + " - " + e.FullPath + ": " + (fsInfo.Length > 1024 ? fsInfo.Length / 1024 + " KByte" : fsInfo.Length + " Byte"));
        }

        private void Fs_Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            FileInfo fsInfo = new FileInfo(e.FullPath);
            this.eWatcherNotificationEvent(DateTime.Now.TimeOfDay + " - " + e.FullPath + " was DELETED!");
            this.eWatcherNotificationEvent(DateTime.Now.TimeOfDay + " - " + e.FullPath + ": " + (fsInfo.Length > 1024 ? fsInfo.Length / 1024 + " KByte" : fsInfo.Length + " Byte"));
        }

        private void Fs_Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            FileInfo fsInfo = new FileInfo(e.FullPath);
            this.eWatcherNotificationEvent(DateTime.Now.TimeOfDay + " - " + e.FullPath + " was RENAMED!");
            this.eWatcherNotificationEvent(DateTime.Now.TimeOfDay + " - " + e.FullPath + ": " + (fsInfo.Length > 1024 ? fsInfo.Length / 1024 + " KByte" : fsInfo.Length + " Byte"));
        }

        private void Fs_Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            FileInfo fsInfo = new FileInfo(e.FullPath);
            this.eWatcherNotificationEvent(DateTime.Now.TimeOfDay + " - " + e.FullPath + " was CHANGED!");
            this.eWatcherNotificationEvent(DateTime.Now.TimeOfDay + " - " + e.FullPath + ": " + (fsInfo.Length > 1024 ? fsInfo.Length / 1024 + " KByte" : fsInfo.Length + " Byte"));
        }

        private void btnWatch_Click(object sender, EventArgs e)
        {
            if (btnWatch.Text == "Watch")
            {
                if (String.IsNullOrEmpty(txtFileToWatch.Text))
                {
                    lstLog.Items.Add("File to watch is EMPTY!");
                    return;
                }
                sr_LogFile = new StreamWriter("vi2FileWatcher.log");
                sr_LogFile.WriteLine("***Start: " + DateTime.Now + "***");

                foreach (string strFile in System.IO.Directory.GetFiles(txtFileToWatch.Text) )
                {
                    FileInfo fsInfo = new FileInfo(strFile);
                    this.eWatcherNotificationEvent(strFile + ": " + (fsInfo.Length > 1024 ? fsInfo.Length / 1024 + " KByte" : fsInfo.Length + " Byte"));
                    lstLog.Items.Add(strFile + ": " + (fsInfo.Length > 1024 ? fsInfo.Length / 1024 + " KByte" : fsInfo.Length + " Byte"));
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

                if (chkInclSubDirs.Checked)
                {
                    this.eWatcherNotificationEvent("Watching Directory and Subdirectories with Filter (" + txtFilter.Text + "): " + txtFileToWatch.Text);
                    lstLog.Items.Add("Watching Directory and Subdirectories with Filter (" + txtFilter.Text + "): " + txtFileToWatch.Text);
                }
                else
                {
                    this.eWatcherNotificationEvent("Watching Directory with Filter (" + txtFilter.Text + "): " + txtFileToWatch.Text);
                    lstLog.Items.Add("Watching Directory with Filter (" + txtFilter.Text + "): " + txtFileToWatch.Text);
                }
                btnWatch.Text = "STOP";
            }
            else
            {
                fs_Watcher.Dispose();
                fs_Watcher = null;
                btnWatch.Text = "Watch";
                lstLog.Items.Add("Stopped watching...");
                sr_LogFile.WriteLine("***END: " + DateTime.Now + "***");
                sr_LogFile.Close();
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

        private void btnWatch_Leave(object sender, EventArgs e)
        {
            if ( sr_LogFile != null )
                sr_LogFile.WriteLine("***END: " + DateTime.Now + "***");
        }
    }
}
