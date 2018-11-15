using System;
using System.Collections;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using DiscManageMaster.Core;
using DiscManageMaster.Core.Classes;
using VistaControls;
using API=DiscManageMaster.Core.API;
using ListView=VistaControls.ListView;

namespace DiscManageMaster.Controls
{
    public class DiscListView : ListView
    {
        private CFolder thisContent = new CFolder();
        private bool thisUseVistaIcon = false;
        private Thread FillContentThread;
        private View thisView = View.Details;

        private int currentCol = -1;
        private bool sort;

        public delegate void ListViewFilled(object sender, FillListViewEventArgs e);
        public event ListViewFilled OnListViewFilled;

        public DiscListView()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            SystemImageListHelper.SetListViewImageList(this, CommVar.sil_s, false);
            API.SendMessage(Handle, API.LVM_SETVIEW, API.LV_VIEW_SMALLICON, 0);
            this.ColumnClick += ListView_ColumnClick;
        }

        public CFolder Content
        {
            get { return thisContent; }
            set
            {
                thisContent = value;
                if (thisContent == null) return;

                if (CommVar.UseThread)
                {
                    try
                    {
                        if (FillContentThread.IsAlive)
                            FillContentThread.Abort();
                    }
                    catch { }

                    try
                    {
                        FillContentThread = new Thread(FillContent);
                        FillContentThread.IsBackground = true;
                        FillContentThread.Start();
                    }
                    catch
                    {
                        FillContentThread.Abort();
                    }
                }
                else
                {
                    FillContent();
                }
            }
        }

        public int HighlightItem = -1;
        private void FillContent()
        {
            BeginUpdate();
            try
            {
                ListViewItem[] lvis = new ListViewItem[thisContent.Folders.Count + thisContent.Files.Count];
                int k = 0;
                ArrayList hiddenitem = new ArrayList();

                for (int i = 0; i < thisContent.Folders.Count; i++)
                {
                    lvis[k] = new ListViewItem(thisContent.Folders[i].Name, thisContent.Folders[i].IconIndex);
                    lvis[k].SubItems.Add(thisContent.Folders[i].FolderType == CommVar.FolderType.ExtractFolder ? "Extract File Folder" : "File Folder");
                    lvis[k].SubItems.Add(Functions.SizeStr(thisContent.Folders[i].Size)).Tag = thisContent.Folders[i].Size;
                    lvis[k].Tag = thisContent.Folders[i];
                    if (thisContent.Folders[i].Hidden) hiddenitem.Add(i);
                    k++;
                }

                for (int j = 0; j < thisContent.Files.Count; j++)
                {
                    if (thisContent.Files[j].SpecialItem)
                        continue;

                    int iconindex = -1;
                    switch (thisContent.Files[j].Extension.ToLower())
                    { 
                        case ".htm":
                            iconindex = CommVar.HtmIconIndex; break;
                        case ".html":
                            iconindex = CommVar.HtmlIconIndex; break;
                        case ".mht":
                            iconindex = CommVar.MhtIconIndex; break;
                        case ".xml":
                            iconindex = CommVar.XmlIconIndex; break;
                        default:
                            iconindex = thisContent.Files[j].IconIndex;break;
                    }

                    lvis[k] = new ListViewItem(thisContent.Files[j].Name, iconindex);
                    lvis[k].SubItems.Add(thisContent.Files[j].Type);
                    lvis[k].SubItems.Add(Functions.SizeStr(thisContent.Files[j].Size)).Tag = thisContent.Files[j].Size;
                    lvis[k].Tag = thisContent.Files[j];

                    if (thisContent.Files[j].Path != "")
                        lvis[k].Name = thisContent.Files[j].Path;

                    if (thisContent.Files[j].Hidden)
                        hiddenitem.Add(thisContent.Folders.Count + j);
                    
                    k++;
                }

                Array.Resize(ref lvis, k);
                Items.AddRange(lvis);

                if(HighlightItem != -1)
                {
                    Items[HighlightItem].EnsureVisible();
                    Items[HighlightItem].Selected = true;
                    HighlightItem = -1;
                }

                OnListViewFilled(this, new FillListViewEventArgs(hiddenitem));
            }
            catch
            {
            }
            EndUpdate();
        }

        public bool UseVistaIcon
        {
            get { return thisUseVistaIcon;}
            set
            {
                thisUseVistaIcon = value;
                if(!CommVar.IsVista)
                    thisUseVistaIcon = false;
            }
        }

        public new View View
        {
            get { return thisView; }
            set
            {
                thisView = value;

                switch (thisView)
                {
                    case View.LargeIcon:
                        {
                            if (thisUseVistaIcon)
                                SystemImageListHelper.SetListViewImageList(this, CommVar.sil_v, false);
                            else
                                SystemImageListHelper.SetListViewImageList(this, CommVar.sil_l, false);
                            API.SendMessage(Handle, API.LVM_SETVIEW, API.LV_VIEW_ICON, 0);
                        }
                        break;
                    case View.Tile:
                        {
                            SystemImageListHelper.SetListViewImageList(this, CommVar.sil_x, false);
                            API.SendMessage(Handle, API.LVM_SETVIEW, API.LV_VIEW_TILE, 0);
                        }
                        break;
                    case View.SmallIcon:
                        {
                            SystemImageListHelper.SetListViewImageList(this, CommVar.sil_s, false);
                            API.SendMessage(Handle, API.LVM_SETVIEW, API.LV_VIEW_SMALLICON, 0);
                        }
                        break;
                    case View.List:
                        {
                            SystemImageListHelper.SetListViewImageList(this, CommVar.sil_s, false);
                            API.SendMessage(Handle, API.LVM_SETVIEW, API.LV_VIEW_LIST, 0);
                        }
                        break;
                    case View.Details:
                        {
                            SystemImageListHelper.SetListViewImageList(this, CommVar.sil_s, false);
                            API.SendMessage(Handle, API.LVM_SETVIEW, API.LV_VIEW_DETAILS, 0);
                        }
                        break;
                }
            }
        }

        private void ListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            string Asc = ((char)0x25bc).ToString().PadLeft(4, ' ');
            string Des = ((char)0x25b2).ToString().PadLeft(4, ' ');

            if (sort == false)
            {
                sort = true;
                string oldStr = Columns[e.Column].Text.TrimEnd((char)0x25bc, (char)0x25b2, ' ');
                Columns[e.Column].Text = oldStr + Des;
            }
            else if (sort == true)
            {
                sort = false;
                string oldStr = Columns[e.Column].Text.TrimEnd((char)0x25bc, (char)0x25b2, ' ');
                Columns[e.Column].Text = oldStr + Asc;
            }

            ListViewItemSorter = new ListViewItemComparer(e.Column, sort);
            Sort();
            int rowCount = Items.Count;
            if (currentCol != -1)
            {
                for (int i = 0; i < rowCount; i++)
                {
                    Items[i].UseItemStyleForSubItems = false;
                    Items[i].SubItems[currentCol].BackColor = Color.White;

                    if (e.Column != currentCol)
                        Columns[currentCol].Text = Columns[currentCol].Text.TrimEnd((char)0x25bc, (char)0x25b2, ' ');
                }
            }

            currentCol = e.Column;
        }
    }

    public class FillListViewEventArgs : EventArgs
    {
        public ArrayList HiddenItem;

        public FillListViewEventArgs(ArrayList hiddenitem)
        {
            HiddenItem = hiddenitem;
        }
    }

    public class ListViewItemComparer : IComparer
    {
        public bool sort_b;
        public SortOrder order = SortOrder.Ascending;

        private int col;

        public ListViewItemComparer()
        {
            col = 0;
        }

        public ListViewItemComparer(int column, bool sort)
        {
            col = column;
            sort_b = sort;
        }

        public int Compare(object x, object y)
        {
            CFolder x_folder;
            CFolder y_folder;
            CFile x_file;
            CFile y_file;
            bool xx = false;
            bool yy = false;

            try
            {
                x_folder = (CFolder)((ListViewItem)x).Tag;
                xx = true;
            }
            catch
            {
                x_file = (CFile)((ListViewItem)x).Tag;
            }

            try
            {
                y_folder = (CFolder)((ListViewItem)y).Tag;
                yy = true;
            }
            catch
            {
                y_file = (CFile)((ListViewItem)y).Tag;
            }

            switch (col)
            {
                case 0:
                    {
                        if (sort_b)
                        {
                            if (xx == true && yy == false)
                                return 1;
                            if (xx == false && yy == true)
                                return -1;
                            return String.Compare(((ListViewItem)x).Text, ((ListViewItem)y).Text);
                        }
                        else
                        {
                            if (xx == true && yy == false)
                                return -1;
                            if (xx == false && yy == true)
                                return 1;
                            return String.Compare(((ListViewItem)y).Text, ((ListViewItem)x).Text);
                        }
                    } 
                    //break;
                case 1:
                    {
                        if (sort_b)
                        {
                            return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
                        }
                        else
                        {
                            return String.Compare(((ListViewItem)y).SubItems[col].Text, ((ListViewItem)x).SubItems[col].Text);
                        }
                    } 
                    //break;
                case 2:
                    {
                        long a = long.Parse(((ListViewItem)x).SubItems[col].Tag.ToString());
                        long b = long.Parse(((ListViewItem)y).SubItems[col].Tag.ToString());

                        if (sort_b)
                        {
                            if (a == b) return 0;
                            if (a > b) return 1;
                            return -1;
                        }
                        else
                        {
                            if (a == b) return 0;
                            if (a > b) return -1;
                            return 1;
                        }
                    } 
                    //break;
            }

            return 0;
        }
    }
}