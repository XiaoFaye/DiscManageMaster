//----------------------------------------Code File Info-----------------------------------------//
//------------Project:     DiscManageMaster--------------------------------------------------//
//---------Company:     US-SOFT Technology(GZ) Company Limited----------------------//
//---------Copyright:     Copyright (C) 2007-2008 US-SOFT Inc. All Rights Reserved-----//
//-------------Author:    James Yang-----------------------------------------------------------//
//---------File Name:    CFile.cs----------------------------------------------------------------//
//-------Description:    Internal File Class-----------------------------------------------------//
//-----Last Updated:    2008-05-21   7:45PM------------------------------------------------//

using System;
using System.IO;
using System.Runtime.InteropServices;
using DiscManageMaster.Core.Interface;
using System.Threading;
using System.Drawing.IconLib;

namespace DiscManageMaster.Core.Classes
{
    [Serializable]
    public class CFile : IObject
    {
        private const int FILE_ATTRIBUTE_NORMAL = 0x80;
        private static SystemImageList.SHGetFileInfoConstants dwFlags;
        private static uint shfiSize;
        private static SystemImageList.SHFILEINFO shfi;
        private string thisExtension = "";
        private string thisPath = "";
        private string thisType = "";
        private int thisIconIndex = -1;
        private byte[] IconData;

        private DateTime thisLastAccessTime;
        private DateTime thisLastModifiedTime;

        static CFile()
        {
            dwFlags =
                SystemImageList.SHGetFileInfoConstants.SHGFI_USEFILEATTRIBUTES |
                SystemImageList.SHGetFileInfoConstants.SHGFI_SYSICONINDEX |
                SystemImageList.SHGetFileInfoConstants.SHGFI_SMALLICON |
                SystemImageList.SHGetFileInfoConstants.SHGFI_TYPENAME;

            shfi = new SystemImageList.SHFILEINFO();
            shfiSize = (uint) Marshal.SizeOf(shfi.GetType());
        }

        public CFile(string name)
        {
            Name = name;
        }

        public CFile(FileInfo fi)
        {
            GetInfo(fi);
        }

        public string Extension
        {
            get
            {
                if (thisExtension == "")
                {
                    thisExtension = System.IO.Path.GetExtension(Name);
                }
                return thisExtension;
            }
            set { thisExtension = value; }
        }

        public string Path
        {
            get { return thisPath; }
            set { thisPath = value; }
        }

        public string Type
        {
            get
            {
                if (thisType == "")
                {
                    SystemImageList.SHGetFileInfo(
                        Extension,
                        FILE_ATTRIBUTE_NORMAL,
                        ref shfi,
                        shfiSize,
                        (uint) dwFlags);

                    thisType = shfi.szTypeName;
                }
                return thisType;
            }
            set { thisType = value; }
        }

        public DateTime LastAccessTime
        {
            get { return thisLastAccessTime; }
            set { thisLastAccessTime = value; }
        }

        public DateTime LastModifiedTime
        {
            get { return thisLastModifiedTime; }
            set { thisLastModifiedTime = value; }
        }

        public new int IconIndex
        {
            get
            {
                if (thisIconIndex == -1)
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

                            Thread.Sleep(10);
                            thisIconIndex = CommVar.sil_l.IconIndex(filepath, false);
                        }
                        else
                        {
                            //SystemImageList.SHGetFileInfo(
                            //    Extension,
                            //    FILE_ATTRIBUTE_NORMAL,
                            //    ref shfi,
                            //    shfiSize,
                            //    (uint)dwFlags);
                            thisIconIndex = Functions.GetIconIndex(Extension);//shfi.iIcon;
                        }
                    }
                    catch { }
                }

                return thisIconIndex;
            }
            set { thisIconIndex = value; }
        }

        private void GetInfo(FileInfo fi)
        {
            Name = fi.Name;
            Size = fi.Length;
            Hidden = (FileAttributes.Hidden & fi.Attributes) > 0 ? true : false;
            CreatedTime = fi.CreationTimeUtc;
            LastAccessTime = fi.LastAccessTimeUtc;
            LastModifiedTime = fi.LastWriteTimeUtc;

            if (Extension.ToLower() == ".exe" || Extension.ToLower() == ".ttf" || Extension.ToLower() == ".ico")
            {
                int index = CommVar.sil_l.IconIndex(fi.FullName, false);

                SystemImageList.SHGetFileInfo(
                    Extension,
                    FILE_ATTRIBUTE_NORMAL,
                    ref shfi,
                    shfiSize,
                    (uint) dwFlags);

                if (shfi.iIcon == index)
                    return;
                else
                    thisIconIndex = index;

                Path = fi.FullName;
                CommVar.mi.Clear();
                SingleIcon singleIcon = CommVar.mi.Add("Untitled");
                if (CommVar.IsVista)
                    singleIcon.Add(CommVar.sil_v.Icon(IconIndex));
                singleIcon.Add(CommVar.sil_x.Icon(IconIndex));
                singleIcon.Add(CommVar.sil_l.Icon(IconIndex));
                singleIcon.Add(CommVar.sil_s.Icon(IconIndex));

                CommVar.mi.SelectedIndex = 0;
                CommVar.mi.Save(CommVar.TempFolder + @"\temp.ico", MultiIconFormat.ICO);
                FileStream fs =
                    new FileStream(CommVar.TempFolder + @"\temp.ico", FileMode.Open,
                                   FileAccess.Read, FileShare.None);

                byte[] fbs = new byte[fs.Length];
                fs.Read(fbs, 0, (int)fs.Length);
                fs.Close();
                fs.Dispose();
                IconData = fbs;
                File.Delete(CommVar.TempFolder + @"\temp.ico");
                Path = "";
            }
        }
    }
}