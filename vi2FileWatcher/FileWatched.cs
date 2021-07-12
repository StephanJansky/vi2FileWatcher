using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vi2FileWatcher
{
    public class FileWatched
    {
        private string strFileName = "";
        private string strFilePath = "";
        private string strFullname = "";
        private Dictionary<DateTime, double> dValues = new Dictionary<DateTime, double>();
        private eType TypeOfElement = eType.UNKNOWN;

        public enum eType
        {
            Directory,
            File,
            UNKNOWN
        }

        public FileWatched(string strFile)
        {
            FileAttributes attr = File.GetAttributes(strFile);
            if ((attr & FileAttributes.Directory) != FileAttributes.Directory)
            {
                FileInfo fsInfo = new FileInfo(strFile);
                dValues.Add(DateTime.Now, fsInfo.Length);
                TypeOfElement = eType.File;
            }
            else
            {
                TypeOfElement = eType.Directory;
                return;
            }

            strFullname = strFile;
            strFileName = strFile.Substring(strFile.LastIndexOf("\\") + 1);
            strFilePath = strFile.Substring(0, strFile.LastIndexOf("\\")+1);
        }

        public void AddValue()
        {
            FileInfo fsInfo = new FileInfo(Path.Combine(strFilePath, strFileName));
            if ( !dValues.ContainsKey(DateTime.Now) )
                dValues.Add(DateTime.Now, fsInfo.Length);
        }

        public Dictionary<DateTime, double> Values
        {
            get { return (dValues); }
            set { dValues = value; }
        }

        public string Filename
        {
            get { return (strFileName); }
        }

        public string Filepath
        {
            get { return (strFilePath); }
        }

        public double LastValue
        {
            get { return (dValues.Last().Value); }
        }

        public string Fullname
        { get { return (strFullname); } }
    }
}
