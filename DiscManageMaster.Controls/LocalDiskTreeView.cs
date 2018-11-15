using System;
using System.IO;
using System.Windows.Forms;
using DiscManageMaster.Core;
using TreeView = VistaControls.TreeView;

namespace DiscManageMaster.Controls
{
    public class LocalDiskTreeView : TreeView
    {
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            
            TreeNode tn = GetNodeAt(e.X, e.Y);
            if (tn.Nodes.Count == 0)
            {
                try
                {
                    DirectoryInfo dis = new DirectoryInfo(tn.Name);
                    foreach (DirectoryInfo di in dis.GetDirectories())
                    {
                        if (!Functions.IsNormalFileSystem(di.Attributes)) continue;
                        int index = CommVar.sil_s.IconIndex(di.FullName, true);
                        tn.Nodes.Add(di.FullName, di.Name, index, index);
                    }

                    tn.Expand();
                }catch{}
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            SystemImageListHelper.SetTreeViewImageList(this, CommVar.sil_s, false);

            Nodes.Clear();

            foreach (DriveInfo di in DriveInfo.GetDrives())
            {
                if (di.DriveType == DriveType.Fixed && di.IsReady)
                    Nodes.Add(di.Name, di.VolumeLabel + " (" + di.Name.Substring(0, 2) + ")",
                              CommVar.sil_s.IconIndex(di.Name),
                              CommVar.sil_s.IconIndex(di.Name));
            }
        }
    }
}