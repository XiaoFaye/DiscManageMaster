using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using DiscManageMaster.Core;

namespace DiscManageMaster
{
    public partial class FormAddNew : Form
    {
        public bool IsUpdate = false;
        private CommVar.NewFolderType nft;
        public string Path = "";
        public string FolderName = "";
        public string Memo = null;
        public int IconIndex = 1;
        private int thisCollectionIndex = 0;
        private CommVar.Plugins[] ps;
        public ArrayList Plugins = new ArrayList();

        public CommVar.NewFolderType NewFolderType
        {
            get { return nft; }
            set
            {
                nft = value;
                switch(nft)
                {
                    case CommVar.NewFolderType.CD:
                        {
                            TabAddNew.SelectedIndex = 0;
                            TabAddNew_Selected(this, new TabControlEventArgs(TabAddNewCD, 0, TabControlAction.Selected));
                        }
                        break;
                    case CommVar.NewFolderType.Removable:
                        {
                            TabAddNew.SelectedIndex = 1;
                            TabAddNew_Selected(this, new TabControlEventArgs(TabAddNewRemovable, 1, TabControlAction.Selected));
                        }
                        break;
                    case CommVar.NewFolderType.LocalFolder:
                        {
                            TabAddNew.SelectedIndex = 2;
                            TabAddNew_Selected(this, new TabControlEventArgs(TabAddNewLocalFolder, 2, TabControlAction.Selected));
                        }
                        break;
                }
            }
        }

        public int CollectionIndex
        {
            get
            {
                if (thisCollectionIndex == 1)
                    return 3;
                if (thisCollectionIndex >= 2)
                    return thisCollectionIndex + 2;

                if (thisCollectionIndex == 0)
                    switch (nft)
                    {
                        case CommVar.NewFolderType.CD:
                            return 0;
                        case CommVar.NewFolderType.Removable:
                            return 1;
                        case CommVar.NewFolderType.LocalFolder:
                            return 2;
                    }

                return thisCollectionIndex;
            }
            set { thisCollectionIndex = value; }
        }

        public FormAddNew()
        {
            InitializeComponent();
            TabAddNew.Selecting += delegate(object sender, TabControlCancelEventArgs e)
            {
                if (IsUpdate)
                {
                    e.Cancel = true;
                }
            };
        }

        private void FormAddNew_Load(object sender, EventArgs e)
        {
            ListViewPlugins.Columns.Add("Name",100);
            ListViewPlugins.Columns.Add("Type");
            
            ps = Core.Functions.GetPlugins();
            for (int i = 0; i < ps.Length; i++)
            {
                ListViewPlugins.Items.Add(ps[i].name, Core.Functions.GetIconIndex("." + ps[i].name)).SubItems.Add(
                    ps[i].pt.ToString());
            }
            
            ListViewPlugins.View = View.Details;
            ListViewCD.View = View.Tile;
            ListViewRemovable.View = View.Tile;

            label1.Text = "Please select the plugins you want to use.";

            if (IsUpdate)
            {
                comboBox1.Enabled = false;
                switch (NewFolderType)
                {
                    case CommVar.NewFolderType.LocalFolder:
                        TabAddNew.SelectedIndex = 2; break;
                    case CommVar.NewFolderType.Removable:
                        TabAddNew.SelectedIndex = 1; break;
                }
            }
        }

        private void GetCD()
        {
            ListViewCD.Items.Clear();
            DriveInfo[] dis = Core.Functions.GetDrivers(DriveType.CDRom);
            for (int i = 0; i < dis.Length; i++)
            {
                if (dis[i].IsReady)
                    ListViewCD.Items.Add(dis[i].RootDirectory.FullName, dis[i].VolumeLabel,
                                         CommVar.sil_x.IconIndex(dis[i].RootDirectory.FullName, true));
            }

            if (ListViewCD.Items.Count == 0)
                label3.Text =
                    "No Compact Disc is ready in your computer, you can insert a Compact Disc and click Refresh below.";
            else
                label3.Text = "";

            comboBox1.Items.Clear();
            comboBox1.Items.Add(PublicVar.root.Collection[0].Name);
            for(int j = 3; j< PublicVar.root.Collection.Count; j++)
            {
                comboBox1.Items.Add(PublicVar.root.Collection[j].Name);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void GetRemovable()
        {
            ListViewRemovable.Items.Clear();
            DriveInfo[] dis = Core.Functions.GetDrivers(DriveType.Removable);
            for (int i = 0; i < dis.Length; i++)
            {
                if (dis[i].IsReady)
                    ListViewRemovable.Items.Add(dis[i].RootDirectory.FullName, dis[i].VolumeLabel, CommVar.sil_x.IconIndex(dis[i].RootDirectory.FullName));
            }

            if (ListViewRemovable.Items.Count == 0)
                label3.Text =
                    "No Removable Driver is ready in your computer, you can insert a Removable Driver and click Refresh below.";
            else
                label3.Text = "";

            comboBox1.Items.Clear();
            comboBox1.Items.Add(PublicVar.root.Collection[1].Name);
            for (int j = 3; j < PublicVar.root.Collection.Count; j++)
            {
                comboBox1.Items.Add(PublicVar.root.Collection[j].Name);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void TabAddNew_Selected(object sender, TabControlEventArgs e)
        {
            switch (e.TabPageIndex)
            {
                case 0:
                    {
                        nft = CommVar.NewFolderType.CD;
                        ListViewCD.Items.Clear();
                        SystemImageListHelper.SetListViewImageList(ListViewCD, CommVar.sil_x, false);
                        GetCD();
                    }
                    break;
                case 1:
                    {
                        nft = CommVar.NewFolderType.Removable;
                        ListViewRemovable.Items.Clear();
                        SystemImageListHelper.SetListViewImageList(ListViewRemovable, CommVar.sil_x, false);
                        GetRemovable();
                    }
                    break;
                case 2:
                    {
                        nft = CommVar.NewFolderType.LocalFolder;

                        label3.Text = "";

                        comboBox1.Items.Clear();
                        comboBox1.Items.Add(PublicVar.root.Collection[2].Name);
                        for (int j = 3; j < PublicVar.root.Collection.Count; j++)
                        {
                            comboBox1.Items.Add(PublicVar.root.Collection[j].Name);
                        }
                        comboBox1.SelectedIndex = 0;
                    }
                    break;
            }
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            int count = 0;
            if (!PublicVar.IsRegisted())
            {

                for (int i = 0; i < PublicVar.root.Collection.Count; i++)
                {
                    for (int j = 0; j < PublicVar.root.Collection[i].Count; j++)
                    {
                        count++;
                        if (count >= 10)
                        {
                            if (MessageBox.Show("You can only manage 10 folders with Unregisteration version! Do you want to register now?", "Add Folder", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                                DialogResult = DialogResult.Yes;
                            else
                                DialogResult = DialogResult.Cancel;
                            return;
                        }
                    }
                }
            }

            for (int i = 0; i < ListViewPlugins.Items.Count; i++)
            {
                if (ListViewPlugins.Items[i].Checked)
                    Plugins.Add(ps[i]);
            }

            if (!PublicVar.IsRegisted() && Plugins.Count > 0)
            {
                if (MessageBox.Show("You cannot using Plug-ins with Unregisteration version! Do you want to register now?", "Add Folder", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                    DialogResult = DialogResult.Yes;
                else
                    DialogResult = DialogResult.Cancel;
                return;
            }

            switch(nft)
            {
                case CommVar.NewFolderType.CD:
                    {
                        if(ListViewCD.Items.Count ==0)
                        {
                            MessageBox.Show("No Compact Disc selected! please insert your Compact Disc and refresh.", "New Folder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        if(ListViewCD.SelectedItems.Count ==0)
                        {
                            MessageBox.Show("No CD selected! ", "New Folder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        Path = ListViewCD.SelectedItems[0].Name;
                        FolderName = ListViewCD.SelectedItems[0].Text;
                        IconIndex = ListViewCD.SelectedItems[0].ImageIndex;
                        Memo = TextBoxMemo.Text;
                    }
                    break;
                case CommVar.NewFolderType.Removable:
                    {
                        if (ListViewRemovable.Items.Count == 0)
                        {
                            MessageBox.Show("No Removable Drive selected! please insert your compact disc and refresh.", "New Folder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        if (ListViewRemovable.SelectedItems.Count == 0)
                        {
                            MessageBox.Show("No Removable Drive selected! ");
                            return;
                        }

                        Path = ListViewRemovable.SelectedItems[0].Name;
                        FolderName = ListViewRemovable.SelectedItems[0].Text;
                        IconIndex = ListViewRemovable.SelectedItems[0].ImageIndex;
                        Memo = TextBoxMemo.Text;
                    }
                    break;
                case CommVar.NewFolderType.LocalFolder:
                    {
                        if(TreeViewLocalFolder.SelectedNode == null)
                        {
                            MessageBox.Show("No Folder selected! ");
                            return;
                        }

                        Path = TreeViewLocalFolder.SelectedNode.Name;
                        FolderName = TreeViewLocalFolder.SelectedNode.Text;
                        IconIndex = TreeViewLocalFolder.SelectedNode.ImageIndex; 
                        Memo = TextBoxMemo.Text;
                    }
                    break;
            }

            thisCollectionIndex = comboBox1.SelectedIndex;

            for (int i = 0; i < PublicVar.root.Collection[CollectionIndex].Count; i++)
            {
                if (FolderName == PublicVar.root.Collection[CollectionIndex][i].Name)
                { 
                    //MessageBox.Show("A Folder have the same name is already exist in this Collection, 
                    int temp = 1;
                    while (PublicVar.root.Collection[CollectionIndex].Contains(FolderName + "_" + temp))
                        temp++;

                    FolderName = FolderName + "_" + temp;
                    break;
                }
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            switch(TabAddNew.SelectedIndex)
            {
                case 0:
                    GetCD();
                    break;
                case 1:
                    GetRemovable();
                    break;
            }
        }

        private void ListViewPlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListViewPlugins.SelectedItems.Count > 0)
            {
                label1.Text = ps[ListViewPlugins.SelectedItems[0].Index].description;
                ListViewPlugins.SelectedItems[0].Checked = ListViewPlugins.SelectedItems[0].Checked ? false : true;
            }
            else
                label1.Text = "Please select the plugins you want to use.";
        }
    }
}
