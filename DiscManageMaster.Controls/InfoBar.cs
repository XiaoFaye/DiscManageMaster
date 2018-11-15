using System;
using System.Drawing;
using System.Windows.Forms;
using DiscManageMaster.Core;
using DiscManageMaster.Core.Classes;

namespace DiscManageMaster.Controls
{
    public partial class InfoBar : UserControl
    {
        private object InfoBarDetailModeObject;
        private InfoBarDetailMode thisDetailMode;
        public InfoBarDetailMode DetailMode
        {
            get { return thisDetailMode; }
            set
            {
                thisDetailMode = value;
                //if (InfoBarDetailModeObject == null && thisDetailMode != InfoBarDetailMode.Welcome)
                //    return;
                switch (thisDetailMode)
                {
                    case InfoBarDetailMode.File:
                        {
                            if (InfoBarDetailModeObject == null)
                                return;
                            ButtonUpdate.Visible = false;
                            MemoPanel.Visible = false;
                            DetailPanel.Visible = true;
                            DetailPanel.RowStyles[0].Height = 0;
                            DetailPanel.RowStyles[1].Height = 20;
                            DetailPanel.RowStyles[2].Height = 20;
                            DetailPanel.RowStyles[3].Height = 20;
                            DetailPanel.RowStyles[4].Height = 0;
                            DetailPanel.Height = 60;

                            CFile cf = (CFile) InfoBarDetailModeObject;
                            NameText = cf.Name;
                            TypeText = cf.Type;
                            ModifiedDateText = cf.LastModifiedTime.ToString();
                            SizeText = Core.Functions.SizeStr(cf.Size);
                            if (cf.CreatedTime.Year == 0001)
                            {
                                DetailPanel.RowStyles[3].Height = 0;
                                DetailPanel.Height = 40;
                            }
                            else
                                CreatedDateText = cf.CreatedTime.ToString();

                            cf = null;
                        }
                        break;
                    case InfoBarDetailMode.Folder:
                        {
                            if (InfoBarDetailModeObject == null)
                                return;
                            ButtonUpdate.Visible = false;
                            MemoPanel.Visible = false;
                            DetailPanel.Visible = true;
                            DetailPanel.RowStyles[0].Height = 20;
                            DetailPanel.RowStyles[1].Height = 0;
                            DetailPanel.RowStyles[2].Height = 20;
                            DetailPanel.RowStyles[3].Height = 20;
                            DetailPanel.RowStyles[4].Height = 0;
                            DetailPanel.Height = 60;

                            CFolder ccf = (CFolder) InfoBarDetailModeObject;
                            NameText = ccf.Name;
                            TypeText = ccf.FolderType == CommVar.FolderType.ExtractFolder ? "Extract File Folder" : "File Folder";
                            ContainsText = ccf.FileCount + (ccf.FileCount > 1 ? " Files, " : " File, ") + ccf.FolderCount + (ccf.FolderCount > 1 ? " Folders" : " Folder");
                            SizeText = Functions.SizeStr(ccf.Size);
                            CreatedDateText = ccf.CreatedTime.ToString();

                            ccf = null;
                        }
                        break;
                    case InfoBarDetailMode.Disc:
                        {
                            if (InfoBarDetailModeObject == null)
                                return;
                            ButtonUpdate.Visible = false;
                            MemoPanel.Visible = true;
                            DetailPanel.Visible = true;
                            DetailPanel.RowStyles[0].Height = 20;
                            DetailPanel.RowStyles[1].Height = 0;
                            DetailPanel.RowStyles[2].Height = 20;
                            DetailPanel.RowStyles[3].Height = 20;
                            DetailPanel.RowStyles[4].Height = 0;
                            DetailPanel.Height = 60;

                            CFolder ccf = (CFolder) InfoBarDetailModeObject;
                            NameText = ccf.Name;
                            TypeText = "Compact Disc";
                            ContainsText = ccf.FileCount + (ccf.FileCount > 1 ? " Files, " : " File, ") + ccf.FolderCount + (ccf.FolderCount > 1 ? " Folders" : " Folder");
                            SizeText = Core.Functions.SizeStr(ccf.Size);
                            CreatedDateText = ccf.CreatedTime.ToString();
                            Memo = ccf.Memo;

                            ccf = null;
                        }
                        break;
                    case InfoBarDetailMode.Removable:
                        {
                            if (InfoBarDetailModeObject == null)
                                return;
                            ButtonUpdate.Visible = true;
                            MemoPanel.Visible = true;
                            DetailPanel.Visible = true;
                            DetailPanel.RowStyles[0].Height = 20;
                            DetailPanel.RowStyles[1].Height = 0;
                            DetailPanel.RowStyles[2].Height = 20;
                            DetailPanel.RowStyles[3].Height = 20;
                            DetailPanel.RowStyles[4].Height = 0;
                            DetailPanel.Height = 60;

                            CFolder ccf = (CFolder) InfoBarDetailModeObject;
                            NameText = ccf.Name;
                            TypeText = "Removable Drive";
                            ContainsText = ccf.FileCount + (ccf.FileCount > 1 ? " Files, " : " File, ") + ccf.FolderCount + (ccf.FolderCount > 1 ? " Folders" : " Folder");
                            SizeText = Core.Functions.SizeStr(ccf.Size);
                            CreatedDateText = ccf.CreatedTime.ToString();
                            Memo = ccf.Memo;

                            ccf = null;
                        }
                        break;
                    case InfoBarDetailMode.LocalFolder:
                        {
                            if (InfoBarDetailModeObject == null)
                                return;
                            ButtonUpdate.Visible = true;
                            MemoPanel.Visible = true;
                            DetailPanel.Visible = true;
                            DetailPanel.RowStyles[0].Height = 20;
                            DetailPanel.RowStyles[1].Height = 0;
                            DetailPanel.RowStyles[2].Height = 20;
                            DetailPanel.RowStyles[3].Height = 20;
                            DetailPanel.RowStyles[4].Height = 20;
                            DetailPanel.Height = 80;

                            CFolder ccf = (CFolder) InfoBarDetailModeObject;
                            NameText = ccf.Name;
                            TypeText = "Local Folder";
                            ContainsText = ccf.FileCount + (ccf.FileCount > 1 ? " Files, " : " File, ") + ccf.FolderCount + (ccf.FolderCount > 1 ? " Folders" : " Folder");
                            SizeText = Core.Functions.SizeStr(ccf.Size);
                            CreatedDateText = ccf.CreatedTime.ToString();
                            PathText = ccf.Path;
                            Memo = ccf.Memo;

                            ccf = null;
                        }
                        break;
                    case InfoBarDetailMode.Collection:
                        {
                            if (InfoBarDetailModeObject == null)
                                return;
                            ButtonUpdate.Visible = false;
                            MemoPanel.Visible = false;
                            DetailPanel.Visible = true;
                            DetailPanel.RowStyles[0].Height = 20;
                            DetailPanel.RowStyles[1].Height = 0;
                            DetailPanel.RowStyles[2].Height = 0;
                            DetailPanel.RowStyles[3].Height = 20;
                            DetailPanel.RowStyles[4].Height = 0;
                            DetailPanel.Height = 40;

                            CDiscCollection col = (CDiscCollection) InfoBarDetailModeObject;
                            NameText = col.Name;
                            TypeText = "Data Collection";
                            ContainsText = col.Count + (col.Count > 1 ? " Items" : " Item");
                            CreatedDateText = col.CreatedTime.ToString();

                            col = null;
                        }
                        break;
                    case InfoBarDetailMode.Welcome:
                        {
                            NameText = "Welcome to use Personal Files Manager!";
                            TypeText = "";
                            ButtonUpdate.Visible = false;
                            DetailPanel.Visible = false;
                            MemoPanel.Visible = false;
                        }
                        break;
                    case InfoBarDetailMode.MyCollections:
                        {
                            if (InfoBarDetailModeObject == null)
                                return;
                            ButtonUpdate.Visible = false;
                            MemoPanel.Visible = false;
                            DetailPanel.Visible = true;
                            DetailPanel.RowStyles[0].Height = 20;
                            DetailPanel.RowStyles[1].Height = 0;
                            DetailPanel.RowStyles[2].Height = 0;
                            DetailPanel.RowStyles[3].Height = 20;
                            DetailPanel.RowStyles[4].Height = 0;
                            DetailPanel.Height = 40;

                            NameText = "My Collections";
                            TypeText = "";
                            CRoot root = (CRoot) InfoBarDetailModeObject;
                            ContainsText = root.Collection.Count + " Collections";
                            CreatedDateText = root.CreatedTime.ToString();

                            root = null;
                        }
                        break;
                    case InfoBarDetailMode.SearchResult:
                        {
                            ButtonUpdate.Visible = false;
                            if (InfoBarDetailModeObject == null)
                            {
                                NameText = "There is no Search Result for viewing.";
                                TypeText = "";
                                MemoPanel.Visible = false;
                                DetailPanel.Visible = false;
                            }
                            else
                            {
                                MemoPanel.Visible = false;
                                DetailPanel.Visible = true;
                                DetailPanel.RowStyles[0].Height = 20;
                                DetailPanel.RowStyles[1].Height = 0;
                                DetailPanel.RowStyles[2].Height = 0;
                                DetailPanel.RowStyles[3].Height = 0;
                                DetailPanel.RowStyles[4].Height = 20;
                                DetailPanel.Height = 40;

                                SearchHistory SH = (SearchHistory) InfoBarDetailModeObject;
                                NameText = "Search Result of \"" + SH.Keyword + "\"";
                                TypeText = "";
                                int foldercount = 0;
                                int filecount = 0;
                                for (int i = 0; i < SH.Historys.Length; i++)
                                {
                                    if (SH.Historys[i].Name.Substring(0, 1) == "1")
                                        foldercount++;
                                    else
                                        filecount++;
                                }
                                ContainsText = filecount + (filecount > 1 ? " Files, " : " File, ") + foldercount + (foldercount > 1 ? " Folders" : " Folder");
                                PathText = SH.Location;

                                SH = null;
                            }
                        }
                        break;
                    case InfoBarDetailMode.Options:
                        {
                            NameText = "Please save Options after you changed it.";
                            TypeText = "";
                            ButtonUpdate.Visible = false;
                            DetailPanel.Visible = false;
                            MemoPanel.Visible = false;
                        }
                        break;
                    case InfoBarDetailMode.Help:
                        {
                            if (((TreeNode)InfoBarDetailModeObject).Tag == null)
                                NameText = "Please select Help Topic from left.";
                            else
                                NameText = ((TreeNode)InfoBarDetailModeObject).Text;
                            TypeText = "";
                            ButtonUpdate.Visible = false;
                            DetailPanel.Visible = false;
                            MemoPanel.Visible = false;
                        }
                        break;
                    case InfoBarDetailMode.About:
                        {
                            NameText = "About US-SOFT.COM";
                            TypeText = "";
                            ButtonUpdate.Visible = false;
                            DetailPanel.Visible = false;
                            MemoPanel.Visible = false;
                        }
                        break;
                }
                InfoBarDetailModeObject = null;
            }
        }

        public void SetDetailMode(object sender, InfoBarDetailMode dm)
        {
            InfoBarDetailModeObject = sender;
            DetailMode = dm;
        }

        public string NameText
        {
            get { return LabelName.Text;}
            set { LabelName.Text = value; }
        }

        public string TypeText
        {
            get { return LabelType.Text; }
            set { LabelType.Text = value; }
        }

        public string ModifiedDateText
        {
            get { return LabelModifiedDate.Text; }
            set { LabelModifiedDate.Text = value; }
        }

        public string SizeText
        {
            get { return LabelSize.Text; }
            set { LabelSize.Text = value; }
        }

        public string CreatedDateText
        {
            get { return LabelCreatedDate.Text; }
            set { LabelCreatedDate.Text = value; }
        }

        public string ContainsText
        {
            get { return LabelContains.Text; }
            set { LabelContains.Text = value; }
        }

        public string PathText
        {
            get { return LabelPath.Text; }
            set { LabelPath.Text = value; }
        }

        public string Memo
        {
            get { return LabelMemo.Text; }
            set { LabelMemo.Text = value; }
        }

        public Button Update
        {
            get { return ButtonUpdate; }
        }

        public InfoBar()
        {
            InitializeComponent();
        }

        public Image Image
        {
            get { return pictureBox1.Image; }
            set
            {
                pictureBox1.Image = value;

                try
                {
                    if (pictureBox1.Image.Height == 256 || pictureBox1.Image.Height != pictureBox1.Image.Width)
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    else
                        pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                }catch{}
            }
        }

        private int CurrentImageIconIndex = -1;
        public void SetImageFromIconIndex(int iconindex)
        {
            if (iconindex == CurrentImageIconIndex || iconindex == -1)
                return;

            Bitmap bmp = CommVar.sil_v.Icon(iconindex).ToBitmap();
            if (CommVar.IsVista)
            {
                if (Functions.IsRightVistaIcon(bmp))
                    Image = bmp;
                else
                    Image = CommVar.sil_x.Icon(iconindex).ToBitmap();
            }
            else
                Image = bmp;

            CurrentImageIconIndex = iconindex;
        }

        private void InfoBar_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            DetailMode = InfoBarDetailMode.File;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            pictureBox1.Location = new Point(5, 5);
            pictureBox1.Size = new Size(Height - 10, Height - 10);

            LabelName.Location = new Point(pictureBox1.Width + 10, 5);
            LabelType.Location = new Point(LabelName.Location.X, LabelName.Location.Y + LabelName.Height + 5);

            ButtonUpdate.Location = new Point(LabelName.Location.X + LabelName.Width + 150, LabelName.Location.Y + LabelName.Height / 2);

            if (Height - LabelType.Location.Y - LabelType.Height - 10 > DetailPanel.Height)
                DetailPanel.Location = new Point(LabelName.Location.X + 20, LabelType.Location.Y + LabelType.Height + 5);
            else
                DetailPanel.Location = new Point(LabelType.Location.X + LabelType.Width + 15, LabelType.Location.Y + 5);

            MemoPanel.Height = Height - DetailPanel.Location.Y - 10;
            MemoPanel.Width = 300;
            MemoPanel.Location = new Point(Width - MemoPanel.Width - 10, DetailPanel.Location.Y);
        }

        public int RealHeight
        {
            get { return LabelName.Height + LabelType.Height + DetailPanel.Height; }
        }
    }

    public enum InfoBarDetailMode
    {
        File,
        Folder,
        Disc,
        Removable,
        LocalFolder,
        Collection,
        Welcome,
        MyCollections,
        SearchResult,
        Options,
        Help,
        About
    }
}