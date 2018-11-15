using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DiscManageMaster.Core;
using DiscManageMaster.Core.Classes;
using DiscManageMaster.Controls;

namespace DiscManageMaster
{
    public partial class MainForm : Form
    {
        #region "Private members..."

        /// <summary>
        /// DataChanged: 判断数据是否已经更改，需要保存。
        /// </summary>
        private bool DataChanged = false;

        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private int timercount = 0;

        #endregion

        #region "Action on load..."

        public MainForm()
        {
            InitializeComponent();
            Text = "Personal Files Manager";
            travelBar1.TB.OnHistoryChanged += HistoryChanged;
            rightPanel1.ListView.OnListViewFilled += discListView1_OnListViewFilled;
            travelBar1.OnSearchEnded += SearchEnded;

            rightPanel1.ListView.MouseClick += discListView1_MouseClick;
            rightPanel1.ListView.MouseDoubleClick += discListView1_MouseDoubleClick;
            rightPanel1.ListView.OnListViewFilled += discListView1_OnListViewFilled;

            splitContainer3.Panel2MinSize = 150;
            
            rightPanel1.Welcome.NewFolder.Click += NewFolder;
            rightPanel1.Welcome.ViewFiles.Click += ViewFiles;
            rightPanel1.Welcome.ChangeSettings.Click += ChangeSettings;

            rightPanel1.Option.ButtonCancel.Click += OptionCancel;
            rightPanel1.Option.ButtonSave.Click += OptionSave;

            rightPanel1.About.OnRegNow += RegNow;

            infoBar1.Update.Click += UpdateFolder;

            Shown += MainForm_Shown;
            timer.Enabled = false;
            timer.Interval = 100;
            timer.Tick += Delay;


            //discTreeView1.MouseClick +=new MouseEventHandler(discTreeView1_MouseClick);

            if (PublicVar.root.CreatedTime.Year == 0001)
            {
                PublicVar.root.CreatedTime = DateTime.Now;
                DataChanged = true;
            }
        }

        private void Delay(object sender, EventArgs e)
        {
            timercount++;
            if (timercount == 4)
            {
                timer.Enabled = false;
                Register reg = new Register();
                reg.StartPosition = FormStartPosition.CenterParent;
                if (reg.ShowDialog(this) != DialogResult.OK)
                    myToolBar1.AddButton("Register", Images.Edit, "Register");
                else
                {
                    Text = "Personal Files Manager";
                    rightPanel1.About.IsRegisted = true;
                    rightPanel1.About.RegisterName = PublicVar.root.UserName;
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                Directory.Delete(CommVar.TempFolder, true);
            }
            catch { }
            Directory.CreateDirectory(CommVar.TempFolder);
            
            SetToolBar();
            rightPanel1.ListView.Columns.Add("Name", 200);
            rightPanel1.ListView.Columns.Add("Type", 200);
            rightPanel1.ListView.Columns.Add("Size", 200);
            myToolBar1.Buttons["Views"].DropDownMenu.MenuItems[4].PerformClick();

            discTreeView1.Root = PublicVar.root;
            discTreeView1.Nodes[1].Expand();
            LoadOptions();
            if (!PublicVar.IsRegisted()) Text = Text + " - Unregistered version";
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (!PublicVar.IsRegisted())
                timer.Enabled = true;
            else
                rightPanel1.About.RegisterName = PublicVar.root.UserName;
        }

        private void LoadOptions()
        {
            switch (PublicVar.root.Options.StartupPage)
            {
                case 0:
                    {
                        discTreeView1.SelectedNode = discTreeView1.Nodes[0];
                    }
                    break;
                case 1:
                    {
                        discTreeView1.SelectedNode = discTreeView1.Nodes[1];
                    }
                    break;
                case 2:
                    {
                        try
                        {
                            TreeNode tn = discTreeView1.Nodes[int.Parse(PublicVar.root.Options.LastPosition[0].ToString())];
                            for (int i = 1; i < PublicVar.root.Options.LastPosition.Count; i++)
                            {
                                tn = tn.Nodes[int.Parse(PublicVar.root.Options.LastPosition[i].ToString())];
                            }
                            tn.EnsureVisible();
                            discTreeView1.SelectedNode = tn;
                        }
                        catch
                        {
                            discTreeView1.SelectedNode = discTreeView1.Nodes[0];
                        }
                    }
                    break;
            }

            rightPanel1.Option.StartupPage = PublicVar.root.Options.StartupPage;
            if (!CommVar.IsVista && PublicVar.root.Options.ViewSetting == 2)
                PublicVar.root.Options.ViewSetting = 0;
            rightPanel1.Option.ViewSetting = PublicVar.root.Options.ViewSetting;
            ViewMode(myToolBar1.Buttons[3].DropDownMenu.MenuItems[PublicVar.root.Options.ViewSetting], new EventArgs());
            rightPanel1.Option.SaveOnExit = PublicVar.root.Options.SaveOnExit;
            //rightPanel1.Option.ShowHidden = PublicVar.root.Options.ShowHidden;
        }

        private void SetToolBar()
        {
            ContextMenu newmenu = new ContextMenu();
            newmenu.MenuItems.Add("New CD...", NewFolder);
            newmenu.MenuItems.Add("New Removable...", NewFolder);
            newmenu.MenuItems.Add("New Local Folder...", NewFolder);
            myToolBar1.AddButton("NewFolder", "&New Folder", Images.New, "NewFolder", ToolBarButtonStyle.DropDownButton, newmenu);

            myToolBar1.AddButton("New &Collection", Images.Favorites, "NewCollection");

            ContextMenu delmenu = new ContextMenu();
            delmenu.MenuItems.Add("Delete Folder...", DeleteFolder);
            delmenu.MenuItems.Add("Delete Collection...", DeleteCollection);
            myToolBar1.AddButton("DeleteFolder", "&Delete Folder", Images.Delete, "Delete", ToolBarButtonStyle.DropDownButton, delmenu);

            ContextMenu viewmenu = new ContextMenu();
            viewmenu.MenuItems.Add("List", ViewMode).RadioCheck = true;
            viewmenu.MenuItems.Add("LargeIcon", ViewMode).RadioCheck = true;
            viewmenu.MenuItems.Add("LargeIcon (Vista)", ViewMode).Enabled = CommVar.IsVista;
            viewmenu.MenuItems[2].RadioCheck = true;
            viewmenu.MenuItems.Add("Details", ViewMode).RadioCheck = true;
            viewmenu.MenuItems.Add("Tile", ViewMode).RadioCheck = true;

            myToolBar1.AddButton("Views", "&Views", Images.Tiles, "Tiles", ToolBarButtonStyle.DropDownButton, viewmenu);
            rightPanel1.ListView.ContextMenu = viewmenu;

            ContextMenu datamenu = new ContextMenu();
            datamenu.MenuItems.Add("Save Data", DataProcess);
            datamenu.MenuItems.Add("Reload Data", DataProcess);
            datamenu.MenuItems.Add("Clear Data", DataProcess);
            datamenu.MenuItems.Add("Restore Data", DataProcess);
            datamenu.MenuItems.Add("Backup Data", DataProcess);
            myToolBar1.AddButton("SaveData", "&Save Data", Images.Save, "Save", ToolBarButtonStyle.DropDownButton, datamenu);

            myToolBar1.AddButton("&Options", Images.FolderProperties, "Options");

            myToolBar1.AddButton("&Exit", Images.Undo, "Exit");
        }

        #endregion

        #region "Event handlers..."

        private void RegNow(object sender, EventArgs e)
        {
            myToolBar1_ButtonClick(this, new ToolBarButtonClickEventArgs(myToolBar1.Buttons[myToolBar1.Buttons.Count - 1]));
        }

        private void OptionCancel(object sender, EventArgs e)
        {
            travelBar1.TB.MoveBack();
        }

        private void OptionSave(object sender, EventArgs e)
        {
            PublicVar.root.Options.StartupPage = rightPanel1.Option.StartupPage;
            PublicVar.root.Options.ViewSetting = rightPanel1.Option.ViewSetting;
            PublicVar.root.Options.SaveOnExit = rightPanel1.Option.SaveOnExit;
            rightPanel1.Option.ButtonSave.Enabled = false;
            DataChanged = true;
            MessageBox.Show("Settings saved!", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ViewFiles(object sender, EventArgs e)
        {
            discTreeView1.SelectedNode = discTreeView1.Nodes[1];
        }

        private void ChangeSettings(object sender,EventArgs e)
        {
            discTreeView1.SelectedNode = discTreeView1.Nodes[3];
        }

        private void NewFolder(object sender, EventArgs e)
        {
            CommVar.NewFolderType nft;
            try
            {
                nft = (CommVar.NewFolderType)((MenuItem)sender).Index + 1;
            }
            catch
            {
                try
                {
                    nft = (CommVar.NewFolderType) sender;
                }
                catch
                {
                    nft = CommVar.NewFolderType.CD;
                }
            }

            try
            {
                FormAddNew fan = new FormAddNew();
                fan.NewFolderType = nft;
                fan.StartPosition = FormStartPosition.CenterParent;
                DialogResult ds = fan.ShowDialog(this);

                if (ds == DialogResult.OK)
                {
                    Loading loading = new Loading();
                    loading.CollectionIndex = fan.CollectionIndex;
                    loading.Path = fan.Path;
                    loading.FolderName = fan.FolderName;
                    loading.Memo = fan.Memo;
                    loading.IconIndex = fan.IconIndex;
                    loading.Plugins = fan.Plugins;
                    loading.StartPosition = FormStartPosition.CenterParent;

                    switch (fan.NewFolderType)
                    {
                        case CommVar.NewFolderType.CD:
                            loading.FT = CommVar.FolderType.DiscFolder;
                            break;
                        case CommVar.NewFolderType.Removable:
                            loading.FT = CommVar.FolderType.RemoveableFolder;
                            break;
                        case CommVar.NewFolderType.LocalFolder:
                            loading.FT = CommVar.FolderType.LocalFolder;
                            break;
                    }

                    if (loading.ShowDialog(this) == DialogResult.OK)
                    {
                        DataChanged = true;
                        int index = discTreeView1.Nodes[1].Nodes[fan.CollectionIndex].Nodes.Add(loading.TN);
                        discTreeView1.Nodes[1].Nodes[fan.CollectionIndex].Nodes[index].EnsureVisible();
                        discTreeView1.SelectedNode = discTreeView1.Nodes[1].Nodes[fan.CollectionIndex].Nodes[index];
                    }
                }
                else if (ds == DialogResult.Yes)
                    myToolBar1_ButtonClick(this, new ToolBarButtonClickEventArgs(myToolBar1.Buttons[myToolBar1.Buttons.Count - 1]));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ViewMode(object sender, EventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            mi.Checked = true;

            foreach (MenuItem m in mi.GetContextMenu().MenuItems)
                if (m.Index != mi.Index)
                    m.Checked = false;

            switch (mi.Index)
            {
                case 0:
                    {
                        myToolBar1.SetButtonImage(3, Images.List, "List");
                        rightPanel1.ListView.View = View.List;
                    }
                    break;
                case 1:
                    {
                        rightPanel1.ListView.UseVistaIcon = false;
                        myToolBar1.SetButtonImage(3, Images.Icons, "LargeIcon");
                        rightPanel1.ListView.View = View.LargeIcon;
                    }
                    break;
                case 2:
                    {
                        rightPanel1.ListView.UseVistaIcon = true;
                        myToolBar1.SetButtonImage(3, Images.Icons, "LargeIcon");
                        rightPanel1.ListView.View = View.LargeIcon;
                    }
                    break;
                case 3:
                    {
                        myToolBar1.SetButtonImage(3, Images.Details, "Details");
                        rightPanel1.ListView.View = View.Details;
                    }
                    break;
                case 4:
                    {
                        myToolBar1.SetButtonImage(3, Images.Tiles, "Tiles");
                        rightPanel1.ListView.View = View.Tile;
                    }
                    break;
            }
        }

        private void DataProcess(object sender, EventArgs e)
        {
            switch (((MenuItem)sender).Index)
            {
                case 0:
                    {
                        if (DataChanged)
                        {
                            CRoot.Save(PublicVar.root);
                            PublicVar.backuproot = CRoot.Load();
                            MessageBox.Show("Data saved!", "Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DataChanged = false;
                        }
                    }
                    break;
                case 1:
                    {
                        if(MessageBox.Show("Reload will lost all data that haven't saved, are you sure?","Data",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            travelBar1.TB.RemoveAllSteps();
                            discTreeView1.Nodes.Clear();
                            PublicVar.root = CRoot.Load();
                            discTreeView1.Root = PublicVar.root;
                            DataChanged = false;
                        }
                    }
                    break;
                case 2:
                    {
                        if (MessageBox.Show("Clear will lost all data that haven't saved, are you sure?", "Data", MessageBoxButtons.YesNo,MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            travelBar1.TB.RemoveAllSteps();
                            discTreeView1.Nodes.Clear();
                            DateTime dt = PublicVar.root.RegDate;
                            PublicVar.root = new CRoot("Root");
                            PublicVar.root.RegDate = dt;
                            discTreeView1.Root = PublicVar.root;
                            DataChanged = true;
                        }
                    }
                    break;
                case 3:
                    {
                        if (MessageBox.Show("Restore will lost all data that haven't saved, are you sure?", "Data", MessageBoxButtons.YesNo,MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            OpenFileDialog ofd = new OpenFileDialog();
                            ofd.Filter = "Data files (*.dat)|*.dat";
                            ofd.Multiselect = false;
                            ofd.Title = "Restore data";
                            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                try
                                {
                                    CRoot temp = CRoot.Load(ofd.FileName);
                                    if (temp.Name == "Root")
                                    {
                                        DateTime dt = temp.RegDate;
                                        PublicVar.root = temp;
                                        PublicVar.root.RegDate = dt;
                                        discTreeView1.Nodes.Clear();
                                        discTreeView1.Root = PublicVar.root;
                                        DataChanged = true;
                                        MessageBox.Show("Data restored!", "Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                catch
                                {
                                    MessageBox.Show("Data restore fail! Please choose the right data file.", "Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    break;
                case 4:
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "Data files (*.dat)|*.dat";
                        sfd.AddExtension = true;
                        sfd.FileName = DateTime.Now.Date.ToShortDateString();
                        sfd.Title = "Backup data";
                        
                        if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            CRoot.Save(PublicVar.root, sfd.FileName);
                            MessageBox.Show("Data backuped!", "Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    break;
            }
        }

        private void DeleteCollection(object sender, EventArgs e)
        {
            if (discTreeView1.SelectedNode.Index > 3 && discTreeView1.SelectedNode.Level == 1 && discTreeView1.SelectedNode.Parent.Index == 1)
            {
                if (MessageBox.Show("Do you really want to delete this Collection: " + discTreeView1.SelectedNode.Text, "Confirm Delete", MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (PublicVar.root.Collection[discTreeView1.SelectedNode.Index].Count > 0)
                        MessageBox.Show("Please delete all Folders in this Collection before delete this Collection!", "Delete Collection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        DataChanged = true;
                        PublicVar.root.Collection.RemoveAt(discTreeView1.SelectedNode.Index);
                        int temp = travelBar1.TB.FindStep(discTreeView1.SelectedNode);
                        discTreeView1.SelectedNode.Remove();
                        if (temp != -1)
                            travelBar1.TB.RemoveStep(temp);
                    }
                }
            }
            else
                MessageBox.Show("Please select a Collection that you want to delete.", "Delete Collection", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void DeleteFolder(object sender, EventArgs e)
        {
            if (discTreeView1.SelectedNode.Level == 2)
            {
                if (MessageBox.Show("Do you really want to delete this folder: " + discTreeView1.SelectedNode.Text, "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    DataChanged = true;
                    PublicVar.root.Collection[discTreeView1.SelectedNode.Parent.Index].RemoveAt(discTreeView1.SelectedNode.Index);
                    int temp = travelBar1.TB.FindStep(discTreeView1.SelectedNode);
                    discTreeView1.SelectedNode.Remove();
                    if (temp != -1)
                        travelBar1.TB.RemoveStep(temp);
                }
            }
            else
                MessageBox.Show("Please select a Folder that you want to delete.", "Delete Collection", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void HistoryChanged(object sender, HistoryEventArgs e)
        {
            TreeNode tn = (TreeNode)e.Data;
            if (tn.Name != "Search")
            {
                discTreeView1.SelectedNode = tn;
            }
            else
            {
                discTreeView1.Nodes[2].Text = tn.Text;
                discTreeView1.Nodes[2].Tag = tn.Tag;
                travelBar1.Address = tn.Text;
                discTreeView1.SelectedNode = discTreeView1.Nodes[2];
                discTreeView1_AfterSelect(this, new TreeViewEventArgs(discTreeView1.Nodes[2], TreeViewAction.ByMouse));
            }
        }

        private void UpdateFolder(object sender, EventArgs e)
        {
            if (!PublicVar.IsRegisted())
            {
                if (MessageBox.Show("You can't use Update function with Unregisteration version! Do you want to register now?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    RegNow(sender, e);
                }
                return;
            }

            int CollectionIndex;
            TreeNode node;
            CDisc disc;
            CommVar.NewFolderType nft = CommVar.NewFolderType.LocalFolder;
            if (rightPanel1.ListView.SelectedItems.Count > 0)
            {
                CollectionIndex = discTreeView1.SelectedNode.Index;
                node = discTreeView1.SelectedNode.Nodes[rightPanel1.ListView.SelectedItems[0].Index];
                disc = PublicVar.root.Collection[CollectionIndex][rightPanel1.ListView.SelectedItems[0].Index];
            }
            else
            {
                CollectionIndex = discTreeView1.SelectedNode.Parent.Index;
                node = discTreeView1.SelectedNode;
                disc = PublicVar.root.Collection[CollectionIndex][discTreeView1.SelectedNode.Index];
            }

            if (disc.FolderType == CommVar.FolderType.RemoveableFolder)
                nft = CommVar.NewFolderType.Removable;

            Loading loading = new Loading();
            loading.CollectionIndex = CollectionIndex;

            FormAddNew fan;
            if (!Directory.Exists(disc.Path))
            {
                if (MessageBox.Show("Folder path is not exist, do you want to select another folder?", "Update", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        fan = new FormAddNew();
                    }
                    catch
                    {
                        return;
                    }

                    fan.IsUpdate = true;
                    fan.NewFolderType = nft;
                    fan.StartPosition = FormStartPosition.CenterParent;
                    if (fan.ShowDialog(this) != DialogResult.OK)
                        return;

                    loading.Path = fan.Path;
                    loading.FolderName = fan.FolderName;
                    loading.Memo = fan.Memo;
                    loading.IconIndex = fan.IconIndex;
                    loading.Plugins = fan.Plugins;
                    loading.StartPosition = FormStartPosition.CenterParent;
                    loading.FT = disc.FolderType;
                    loading.IsUpdate = true;
                    loading.DiscIndex = node.Index;
                }
                else
                    return;
            }
            else
            {
                loading.Path = disc.Path;
                loading.FolderName = disc.Name;
                loading.Memo = disc.Memo;
                loading.IconIndex = disc.IconIndex;
                ArrayList plugins = new ArrayList();
                try
                {
                    for (int i = 0; i < disc.Plugins.Length; i++)
                    {
                        plugins.Add(disc.Plugins[i]);
                    }
                }
                catch { }
                loading.Plugins = plugins;
                loading.StartPosition = FormStartPosition.CenterParent;
                loading.FT = disc.FolderType;
                loading.IsUpdate = true;
                loading.DiscIndex = node.Index;
            }

            if (loading.ShowDialog(this) == DialogResult.OK)
            {
                DataChanged = true;
                int a = node.Index;
                discTreeView1.Nodes[1].Nodes[CollectionIndex].Nodes.RemoveAt(a);
                discTreeView1.Nodes[1].Nodes[CollectionIndex].Nodes.Insert(a, loading.TN);
                discTreeView1.Nodes[1].Nodes[CollectionIndex].Nodes[a].EnsureVisible();
                discTreeView1_AfterSelect(this, new TreeViewEventArgs(discTreeView1.Nodes[1].Nodes[CollectionIndex].Nodes[a], TreeViewAction.ByMouse));
                discTreeView1.Nodes[1].Nodes[CollectionIndex].Nodes[a].Expand();
            }
        }

        #endregion

        #region "ToolBar, TreeView, ListView handler codes..."

        private void discListView1_OnListViewFilled(object sender, FillListViewEventArgs e)
        {
            API.LVITEM lvi = new API.LVITEM();
            lvi.state = -1;
            lvi.stateMask = API.LVIS_CUT;
            for (int i = 0; i < e.HiddenItem.Count; i++)
                API.SendMessage(rightPanel1.ListView.Handle, API.LVM_SETITEMSTATE, (int)e.HiddenItem[i], ref lvi);
        }

        private void myToolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            switch (myToolBar1.GetButtonIndex(e.Button))
            {
                case 0:
                    {
                        NewFolder(CommVar.NewFolderType.CD, new EventArgs());
                    }
                    break;
                case 1:
                    {
                        int temp = 1;
                        while (PublicVar.root.Collection.Contains("collection " + temp))
                            temp++;

                        PublicVar.root.Collection.Add(new CDiscCollection("collection " + temp),
                                                      discTreeView1.Nodes[1].Nodes);
                        discTreeView1.SelectedNode.BeginEdit();
                        DataChanged = true;
                    }
                    break;
                case 2:
                    {
                        DeleteFolder(sender, new EventArgs());
                    }
                    break;
                case 3:
                    {
                        if (!rightPanel1.ListView.Visible) return;
                        foreach (MenuItem mi in myToolBar1.Buttons["Views"].DropDownMenu.MenuItems)
                            if (mi.Checked)
                            {
                                try
                                {
                                    if (!myToolBar1.Buttons["Views"].DropDownMenu.MenuItems[mi.Index + 1].Enabled)
                                        myToolBar1.Buttons["Views"].DropDownMenu.MenuItems[mi.Index + 2].PerformClick();
                                    else
                                        myToolBar1.Buttons["Views"].DropDownMenu.MenuItems[mi.Index + 1].PerformClick();
                                    return;
                                }
                                catch { }
                                myToolBar1.Buttons["Views"].DropDownMenu.MenuItems[0].PerformClick();
                            }
                    }
                    break;
                case 4:
                    {
                        DataProcess(myToolBar1.Buttons[4].DropDownMenu.MenuItems[0], new EventArgs());
                    }
                    break;
                case 5:
                    {
                        discTreeView1.SelectedNode = discTreeView1.Nodes[3];
                    }
                    break;
                case 6:
                    {
                        Application.Exit();
                    }
                    break;
                case 7:
                    {
                        Register reg = new Register();
                        reg.StartPosition = FormStartPosition.CenterParent;
                        if (reg.ShowDialog(this) == DialogResult.OK)
                        {
                            myToolBar1.Buttons[myToolBar1.Buttons.Count - 1].Visible = false;
                            Text = "Personal Files Manager";
                            rightPanel1.About.IsRegisted = true;
                            rightPanel1.About.RegisterName = PublicVar.root.UserName;
                        }
                    }
                    break;
            }
        }

        //private void discTreeView1_MouseClick(object sender, MouseEventArgs e)
        //{ 
        //    if(infoBar1.DetailMode ==InfoBarDetailMode.File)
        //        discTreeView1_AfterSelect(this,new TreeViewEventArgs(discTreeView1.GetNodeAt(e.Location)));
        //}

        private void discTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            infoBar1.SetImageFromIconIndex(e.Node.ImageIndex);

            rightPanel1.ListView.Items.Clear();
            rightPanel1.ListView.ListViewItemSorter = null;
            for (int i = 0; i < rightPanel1.ListView.Columns.Count; i++)
            {
                if (rightPanel1.ListView.Columns[i].Text.IndexOf(' ') > -1)
                    rightPanel1.ListView.Columns[i].Text = rightPanel1.ListView.Columns[i].Text.Split(' ')[0];
            }

            if (SearchNode != null) SearchNode = null;
            if (travelBar1.SearchKeyword != "") travelBar1.SearchKeyword = "";

            TreeNode tn = e.Node;
            while (tn.Parent != null)
                tn = tn.Parent;

            switch (tn.Index)
            {
                case 0:
                    {
                        infoBar1.SetDetailMode(null, InfoBarDetailMode.Welcome);
                        rightPanel1.View = RightPanelView.Welcome;
                    }
                    break;
                case 1:
                    {
                        infoBar1.SetDetailMode(PublicVar.root, InfoBarDetailMode.MyCollections);
                        rightPanel1.View = RightPanelView.Content;

                        switch (e.Node.Level)
                        {
                            case 0:
                                {
                                    for (int j = 0; j < PublicVar.root.Collection.Count; j++)
                                    {
                                        ListViewItem lvi;
                                        switch (j)
                                        {
                                            case 0:
                                                {
                                                    lvi = rightPanel1.ListView.Items.Add(PublicVar.root.Collection[j].Name, CommVar.DiscIconIndex);
                                                } break;
                                            case 1:
                                                {
                                                    lvi = rightPanel1.ListView.Items.Add(PublicVar.root.Collection[j].Name, CommVar.RemovableIconIndex);
                                                } break;
                                            case 2:
                                                {
                                                    lvi = rightPanel1.ListView.Items.Add(PublicVar.root.Collection[j].Name, CommVar.LocalFolderIconIndex);
                                                } break;
                                            default:
                                                {
                                                    lvi = rightPanel1.ListView.Items.Add(PublicVar.root.Collection[j].Name, CommVar.CollectionIconIndex);
                                                } break;
                                        }
                                        lvi.SubItems.Add("Data Collection");
                                        lvi.SubItems.Add(PublicVar.root.Collection[j].Count + (PublicVar.root.Collection[j].Count > 1 ? " Folders" : " Folder")).Tag = PublicVar.root.Collection[j].Count;
                                    }
                                } 
                                break;
                            case 1:
                                {
                                    rightPanel1.View = RightPanelView.Content;
                                    infoBar1.SetDetailMode(PublicVar.root.Collection[e.Node.Index], InfoBarDetailMode.Collection);

                                    int index = e.Node.Index;
                                    for (int j = 0; j < PublicVar.root.Collection[index].Count; j++)
                                    {
                                        ListViewItem lvi = rightPanel1.ListView.Items.Add(PublicVar.root.Collection[index][j].Name,
                                                                PublicVar.root.Collection[index][j].IconIndex);
                                        lvi.SubItems.Add(e.Node.Text);
                                        lvi.SubItems.Add(Core.Functions.SizeStr(PublicVar.root.Collection[index][j].Size)).Tag = PublicVar.root.Collection[index][j].Size;
                                    }
                                }
                                break;
                            case 2:
                                {
                                    rightPanel1.View = RightPanelView.Content;
                                    CFolder ccf = PublicVar.root.Collection[e.Node.Parent.Index][e.Node.Index];
                                    switch (ccf.FolderType)
                                    {
                                        case CommVar.FolderType.DiscFolder:
                                            infoBar1.SetDetailMode(ccf, InfoBarDetailMode.Disc);
                                            break;
                                        case CommVar.FolderType.RemoveableFolder:
                                            infoBar1.SetDetailMode(ccf, InfoBarDetailMode.Removable);
                                            break;
                                        case CommVar.FolderType.LocalFolder:
                                            infoBar1.SetDetailMode(ccf, InfoBarDetailMode.LocalFolder);
                                            break;
                                    }

                                    rightPanel1.ListView.Content = Core.Functions.GetSelectedFolder(PublicVar.root, e.Node);
                                }
                                break;
                            default:
                                {
                                    rightPanel1.View = RightPanelView.Content;
                                    infoBar1.SetDetailMode(Core.Functions.GetSelectedFolder(PublicVar.root, e.Node), InfoBarDetailMode.Folder);

                                    rightPanel1.ListView.Content = Core.Functions.GetSelectedFolder(PublicVar.root, e.Node);
                                }
                                break;
                        }
                    }
                    break;
                case 2:
                    {
                        infoBar1.SetDetailMode(e.Node.Tag, InfoBarDetailMode.SearchResult);
                        rightPanel1.View = RightPanelView.Content;

                        if (e.Node.Tag != null)//SH.Historys != null)
                            rightPanel1.ListView.Items.AddRange(((SearchHistory)e.Node.Tag).Historys);
                        travelBar1.Address = e.Node.Text;
                        return;
                    }
                    //break;
                case 3:
                    {
                        infoBar1.SetDetailMode(e.Node.Tag, InfoBarDetailMode.Options);
                        rightPanel1.View = RightPanelView.Options;
                    }
                    break;
                case 4:
                    {
                        if (tn.Nodes.Count == 0) Functions.GetHelpContent(e.Node);
                        infoBar1.SetDetailMode(e.Node, InfoBarDetailMode.Help);
                        rightPanel1.View = RightPanelView.Help;

                        if (e.Node.Tag != null)
                            rightPanel1.Help.HelpFilePath = e.Node.Tag.ToString();
                        else
                            rightPanel1.Help.HelpFilePath = null;
                    }
                    break;
                case 5:
                    {
                        infoBar1.SetDetailMode(e.Node.Tag, InfoBarDetailMode.About);
                        rightPanel1.View = RightPanelView.About;
                    }
                    break;
            }

            string str = "";
            tn = e.Node;
            while (tn != null)
            {
                str = tn.Text + " -> " + str;
                tn = tn.Parent;
            }
            travelBar1.Address = str.Substring(0, str.Length - 4);
            travelBar1.TB.AddStep(e.Node.Text, e.Node);
        }

        private void discTreeView1_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            try
            {
                if (e.Node.Level < 1 || e.Node.Level > 2)
                    e.CancelEdit = true;

                if (e.Node.Level == 1 && e.Node.Index <= 3)
                    e.CancelEdit = true;

                TreeNode tn = e.Node;
                while (tn.Parent != null)
                    tn = tn.Parent;
                if (tn.Index != 1) e.CancelEdit = true;
            }
            catch { }
        }

        private void discTreeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null && e.Node.Text == "")
            {
                MessageBox.Show("A collection or a Folder's name can't be null, please choose a name.", "Rename", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.CancelEdit = true;
                e.Node.BeginEdit();
                return;
            }

            if (e.Node.Level == 1)
            {
                if (PublicVar.root.Collection.Contains(e.Label))
                {
                    MessageBox.Show("A collection named \"" + e.Label + "\" already existed, please choose another name.", "Rename", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.CancelEdit = true;
                    e.Node.BeginEdit();
                }
                else
                {
                    if (e.Label != null)
                    {
                        DataChanged = true;
                        PublicVar.root.Collection[e.Node.Index].Name = e.Label;
                        e.Node.Text = e.Label;
                    }
                }
            }
            else
            {
                if (PublicVar.root.Collection[e.Node.Parent.Index].Contains(e.Label))
                {
                    MessageBox.Show("A Folder named \"" + e.Label + "\" already existed, please choose another name.", "Rename", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.CancelEdit = true;
                    e.Node.BeginEdit();
                }
                else
                {
                    if (e.Label != null)
                    {
                        DataChanged = true;
                        PublicVar.root.Collection[e.Node.Parent.Index][e.Node.Index].Name = e.Label;
                        e.Node.Text = e.Label;
                    }
                }
            }

            discTreeView1_AfterSelect(sender, new TreeViewEventArgs(e.Node, TreeViewAction.ByMouse));
        }

        private void discListView1_MouseClick(object sender, MouseEventArgs e)
        {
            ListViewItem Item = rightPanel1.ListView.GetItemAt(e.X, e.Y);
            if (Item == null)
                return;

            infoBar1.SetImageFromIconIndex(Item.ImageIndex);

            if (discTreeView1.Nodes[2].IsSelected)
            {
                bool IsFolder = Item.Name.Split('|')[0] == "1" ? true : false;
                infoBar1.SetDetailMode(Core.Functions.GetSelectedItem(PublicVar.root, Item, !IsFolder),
                                       IsFolder ? InfoBarDetailMode.Folder : InfoBarDetailMode.File);
            }
            else
            {
                CFolder ccf;
                TreeNode tn = discTreeView1.SelectedNode;
                switch (tn.Level)
                {
                    case 0:
                        infoBar1.SetDetailMode(PublicVar.root.Collection[Item.Index], InfoBarDetailMode.Collection);
                        break;
                    case 1:
                        {
                            ccf = PublicVar.root.Collection[tn.Index][Item.Index];
                            switch (ccf.FolderType)
                            {
                                case CommVar.FolderType.DiscFolder:
                                    infoBar1.SetDetailMode(ccf, InfoBarDetailMode.Disc);
                                    break;
                                case CommVar.FolderType.RemoveableFolder:
                                    infoBar1.SetDetailMode(ccf, InfoBarDetailMode.Removable);
                                    break;
                                case CommVar.FolderType.LocalFolder:
                                    infoBar1.SetDetailMode(ccf, InfoBarDetailMode.LocalFolder);
                                    break;
                            }
                        }
                        break;
                    default:
                        {
                            ccf = Core.Functions.GetSelectedFolder(PublicVar.root, tn);
                            if (Item.Index + 1 > discTreeView1.SelectedNode.Nodes.Count)
                                infoBar1.SetDetailMode((CFile)Item.Tag, InfoBarDetailMode.File);
                            else
                                infoBar1.SetDetailMode((CFolder)Item.Tag, InfoBarDetailMode.Folder);
                            //if (Item.Index + 1 > discTreeView1.SelectedNode.Nodes.Count)
                            //    infoBar1.SetDetailMode(ccf.Files[Item.Index - ccf.Folders.Count], InfoBarDetailMode.File);
                            //else
                            //    infoBar1.SetDetailMode(ccf.Folders[Item.Index], InfoBarDetailMode.Folder);
                        }
                        break;
                }
            }
        }

        private void discListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!discTreeView1.Nodes[2].IsSelected)
            {
                if (discTreeView1.SelectedNode.Nodes.Count >= rightPanel1.ListView.GetItemAt(e.X, e.Y).Index + 1)
                    discTreeView1.SelectedNode =
                        discTreeView1.SelectedNode.Nodes[rightPanel1.ListView.GetItemAt(e.X, e.Y).Index];
                else
                {
                    try
                    {
                        System.Diagnostics.Process.Start(rightPanel1.ListView.GetItemAt(e.X, e.Y).Name);
                    }
                    catch
                    {

                    }
                }
            }
            else
            {
                string[] temp = rightPanel1.ListView.SelectedItems[0].Name.Split('|');
                bool IsFolder = temp[0] == "1" ? true : false;
                int j = IsFolder ? temp.Length : temp.Length - 1;
                TreeNode tn = discTreeView1.Nodes[int.Parse(temp[1])];
                for (int i = 2; i < j; i++)
                    tn = tn.Nodes[int.Parse(temp[i])];

                if (!IsFolder)
                {
                    CFolder ccf = Core.Functions.GetSelectedFolder(PublicVar.root, tn);
                    rightPanel1.ListView.HighlightItem = ccf.Folders.Count + int.Parse(temp[temp.Length - 1]);
                }

                discTreeView1.SelectedNode = tn;
            }
        }

        #endregion

        #region "Search Code..."

        /// <summary>
        /// TreeNodeTrack: 进行查找操作时记录查找的节点路径。
        /// </summary>
        private readonly ArrayList TreeNodeTrack = new ArrayList();

        /// <summary>
        /// SearchNode: 进行查找操作时记录下的查找起始节点。
        /// </summary>
        private TreeNode SearchNode;

        /// <summary>
        /// SH: 保存查找的历史记录。
        /// </summary>
        private readonly SearchHistory SH = new SearchHistory();

        private SearchEngine SE;

        private void travelBar1_OnSearchBarKeyword(object sender, SearchBarEventArgs e)
        {
            if(!rightPanel1.ListView.Visible)
                return;

            if (e.Keyword == "")
            {
                try
                {
                    SearchNode = discTreeView1.Nodes[int.Parse(TreeNodeTrack[0].ToString())];
                    if (TreeNodeTrack.Count > 1)
                    {
                        int i = 1;
                        while (i < TreeNodeTrack.Count)
                        {
                            SearchNode = SearchNode.Nodes[int.Parse(TreeNodeTrack[i].ToString())];
                            i++;
                        }
                    }
                    discTreeView1_AfterSelect(this, new TreeViewEventArgs(SearchNode, TreeViewAction.ByMouse));
                    discTreeView1.Nodes[2].Text = "Search Result";
                }
                catch { }
                return;
            }

            try
            {
                if (SH.Location == null) SH.Location = travelBar1.Address;
                SH.Keyword = e.Keyword;
                rightPanel1.ListView.Items.Clear();

                if (SearchNode == null)
                {
                    TreeNodeTrack.Clear();
                    SearchNode = discTreeView1.SelectedNode;
                    while (SearchNode != null)
                    {
                        TreeNodeTrack.Add(SearchNode.Index);
                        SearchNode = SearchNode.Parent;
                    }
                    SearchNode = discTreeView1.SelectedNode;
                    TreeNodeTrack.Reverse();
                }

                discTreeView1.Nodes[2].Text = "Search Result of  \"" + e.Keyword + "\"";
                discTreeView1.Nodes[2].Tag = null;
                travelBar1.Address = discTreeView1.Nodes[2].Text;
            }
            catch { }

            SearchType st = new SearchType();
            switch(TreeNodeTrack.Count)
            {
                case 1:
                    st = SearchType.Root;break;
                case 2:
                    st = SearchType.Collection;break;
                default:
                    st = SearchType.Folder;break;
            }

            if (SE != null)
            {
                SE.Stop();
                SE.Reset(e.Keyword);
                SE.Start(true);
            }
            else
            {
                SE = new SearchEngine(PublicVar.root, new SearchPosition(st, TreeNodeTrack), e.Keyword);
                SE.OnSearchResultItemAdd += SearchResult_Add;
                SE.OnSearchFinished += SearchFinished;
                SE.Start(true);
            }
        }

        private void SearchFinished(object sender, EventArgs e)
        {
            infoBar1.SetImageFromIconIndex(discTreeView1.Nodes[2].ImageIndex);

            SearchHistory sh = new SearchHistory();
            sh.Historys = new ListViewItem[rightPanel1.ListView.Items.Count];
            rightPanel1.ListView.Items.CopyTo(sh.Historys, 0);
            sh.Keyword = SH.Keyword;
            sh.Location = SH.Location;

            infoBar1.SetDetailMode(sh, InfoBarDetailMode.SearchResult);
        }

        private void SearchEnded(object sender, EventArgs e)
        {
            if (rightPanel1.ListView.Items.Count > 0)
            {
                SearchHistory sh = new SearchHistory();
                SH.Historys = new ListViewItem[rightPanel1.ListView.Items.Count];
                sh.Historys = new ListViewItem[rightPanel1.ListView.Items.Count];

                rightPanel1.ListView.Items.CopyTo(SH.Historys, 0);
                rightPanel1.ListView.Items.CopyTo(sh.Historys, 0);

                sh.Keyword = SH.Keyword;
                sh.Location = SH.Location;

                TreeNode tn = new TreeNode(discTreeView1.Nodes[2].Text);
                tn.Name = "Search";
                tn.Tag = sh;
                travelBar1.TB.AddStep(discTreeView1.Nodes[2].Text, tn);
                SearchNode = null;
            }
            else
                discTreeView1.Nodes[2].Text = "Search Result";

            discTreeView1.SelectedNode = discTreeView1.Nodes[2];
        }

        private void SearchResult_Add(object sender, SearchResultItemEventArgs e)
        {
            try
            {
                if (e.Item.MatchTime == 0) return;
                if (travelBar1.SearchKeyword.Trim() == "") return;

                int index1 = (int)e.Item.Item[1];
                int index2 = (int)e.Item.Item[2];

                switch (e.Item.Item.Count)
                {
                    case 3:
                        {
                            ListViewItem lvi = rightPanel1.ListView.Items.Add(e.Item.ToString(), PublicVar.root.Collection[index1][index2].Name,
                                                    PublicVar.root.Collection[index1][index2].IconIndex);
                            lvi.Tag = PublicVar.root.Collection[index1][index2];
                            switch (index1)
                            {
                                case 0:
                                    lvi.SubItems.Add("Compact Disc");
                                    break;
                                case 1:
                                    lvi.SubItems.Add("Removable Driver");
                                    break;
                                case 2:
                                    lvi.SubItems.Add("Local Folder");
                                    break;
                            }
                            lvi.SubItems.Add(Core.Functions.SizeStr(PublicVar.root.Collection[index1][index2].Size)).Tag = PublicVar.root.Collection[index1][index2].Size;
                        }
                        break;
                    default:
                        {
                            ArrayList index = (ArrayList)e.Item.Item.Clone();

                            CFolder ccf = PublicVar.root.Collection[index1][index2];
                            index.RemoveAt(0);
                            index.RemoveAt(0);
                            index.RemoveAt(0);

                            for (int j = 0; j < index.Count; j++)
                            {
                                if (j == index.Count - 1)
                                {
                                    if (e.Item.IsFolder)
                                    {
                                        ccf = ccf.Folders[(int)index[j]];
                                        ListViewItem lvi = rightPanel1.ListView.Items.Add(e.Item.ToString(), ccf.Name, ccf.IconIndex);
                                        lvi.Tag = ccf;
                                        lvi.SubItems.Add("File Folder");
                                        lvi.SubItems.Add(Core.Functions.SizeStr(ccf.Size)).Tag = ccf.Size;
                                        return;
                                    }
                                    else
                                    {
                                        CFile cf = ccf.Files[(int)index[j]];
                                        ListViewItem lvi = rightPanel1.ListView.Items.Add(e.Item.ToString(), cf.Name, cf.IconIndex);
                                        lvi.Tag = ccf;
                                        lvi.SubItems.Add(cf.Type);
                                        lvi.SubItems.Add(Core.Functions.SizeStr(cf.Size)).Tag = cf.Size;
                                        return;
                                    }
                                }
                                ccf = ccf.Folders[(int)index[j]];
                            }
                        }
                        break;
                }
            }
            catch { }
        }

        #endregion

        #region "Action on unload..."

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (PublicVar.root.Options.StartupPage == 2)
            {
                TreeNode tn = discTreeView1.SelectedNode;
                PublicVar.root.Options.LastPosition.Clear();
                while (tn != null)
                {
                    PublicVar.root.Options.LastPosition.Add(tn.Index);
                    tn = tn.Parent;
                }
                PublicVar.root.Options.LastPosition.Reverse();
                if (!DataChanged)
                {
                    CRoot.Save(PublicVar.root);
                    return;
                }
            }

            if (DataChanged)
            {
                switch (MessageBox.Show("Your data is changed, do you want to save your data before exit?", "Exit",
                                        MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        DataProcess(myToolBar1.Buttons[4].DropDownMenu.MenuItems[0], new EventArgs());
                        break;
                    case System.Windows.Forms.DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                    case DialogResult.No:
                        {
                            PublicVar.backuproot.Options = PublicVar.root.Options;
                            CRoot.Save(PublicVar.backuproot);
                        } 
                        break;
                }
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Directory.Delete(CommVar.TempFolder, true);
            }
            catch { }
        }

        #endregion
    }
}
