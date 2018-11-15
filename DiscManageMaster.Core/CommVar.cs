using System;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using System.Windows.Forms;
using DiscManageMaster.Core.Classes;
using System.Drawing.IconLib;

namespace DiscManageMaster.Core
{
    public static class CommVar
    {
        public static readonly MultiIcon mi = new MultiIcon();
        public static readonly SystemImageList sil_l = new SystemImageList(SystemImageListSize.LargeIcons);
        public static readonly SystemImageList sil_s = new SystemImageList(SystemImageListSize.SmallIcons);
        public static readonly SystemImageList sil_v = new SystemImageList(SystemImageListSize.VistaIcons);
        public static readonly SystemImageList sil_x = new SystemImageList(SystemImageListSize.ExtraLargeIcons);

        public static readonly string TempFolder = Environment.GetEnvironmentVariable("TEMP") +
                                                   @"\DiscManageMaster_Temp";

        public static bool UseThread = false ;

        #region "IconIndex..."

        private static string thisApplicationPath = null;
        public static string ApplicationPath
        {
            get
            {
                if(thisApplicationPath == null)
                    thisApplicationPath = AppDomain.CurrentDomain.BaseDirectory;
                return thisApplicationPath;
            }
        }

        private static int thisOpenFolderIconIndex = -1;
        public static int OpenFolderIconIndex
        {
            get
            {
                 if (thisOpenFolderIconIndex == -1)
                     thisOpenFolderIconIndex = sil_v.IconIndex(Environment.GetFolderPath(Environment.SpecialFolder.System), true, ShellIconStateConstants.ShellIconStateOpen);
                return thisOpenFolderIconIndex;
            }
        }

        private static int thisCloseFolderIconIndex = -1;
        public static int CloseFolderIconIndex
        {
            get
            {
                if (thisCloseFolderIconIndex == -1)
                    thisCloseFolderIconIndex = sil_v.IconIndex(Environment.GetFolderPath(Environment.SpecialFolder.System), true, ShellIconStateConstants.ShellIconStateNormal);
                return thisCloseFolderIconIndex;
            }
        }

        private static int thisCollectionIconIndex = -1;
        public static int CollectionIconIndex
        {
            get
            {
                if (thisCollectionIconIndex == -1)
                    thisCollectionIconIndex = sil_v.IconIndex(Environment.GetFolderPath(Environment.SpecialFolder.Favorites), true, ShellIconStateConstants.ShellIconStateNormal);
                return thisCollectionIconIndex;
            }
        }

        private static int thisWelcomeIconIndex = -1;
        public static int WelcomeIconIndex
        {
            get
            {
                if (thisWelcomeIconIndex == -1)
                {
                    FileStream fs = new FileStream(CommVar.TempFolder + @"\Welcome.ico", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                    fs.Write(Properties.Resources.Welcome, 0, Properties.Resources.Welcome.Length);
                    fs.Close();
                    fs.Dispose();
                    System.Threading.Thread.Sleep(10);
                    thisWelcomeIconIndex = sil_v.IconIndex(CommVar.TempFolder + @"\Welcome.ico", true, ShellIconStateConstants.ShellIconStateNormal);
                }
                return thisWelcomeIconIndex;
            }
        }

        private static int thisMyCollectionsIconIndex = -1;
        public static int MyCollectionsIconIndex
        {
            get
            {
                if (thisMyCollectionsIconIndex == -1)
                {
                    FileStream fs = new FileStream(CommVar.TempFolder + @"\MyCollections.ico", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                    fs.Write(Properties.Resources.MyCollections, 0, Properties.Resources.MyCollections.Length);
                    fs.Close();
                    fs.Dispose();
                    System.Threading.Thread.Sleep(10);
                    thisMyCollectionsIconIndex = sil_v.IconIndex(CommVar.TempFolder + @"\MyCollections.ico", true, ShellIconStateConstants.ShellIconStateNormal);
                }
                return thisMyCollectionsIconIndex;
            }
        }

        private static int thisRemovableIconIndex = -1;
        public static int RemovableIconIndex
        {
            get
            {
                if (thisRemovableIconIndex == -1)
                {
                    FileStream fs = new FileStream(CommVar.TempFolder + @"\Removable.ico", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                    fs.Write(Properties.Resources.Removable, 0, Properties.Resources.Removable.Length);
                    fs.Close();
                    fs.Dispose();
                    System.Threading.Thread.Sleep(10);
                    thisRemovableIconIndex = sil_v.IconIndex(CommVar.TempFolder + @"\Removable.ico", true, ShellIconStateConstants.ShellIconStateNormal);
                }
                return thisRemovableIconIndex;
            }
        }

        private static int thisDiscIconIndex = -1;
        public static int DiscIconIndex
        {
            get
            {
                if (thisDiscIconIndex == -1)
                {
                    FileStream fs = new FileStream(CommVar.TempFolder + @"\Disc.ico", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                    fs.Write(Properties.Resources.Disc, 0, Properties.Resources.Disc.Length);
                    fs.Close();
                    fs.Dispose();
                    System.Threading.Thread.Sleep(10);
                    thisDiscIconIndex = sil_v.IconIndex(CommVar.TempFolder + @"\Disc.ico", true, ShellIconStateConstants.ShellIconStateNormal);
                }
                return thisDiscIconIndex;
            }
        }

        private static int thisLocalFolderIconIndex = -1;
        public static int LocalFolderIconIndex
        {
            get
            {
                if (thisLocalFolderIconIndex == -1)
                {
                    FileStream fs = new FileStream(CommVar.TempFolder + @"\LocalFolder.ico", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                    fs.Write(Properties.Resources.LocalFolder, 0, Properties.Resources.LocalFolder.Length);
                    fs.Close();
                    fs.Dispose();
                    System.Threading.Thread.Sleep(10);
                    thisLocalFolderIconIndex = sil_v.IconIndex(CommVar.TempFolder + @"\LocalFolder.ico", true, ShellIconStateConstants.ShellIconStateNormal);
                }
                return thisLocalFolderIconIndex;
            }
        }

        private static int thisSearchResultIconIndex = -1;
        public static int SearchResultIconIndex
        {
            get
            {
                if (thisSearchResultIconIndex == -1)
                {
                    FileStream fs = new FileStream(CommVar.TempFolder + @"\SearchResult.ico", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                    fs.Write(Properties.Resources.SearchResult, 0, Properties.Resources.SearchResult.Length);
                    fs.Close();
                    fs.Dispose();
                    System.Threading.Thread.Sleep(10);
                    thisSearchResultIconIndex = sil_v.IconIndex(CommVar.TempFolder + @"\SearchResult.ico", true, ShellIconStateConstants.ShellIconStateNormal);
                }
                return thisSearchResultIconIndex;
            }
        }

        private static int thisOptionsIconIndex = -1;
        public static int OptionsIconIndex
        {
            get
            {
                if (thisOptionsIconIndex == -1)
                {
                    FileStream fs = new FileStream(CommVar.TempFolder + @"\Options.ico", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                    fs.Write(Properties.Resources.Options, 0, Properties.Resources.Options.Length);
                    fs.Close();
                    fs.Dispose();
                    System.Threading.Thread.Sleep(10);
                    thisOptionsIconIndex = sil_v.IconIndex(CommVar.TempFolder + @"\Options.ico", true, ShellIconStateConstants.ShellIconStateNormal);
                }
                return thisOptionsIconIndex;
            }
        }

        private static int thisHelpIconIndex = -1;
        public static int HelpIconIndex
        {
            get
            {
                if (thisHelpIconIndex == -1)
                {
                    FileStream fs = new FileStream(CommVar.TempFolder + @"\Help.ico", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                    fs.Write(Properties.Resources.Help, 0, Properties.Resources.Help.Length);
                    fs.Close();
                    fs.Dispose();
                    System.Threading.Thread.Sleep(10);
                    thisHelpIconIndex = sil_v.IconIndex(CommVar.TempFolder + @"\Help.ico", true, ShellIconStateConstants.ShellIconStateNormal);
                }
                return thisHelpIconIndex;
            }
        }

        private static int thisAboutIconIndex = -1;
        public static int AboutIconIndex
        {
            get
            {
                if (thisAboutIconIndex == -1)
                {
                    FileStream fs = new FileStream(CommVar.TempFolder + @"\About.ico", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                    fs.Write(Properties.Resources.About, 0, Properties.Resources.About.Length);
                    fs.Close();
                    fs.Dispose();
                    System.Threading.Thread.Sleep(10);
                    thisAboutIconIndex = sil_v.IconIndex(CommVar.TempFolder + @"\About.ico", true, ShellIconStateConstants.ShellIconStateNormal);
                }
                return thisAboutIconIndex;
            }
        }

        public static int HtmIconIndex = sil_l.IconIndex(".htm");
        public static int HtmlIconIndex = sil_l.IconIndex(".html");
        public static int MhtIconIndex = sil_l.IconIndex(".mht");
        public static int XmlIconIndex = sil_l.IconIndex(".xml");

        #endregion

        public static bool IsVista
        {
            get { return Environment.OSVersion.Version.Major >= 6 ? true : false; }
        }

        public static bool IsSingleProcess()
        {
            Process[] MyProcesses = Process.GetProcesses();
            int i = 0;
            foreach (Process MyProcess in MyProcesses)
            {
                if (MyProcess.ProcessName == "DiscManageMaster")
                    i++;
                if (i == 2)
                { 
                    MessageBox.Show("Personal Files Manager is already running.", "Personal Files Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            return true;
        }

        #region "Plugins Code..."

        public static readonly string Plugins_Path = AppDomain.CurrentDomain.BaseDirectory + "Plugins";

        [Serializable]
        public enum PluginsType
        {
            Extract,
            Detail,
            Text,
            Image
        }

        [Serializable]
        public struct Plugins
        {
            public Plugins(string na, string des, MethodInfo p_mi, PluginsType p_pt)
            {
                name = na;
                description = des;
                mi = p_mi;
                pt = p_pt;
            }

            public MethodInfo mi;
            public PluginsType pt;
            public string name;
            public string description;
        }

        public static PluginsType GetPluginsType(string str)
        {
            switch(str.Trim().ToLower())
            {
                case "extract":
                    return PluginsType.Extract;
                case "detail":
                    return PluginsType.Detail;
                case "text":
                    return PluginsType.Text;
                case "image":
                    return PluginsType.Image;
            }
            return PluginsType.Detail;
        }

        #endregion

        public enum FolderType
        {
            NormalFolder,
            ExtractFolder,
            LocalFolder,
            RemoveableFolder,
            DiscFolder
        }

        public enum NewFolderType
        {
            CD = 1,
            Removable = 2,
            LocalFolder = 3
        }
    }

    [Serializable]
    public class RegData
    {
        private DateTime RegDate;
        private int reg1;
        private int reg2;
        private int reg3;
        private int reg4;
        private string code;
        private string username;

        public RegData(DateTime a, int b, int c, int d, int e, string f, string g)
        {
            RegDate = a;
            reg1 = b;
            reg2 = c;
            reg3 = d;
            reg4 = e;
            code = f;
            username = g;
        }

        public bool Right1(DateTime date)
        {
            return RegDate == date ? true : false;
        }

        public bool Right2(string str)
        {
            return code == str ? true : false;
        }

        public bool GetReg(string a, string b, string c, string d)
        {
            return code == Functions.MD5(a.Substring(reg1, 16) + b.Substring(reg2, 16) + c.Substring(reg3, 16) + d.Substring(reg4, 16)) ? true : false;
        }

        public bool GetName(string a)
        {
            return username == a ? true : false;
        }

        public static void Save(RegData reg)
        {
            using (FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "reg.key", FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, reg);
            }
        }

        public static RegData Load()
        {
            using (FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "reg.key", FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (RegData)formatter.Deserialize(fs);
            }
        }
    }
}