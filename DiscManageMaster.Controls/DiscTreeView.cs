using System;
using System.Windows.Forms;
using DiscManageMaster.Core;
using DiscManageMaster.Core.Classes;
using TreeView=VistaControls.TreeView;

namespace DiscManageMaster.Controls
{
    public class DiscTreeView : TreeView
    {
        public DiscTreeView()
        {
            ItemHeight = 18;
        }

        private CRoot thisRoot;
        public CRoot Root
        {
            get { return thisRoot; }
            set
            {
                if (value == null)
                    return;

                thisRoot = value;

                Nodes.Add("Welcome", "Welcome", CommVar.WelcomeIconIndex, CommVar.WelcomeIconIndex);
                Nodes.Add("MyCollections", "My Collections", CommVar.MyCollectionsIconIndex, CommVar.MyCollectionsIconIndex);
                Nodes.Add("SearchResult", "Search Result", CommVar.SearchResultIconIndex, CommVar.SearchResultIconIndex);
                Nodes.Add("Settings", "Settings", CommVar.OptionsIconIndex, CommVar.OptionsIconIndex);
                Nodes.Add("Help", "Help", CommVar.HelpIconIndex, CommVar.HelpIconIndex);
                Nodes.Add("About", "About", CommVar.AboutIconIndex, CommVar.AboutIconIndex);

                try
                {
                    TreeNode tn1 = Nodes[1];

                    for (int i = 0; i < thisRoot.Collection.Count; i++)
                    {
                        TreeNode tn2;
                        switch (i)
                        {
                            case 0:
                                {
                                    tn2 = tn1.Nodes.Add("", thisRoot.Collection[i].Name, CommVar.DiscIconIndex, CommVar.DiscIconIndex);
                                } break;
                            case 1:
                                {
                                    tn2 = tn1.Nodes.Add("", thisRoot.Collection[i].Name, CommVar.RemovableIconIndex, CommVar.RemovableIconIndex);
                                } break;
                            case 2:
                                {
                                    tn2 = tn1.Nodes.Add("", thisRoot.Collection[i].Name, CommVar.LocalFolderIconIndex, CommVar.LocalFolderIconIndex);
                                } break;
                            default:
                                {
                                    tn2 = tn1.Nodes.Add("", thisRoot.Collection[i].Name, CommVar.CollectionIconIndex, CommVar.CollectionIconIndex);
                                } break;
                        }

                        for (int j = 0; j < thisRoot.Collection[i].Count; j++)
                        {
                            TreeNode tn3 = tn2.Nodes.Add("", thisRoot.Collection[i][j].Name, thisRoot.Collection[i][j].IconIndex, thisRoot.Collection[i][j].IconIndex == CommVar.CloseFolderIconIndex ? CommVar.OpenFolderIconIndex : thisRoot.Collection[i][j].IconIndex);
                            Functions.FillTreeView(thisRoot.Collection[i][j].Folders, tn3.Nodes);
                        }
                    }

                    tn1.Expand();
                    SelectedNode = tn1;
                }
                catch
                {
                }
            }
        }

        //public static void FillTreeView(CFolderCollection folders, TreeNodeCollection tnc)
        //{
        //    for (int i = 0; i<folders.Count;i++)
        //    {
        //        int selectfoldericonindex = folders[i].IconIndex == CommVar.CloseFolderIconIndex ? CommVar.OpenFolderIconIndex : folders[i].IconIndex;
        //        FillTreeView(folders[i].Folders, tnc.Add(folders[i].Name, folders[i].Name, folders[i].IconIndex, selectfoldericonindex).Nodes);
        //    }
        //}

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            SystemImageListHelper.SetTreeViewImageList(this, CommVar.sil_s, false);
        }
    }
}