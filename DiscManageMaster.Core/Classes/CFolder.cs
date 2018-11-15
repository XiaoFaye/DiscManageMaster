//----------------------------------------Code File Info-----------------------------------------//
//------------Project:     DiscManageMaster--------------------------------------------------//
//---------Company:     US-SOFT Technology(GZ) Company Limited----------------------//
//---------Copyright:     Copyright (C) 2007-2008 US-SOFT Inc. All Rights Reserved-----//
//-------------Author:    James Yang-----------------------------------------------------------//
//---------File Name:    CFolder.cs-------------------------------------------------------------//
//-------Description:    Internal Folder Class--------------------------------------------------//
//-----Last Updated:    2008-05-21   8:15PM-------------------------------------------------//

using System;
using System.IO;
using System.Runtime.InteropServices;
using DiscManageMaster.Core.Interface;
using System.Drawing.IconLib;

namespace DiscManageMaster.Core.Classes
{
    [Serializable]
    public class CFolder : IObject
    {
        private CFileCollection thisFiles = new CFileCollection();
        private CFolderCollection thisFolders = new CFolderCollection();

        private int thisIconIndex = -1;
        public string AssociateIconExtension;

        private CommVar.FolderType thisFolderType = CommVar.FolderType.NormalFolder;
        public string thisMemo = "";
        private string thisPath;
        private byte[] IconData;

        public CFolder()
        {
        }

        public CFolder(string name)
        {
            Name = name;
        }

        public CFolder(DirectoryInfo di)
        {
            Name = di.Name;
            CreatedTime = di.CreationTimeUtc;
        }

        public CommVar.FolderType FolderType
        {
            get { return thisFolderType; }
            set { thisFolderType = value; }
        }

        public new int IconIndex
        {
            get
            {
                if (AssociateIconExtension != null)
                    return Functions.GetIconIndex(AssociateIconExtension);
                if (thisIconIndex == -1 || thisIconIndex == 0)
                {
                    try
                    {
                        if (IconData != null)
                        {
                            string filepath = CommVar.TempFolder + @"\" + Functions.GetRanNumber() + ".ico";
                            FileStream fs =
                                new FileStream(filepath,
                                               FileMode.Create,
                                               FileAccess.ReadWrite,
                                               FileShare.ReadWrite);

                            fs.Write(IconData, 0, IconData.Length);
                            fs.Close();
                            fs.Dispose();

                            thisIconIndex = CommVar.sil_l.IconIndex(filepath, true);
                        }
                        else
                            thisIconIndex = CommVar.CloseFolderIconIndex;
                    }
                    catch{}
                }

                return thisIconIndex;
            }
            set
            {
                if (value != thisIconIndex && value != -1 && value != CommVar.CloseFolderIconIndex && IconData == null)
                {
                    CommVar.mi.Clear();
                    SingleIcon singleIcon = CommVar.mi.Add("Untitled");
                    if (CommVar.IsVista)
                        singleIcon.Add(CommVar.sil_v.Icon(value));
                    singleIcon.Add(CommVar.sil_x.Icon(value));
                    singleIcon.Add(CommVar.sil_l.Icon(value));
                    singleIcon.Add(CommVar.sil_s.Icon(value));

                    CommVar.mi.SelectedIndex = 0;
                    CommVar.mi.Save(CommVar.TempFolder + @"\temp.ico", MultiIconFormat.ICO);
                    FileStream fs =
                        new FileStream(CommVar.TempFolder + @"\temp.ico", FileMode.Open,
                                       FileAccess.Read, FileShare.None);

                    byte[] fbs = new byte[fs.Length];
                    fs.Read(fbs, 0, (int) fs.Length);
                    fs.Close();
                    fs.Dispose();
                    IconData = fbs;
                    File.Delete(CommVar.TempFolder + @"\temp.ico");
                }

                thisIconIndex = value;
            }
        }

        public CFileCollection Files
        {
            get { return thisFiles; }
            set { thisFiles = value; }
        }

        public CFolderCollection Folders
        {
            get { return thisFolders; }
            set { thisFolders = value; }
        }

        private int thisFolderCount = -1;
        public int FolderCount
        {
            get
            {
                if (thisFolderCount == -1)
                {
                    int temp = 0;
                    for (int i = 0; i < thisFolders.Count; i++)
                    {
                        if (thisFolders[i].SpecialItem) continue;
                        temp += thisFolders[i].FolderCount;
                        temp++;
                    }
                    thisFolderCount = temp;
                }
                return thisFolderCount;
            }
        }

        private int thisFileCount = -1;
        public int FileCount
        {
            get
            {
                if (thisFileCount == -1)
                {
                    int temp = 0;
                    for (int i = 0; i < thisFolders.Count; i++)
                    {
                        if (thisFolders[i].SpecialItem) continue;
                        temp += thisFolders[i].FileCount;
                    }
                    temp += thisFiles.Count;
                    thisFileCount = temp;
                }
                return thisFileCount;
            }
        }

        private long thisSize = -1;
        public long Size
        {
            get 
            {
                if (thisSize > 0)
                    return thisSize;
                else
                {
                    thisSize = Functions.GetFolderSize(this);
                    return thisSize;
                }
            }
            set
            {
                thisSize = value;
            }
        }

        public string Memo
        {
            get { return thisMemo;}
            set { thisMemo = value; }
        }

        public string Path
        {
            get { return thisPath; }
            set { thisPath = value; }
        }
    }
}