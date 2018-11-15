using System;
using System.Drawing;
using System.IO;

namespace DiscManageMaster.Core.Classes
{
    [Serializable]
    public class CDisc : CFolder
    {
        private Bitmap thisCover;
        private string thisLabel;
        private CommVar.Plugins[] thisPlugins;

        public CDisc(DirectoryInfo di, string name, int iconindex, string memo, CommVar.Plugins[] plugins,CommVar.FolderType foldertype)
        {
            Name = name;
            IconIndex = iconindex;
            Memo = memo;
            CreatedTime = DateTime.Now;
            Functions.FolderInfo2CFolder(Files, Folders, di, plugins,
                                         foldertype == CommVar.FolderType.LocalFolder ? true : false);
            FolderType = foldertype;
            Path = (foldertype == CommVar.FolderType.LocalFolder || foldertype == CommVar.FolderType.RemoveableFolder) ? di.FullName : null;
        }

        public string Label
        {
            get { return thisLabel; }
            set { thisLabel = value; }
        }

        public Bitmap Cover
        {
            get { return thisCover; }
            set { thisCover = value; }
        }

        public CommVar.Plugins[] Plugins
        {
            get { return thisPlugins; }
            set { thisPlugins = value; }
        }
    }
}