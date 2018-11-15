using System;
using System.IO;
using DiscManageMaster.Core;
using DiscManageMaster.Core.Classes;
using ICSharpCode.SharpZipLib.Zip;

namespace DiscManageMaster.Plugins.ZIP
{
    public static class ZIP
    {
        public static CommVar.PluginsType Type = CommVar.PluginsType.Extract;

        public static readonly string FileExt = "ZIP";

        public static readonly string Description = "Extract content from ZIP files.";

        public static CFolder GetFolder(string FileName)
        {
            if (FileName.Substring(FileName.Length - 4, 4).ToLower() != ".zip")
                throw new Exception("File type did not match this Plugins!");

            CFolder zipfolder = new CFolder();
            ZipFile zip = new ZipFile(FileName);
            long size = 0;
            zipfolder.Name = FileName.Substring(FileName.LastIndexOf(@"\") + 1);
            zipfolder.CreatedTime = new FileInfo(FileName).CreationTimeUtc;

            try
            {
                for (int i = 0; i < zip.Count; i++)
                {
                    if (zip[i].IsFile)
                    {
                        size += zip[i].Size;
                        FillFolder(zip[i].Name.Split('/'), zipfolder, zip[i], true);
                    }
                    else
                    {
                        string[] names = zip[i].Name.Split('/');
                        Array.Resize(ref names, names.Length - 1);
                        FillFolder(names, zipfolder, zip[i], false);
                    }
                }
            }
            catch{}

            zipfolder.Size = size;
            zipfolder.SpecialItem = true;

            return zipfolder;
        }

        private static void FillFolder(string[] TempStr, CFolder zipfolder, ZipEntry ZE, bool isfile)
        {
            string[] temp = new string[1];
            Array.Copy(TempStr, 0, temp, 0, 1);
            int i = 0;

            while (temp.Length != TempStr.Length)
            {
                if (!zipfolder.Folders.Contains(temp[i]))
                {
                    zipfolder = zipfolder.Folders[zipfolder.Folders.Add(temp[i])];
                    zipfolder.CreatedTime = ZE.DateTime;
                }
                else
                    zipfolder = zipfolder.Folders[temp[i]];

                i++;
                Array.Resize(ref temp, temp.Length + 1);
                Array.Copy(TempStr, i, temp, i, 1);
            }

            string str = temp[temp.Length - 1];

            if (isfile)
            {
                zipfolder.Files.Add(str);
                zipfolder.Files[zipfolder.Files.Count - 1].Size = ZE.Size;
                zipfolder.Files[zipfolder.Files.Count - 1].LastModifiedTime = ZE.DateTime;
                zipfolder.Files[zipfolder.Files.Count - 1].Extension = "." +
                                                                       (str.IndexOf('.') != -1 ? str.Split('.')[str.Split('.').Length - 1] : str);
            }
            else
            {
                if (!zipfolder.Folders.Contains(str))
                    zipfolder.Folders[zipfolder.Folders.Add(str)].CreatedTime = ZE.DateTime;
                else
                    zipfolder.Folders[str].CreatedTime = ZE.DateTime;
            }
        }
    }
}