using System;
using System.IO;
using DiscManageMaster.Core;
using DiscManageMaster.Core.Classes;
using Schematrix;

namespace DiscManageMaster.Plugins.RAR
{
    public static class RAR
    {
        public static CommVar.PluginsType Type = CommVar.PluginsType.Extract;

        public static readonly string FileExt = "RAR";

        public static readonly string Description = "Extract content from RAR files.";

        public static CFolder GetFolder(string FileName)
        {
            if (FileName.Substring(FileName.Length - 4, 4).ToLower() != ".rar")
                throw new Exception("File type did not match this Plugins!");

            char[] sp = {'\\'};
            string[] TempStrs = FileName.Split(sp);
            CFolder TempFolder = new CFolder();

            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + " (x86)"))
            {
                Unrar64 rar = new Unrar64(FileName);
                rar.Open(Unrar64.OpenMode.List);

                TempFolder.Name = TempStrs[TempStrs.Length - 1];
                Array.Clear(TempStrs, 0, TempStrs.Length);

                while (rar.ReadHeader())
                {
                    //if (!rar.CurrentFile.IsDirectory)
                    //{
                    TempStrs = rar.CurrentFile.FileName.Split(sp);
                    FillFolder(FileName, TempStrs, 0, TempFolder, rar.CurrentFile);
                    //}
                    rar.Skip();
                }

                rar.Close();
                rar = null;
            }
            else
            {
                Unrar rar = new Unrar(FileName);
                rar.Open(Unrar.OpenMode.List);

                TempFolder.Name = TempStrs[TempStrs.Length - 1];
                Array.Clear(TempStrs, 0, TempStrs.Length);

                while (rar.ReadHeader())
                {
                    //if (!rar.CurrentFile.IsDirectory)
                    //{
                    TempStrs = rar.CurrentFile.FileName.Split(sp);
                    FillFolder(FileName, TempStrs, 0,TempFolder, rar.CurrentFile);
                    //}
                    rar.Skip();
                }

                rar.Close();
                rar = null;
            }

            TempFolder.SpecialItem = true;
            TempFolder.CreatedTime = (new FileInfo(FileName)).CreationTimeUtc;

            return TempFolder;
        }

        private static void FillFolder(string Path, string[] Names, int index, CFolder rarfolder, RARFileInfo RE)
        {
            if (!(rarfolder.Folders.Contains(Names[index])))
            {
                if ((Names.Length <= index + 1) && (!RE.IsDirectory))
                {
                    rarfolder.Files.Add(Names[index]);
                    rarfolder.Files[rarfolder.Files.Count - 1].Size = RE.UnpackedSize;
                    rarfolder.Files[rarfolder.Files.Count - 1].LastModifiedTime = RE.FileTime;
                }
                else
                {
                    rarfolder.Folders[rarfolder.Folders.Add(Names[index])].CreatedTime = RE.FileTime;
                }
            }

            index++;
            if (Names.Length <= index)
                return;
            FillFolder(Path, Names,index, rarfolder.Folders[Names[index - 1]], RE);
        }
    }
}