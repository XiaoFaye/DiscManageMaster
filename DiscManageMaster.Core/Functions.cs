using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DiscManageMaster.Core.Classes;
using System.Security.Cryptography;

namespace DiscManageMaster.Core
{
    public static class Functions
    {
        public static CFolder FolderInfo2CFolder(DirectoryInfo di)
        {
            CFolder cf = new CFolder(di);

            foreach(FileInfo fi in di.GetFiles())
            {
                CFile cfile = new CFile(fi);
                cf.Size += cfile.Size;
                cf.Files.Add(cfile);
            }

            foreach (DirectoryInfo d in di.GetDirectories())
            {
                cf.Size += cf.Folders.Add2(FolderInfo2CFolder(d));
            }

            return cf;
        }

        public delegate void FileAdded(object sender, FileAddedEventArgs e);
        public static event FileAdded OnFileAdded;

        public delegate void FolderAdded(object sender, FolderAddedEventArgs e);
        public static event FolderAdded OnFolderAdded;

        public static void FolderInfo2CFolder(CFileCollection files, CFolderCollection folders, DirectoryInfo di, CommVar.Plugins[] Plugins, bool getpath)
        {
            foreach (DirectoryInfo d in di.GetDirectories())
            {
                try
                {
                    if (!IsNormalFileSystem(d.Attributes)) continue;
                    CFolder cf = new CFolder();
                    cf.Name = d.Name;
                    cf.CreatedTime = d.CreationTimeUtc;
                    FolderInfo2CFolder(cf.Files, cf.Folders, d, Plugins, getpath);
                    folders.Add(cf);
                    OnFolderAdded(null, new FolderAddedEventArgs(cf));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("A___" + ex.Message);
                }
            }

            foreach (FileInfo fi in di.GetFiles())
            {
                if (!IsNormalFileSystem(fi.Attributes)) continue;

                try
                {
                    CFile cfile = new CFile(fi);
                    if (getpath)
                        cfile.Path = fi.FullName;
                    files.Add(cfile);
                    OnFileAdded(null, new FileAddedEventArgs(cfile));

                    bool setplugins = false;
                    for (int i = 0; i < Plugins.Length; i++)
                    {
                        try
                        {
                            switch (Plugins[i].pt)
                            {
                                case CommVar.PluginsType.Extract:
                                    {
                                        CFolder temp =
                                            (CFolder)Plugins[i].mi.Invoke(null, new object[] { fi.FullName });
                                        temp.AssociateIconExtension = cfile.Extension;
                                        temp.Size = cfile.Size;
                                        temp.FolderType = CommVar.FolderType.ExtractFolder;
                                        temp.SpecialItem = true;
                                        cfile.SpecialItem = true;
                                        folders.Add(temp);
                                        setplugins = true;
                                    }
                                    break;
                            }

                            if (setplugins)
                                break;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("B___" + ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("C___" + ex.Message);
                }
            }
        }

        public static bool IsNormalFileSystem(FileAttributes fa)
        {
            if ((fa & FileAttributes.Hidden) == FileAttributes.Hidden && (fa & FileAttributes.System) == FileAttributes.System)
                return false;
            if ((fa & FileAttributes.Hidden) == FileAttributes.Hidden && (fa & FileAttributes.NotContentIndexed) == FileAttributes.NotContentIndexed)
                return false;
            return true;
        }

        public static void FillTreeView(CFolder cf, TreeNodeCollection tnc)
        {
            foreach (CFolder c1 in cf.Folders)
            {
                FillTreeView(c1, tnc.Add(c1.Name,c1.Name,c1.IconIndex,c1.IconIndex).Nodes);
            }

            foreach (CFile c2 in cf.Files)
            {
                tnc.Add(c2.Name, c2.Name, c2.IconIndex,c2.IconIndex);
            }
        }

        public static void FillTreeView(CFolderCollection folders, TreeNodeCollection tnc)
        {
            for (int i = 0; i < folders.Count; i++)
            {
                int selectfoldericonindex = folders[i].IconIndex == CommVar.CloseFolderIconIndex ? CommVar.OpenFolderIconIndex : folders[i].IconIndex;
                FillTreeView(folders[i].Folders, tnc.Add(folders[i].Name, folders[i].Name, folders[i].IconIndex, selectfoldericonindex).Nodes);
            }
        }

        public static TreeNode GetNode(CRoot root, TreeNodeCollection tnc)
        {
            return tnc[tnc.Count];
        }

        public static string GetRanNumber()
        {
            Random ran = new Random(DateTime.Now.Millisecond);
            return
                ran.Next(1, 1000).ToString() + ran.Next(1, 1000).ToString() + ran.Next(1, 1000).ToString() +
                ran.Next(1, 1000).ToString();
        }

        public static CFolder GetSelectedFolder(CRoot root, TreeNode tn)
        {
            ArrayList index = new ArrayList();
            while (tn != null)
            {
                index.Add(tn.Index);
                tn = tn.Parent;
            }

            index.Reverse();

            CFolder ccf = root.Collection[int.Parse(index[1].ToString())][int.Parse(index[2].ToString())];
            index.RemoveAt(0);
            index.RemoveAt(0);
            index.RemoveAt(0);

            for (int j = 0; j < index.Count; j++)
                ccf = ccf.Folders[(int)index[j]];

            return ccf;
        }

        public static object GetSelectedItem(CRoot root, ListViewItem lvi, bool isfile)
        {
            string[] temp = lvi.Name.Split('|');
            ArrayList index = new ArrayList();
            for (int i = 1; i < temp.Length; i++)
                index.Add(int.Parse(temp[i]));

            int index1 = (int)index[1];
            int index2 = (int)index[2];

            CFolder ccf = root.Collection[index1][index2];
            index.RemoveAt(0);
            index.RemoveAt(0);
            index.RemoveAt(0);

            for (int j = 0; j < index.Count; j++)
            {
                if (j == index.Count - 1)
                {
                    if (isfile)
                        return ccf.Files[(int) index[j]];
                    else
                        return ccf.Folders[(int) index[j]];
                }
                ccf = ccf.Folders[(int)index[j]];
            }

            return ccf;
        }

        public static int GetIconIndex(string Extension)
        {
            const int FILE_ATTRIBUTE_NORMAL = 0x80;
            SystemImageList.SHGetFileInfoConstants dwFlags =
                SystemImageList.SHGetFileInfoConstants.SHGFI_USEFILEATTRIBUTES |
                SystemImageList.SHGetFileInfoConstants.SHGFI_SYSICONINDEX |
                SystemImageList.SHGetFileInfoConstants.SHGFI_SMALLICON;
            SystemImageList.SHFILEINFO shfi = new SystemImageList.SHFILEINFO();
            uint shfiSize = (uint) Marshal.SizeOf(shfi.GetType());

            SystemImageList.SHGetFileInfo(
                Extension,
                FILE_ATTRIBUTE_NORMAL,
                ref shfi,
                shfiSize,
                (uint) dwFlags);

            return shfi.iIcon;
        }

        public static DriveInfo[] GetDrivers(DriveType dt)
        {
            ArrayList al = new ArrayList();
            foreach (DriveInfo di in DriveInfo.GetDrives())
            {
                if (di.DriveType == dt && di.IsReady)
                    al.Add(di);
            }

            return al.ToArray(typeof(DriveInfo)) as DriveInfo[];
        }

        public static CommVar.Plugins[] GetPlugins()
        {
            ArrayList al = new ArrayList();

            if (System.IO.Directory.Exists(CommVar.Plugins_Path))
                foreach (FileInfo fi in (new DirectoryInfo(CommVar.Plugins_Path)).GetFiles())
                {
                    try
                    {
                        if ((fi.Name.IndexOf("DiscManageMaster.Plugins") > -1) && (fi.Extension.ToLower() == ".dll"))
                        {
                            Type[] t = Assembly.LoadFrom(fi.FullName).GetTypes();
                            int k = 0;
                            foreach (Type temp in t)
                            {
                                if (temp.IsAbstract) break;
                                k++;
                            }
                            FieldInfo[] fis = t[k].GetFields();
                            int fii = 0;
                            int ext = 1;
                            int des = 2;

                            al.Add(new CommVar.Plugins(fis[ext].GetValue(fis[ext]).ToString(), fis[des].GetValue(fis[des]).ToString(), t[k].GetMethods()[0], CommVar.GetPluginsType(fis[fii].GetValue(fis[fii]).ToString())));
                        }
                    }
                    catch { }
                }

            return al.ToArray(typeof(CommVar.Plugins)) as CommVar.Plugins[];
        }

        public static string SizeStr(long size)
        {
            double temp = size/1024.0;

            if (temp > (1024*1024*1024))
            {
                temp = temp/(1024.0*1024.0*1024.0);
                if (temp > 100)
                    return string.Format("{0:###} TB", temp);
                else if (temp > 10)
                    return string.Format("{0:##.#} TB", temp);
                else
                    return string.Format("{0:#.##} TB", temp);
            }
            else if (temp > (1024*1024))
            {
                temp = temp/(1024.0*1024.0);
                if (temp > 100)
                    return string.Format("{0:###} GB", temp);
                else if (temp > 10)
                    return string.Format("{0:##.#} GB", temp);
                else
                    return string.Format("{0:#.##} GB", temp);
            }
            else if (temp > 1024)
            {
                temp = temp/1024.0;
                if (temp > 100)
                    return string.Format("{0:###} MB", temp);
                else if (temp > 10)
                    return string.Format("{0:##.#} MB", temp);
                else
                    return string.Format("{0:#.##} MB", temp);
            }
            else if (temp > 1)
            {
                if (temp > 100)
                    return string.Format("{0:###} KB", temp);
                else if (temp > 10)
                    return string.Format("{0:##.#} KB", temp);
                else
                    return string.Format("{0:#.##} KB", temp);
            }
            else
                return size + " Bytes";
        }

        public static bool IsRightVistaIcon(Bitmap bmp)
        {
            for (int y = 48; y < 256; y++)
                for (int x = 0; x < 256; x++)
                    if (bmp.GetPixel(x, y) != Color.FromArgb(0, 0, 0, 0))
                        return true;
            return false;
        }

        public static long GetFolderSize(CFolder folder)
        {
            long size = 0;
            for (int i = 0; i < folder.Folders.Count; i++)
                if (folder.Folders[i].SpecialItem)
                    continue;
                else
                    size += folder.Folders[i].Size;
            for (int i = 0; i < folder.Files.Count; i++)
                size += folder.Files[i].Size;

            return size;
        }

        public static void GetHelpContent(TreeNode tn)
        {
            try
            {
                string HelpPath = System.AppDomain.CurrentDomain.BaseDirectory + @"Help\Help.dat";
                StreamReader sr = File.OpenText(HelpPath);
                while (!sr.EndOfStream)
                {
                    string str = sr.ReadLine();
                    if (str.IndexOf('\\') == -1)
                        tn.Nodes.Add(str.Substring(0, str.Length - 4), str.Substring(0, str.Length - 4), CommVar.HelpIconIndex, CommVar.HelpIconIndex).Tag = System.AppDomain.CurrentDomain.BaseDirectory + @"Help\" + str;
                    else if (str.IndexOf('\\') == str.Length - 1)
                        tn.Nodes.Add(str.Substring(0, str.Length - 1), str.Substring(0, str.Length - 1), CommVar.CloseFolderIconIndex, CommVar.OpenFolderIconIndex);
                    else
                    {
                        string str2 = str.Split('\\')[1];
                        tn.Nodes[str.Split('\\')[0]].Nodes.Add(str2.Substring(0, str2.Length - 4), str2.Substring(0, str2.Length - 4), CommVar.HelpIconIndex, CommVar.HelpIconIndex).Tag = System.AppDomain.CurrentDomain.BaseDirectory + @"Help\" + str;
                    }
                }
                tn.Expand();
            }
            catch { }
        }

        public static string MD5(String str)
        {
            System.Security.Cryptography.MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.Default.GetBytes(str);
            byte[] result = md5.ComputeHash(data);
            String ret = "";
            for (int i = 0; i < result.Length; i++)
                ret += result[i].ToString("x").PadLeft(2, '0');
            return ret.ToUpper();
        }

        [DllImport("user32.dll")]
        public static extern bool MessageBeep(uint uType);

        [DllImportAttribute("ole32.dll", EntryPoint = "CoInitialize", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int CoInitialize(System.IntPtr pvReserved);
    }

    public class FileAddedEventArgs : EventArgs
    {
        public CFile File;

        public FileAddedEventArgs(CFile file)
        {
            File = file;
        }
    }

    public class FolderAddedEventArgs : EventArgs
    {
        public CFolder Folder;

        public FolderAddedEventArgs(CFolder folder)
        {
            Folder = folder;
        }
    }
}