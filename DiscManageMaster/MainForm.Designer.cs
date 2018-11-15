namespace DiscManageMaster
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.travelBar1 = new DiscManageMaster.Controls.TravelBar();
            this.myToolBar1 = new DiscManageMaster.Controls.MyToolBar();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.discTreeView1 = new DiscManageMaster.Controls.DiscTreeView();
            this.rightPanel1 = new DiscManageMaster.Controls.RightPanel();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.infoBar1 = new DiscManageMaster.Controls.InfoBar();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.MaximumSize = new System.Drawing.Size(0, 73);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.travelBar1);
            this.splitContainer1.Panel1MinSize = 29;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.myToolBar1);
            this.splitContainer1.Panel2MinSize = 40;
            this.splitContainer1.Size = new System.Drawing.Size(1079, 73);
            this.splitContainer1.SplitterDistance = 29;
            this.splitContainer1.TabIndex = 10;
            // 
            // travelBar1
            // 
            this.travelBar1.Address = "";
            this.travelBar1.BackColor = System.Drawing.SystemColors.Control;
            this.travelBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.travelBar1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.travelBar1.Location = new System.Drawing.Point(0, 0);
            this.travelBar1.Name = "travelBar1";
            this.travelBar1.SearchKeyword = "";
            this.travelBar1.Size = new System.Drawing.Size(1079, 29);
            this.travelBar1.TabIndex = 7;
            this.travelBar1.OnSearchBarKeyword += new DiscManageMaster.Controls.TravelBar.SearchBarKeyword(this.travelBar1_OnSearchBarKeyword);
            // 
            // myToolBar1
            // 
            this.myToolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.myToolBar1.AutoSize = false;
            this.myToolBar1.BackColor = System.Drawing.Color.Black;
            this.myToolBar1.Divider = false;
            this.myToolBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myToolBar1.DropDownArrows = true;
            this.myToolBar1.Location = new System.Drawing.Point(0, 0);
            this.myToolBar1.Name = "myToolBar1";
            this.myToolBar1.ShowToolTips = true;
            this.myToolBar1.Size = new System.Drawing.Size(1079, 40);
            this.myToolBar1.TabIndex = 8;
            this.myToolBar1.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
            this.myToolBar1.Wrappable = false;
            this.myToolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.myToolBar1_ButtonClick);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.discTreeView1);
            this.splitContainer2.Panel1MinSize = 250;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.rightPanel1);
            this.splitContainer2.Panel2MinSize = 100;
            this.splitContainer2.Size = new System.Drawing.Size(1079, 430);
            this.splitContainer2.SplitterDistance = 359;
            this.splitContainer2.TabIndex = 11;
            // 
            // discTreeView1
            // 
            this.discTreeView1.AllowDrop = true;
            this.discTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.discTreeView1.HotTracking = true;
            this.discTreeView1.ItemHeight = 18;
            this.discTreeView1.LabelEdit = true;
            this.discTreeView1.Location = new System.Drawing.Point(0, 0);
            this.discTreeView1.Name = "discTreeView1";
            this.discTreeView1.Root = null;
            this.discTreeView1.ShowLines = false;
            this.discTreeView1.Size = new System.Drawing.Size(359, 430);
            this.discTreeView1.TabIndex = 0;
            this.discTreeView1.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.discTreeView1_AfterLabelEdit);
            this.discTreeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.discTreeView1_AfterSelect);
            this.discTreeView1.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.discTreeView1_BeforeLabelEdit);
            // 
            // rightPanel1
            // 
            this.rightPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPanel1.Location = new System.Drawing.Point(0, 0);
            this.rightPanel1.Name = "rightPanel1";
            this.rightPanel1.Size = new System.Drawing.Size(716, 430);
            this.rightPanel1.TabIndex = 2;
            this.rightPanel1.View = DiscManageMaster.Controls.RightPanelView.Welcome;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer3.Panel1MinSize = 160;
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.infoBar1);
            this.splitContainer3.Panel2MinSize = 48;
            this.splitContainer3.Size = new System.Drawing.Size(1079, 605);
            this.splitContainer3.SplitterDistance = 430;
            this.splitContainer3.SplitterWidth = 1;
            this.splitContainer3.TabIndex = 12;
            // 
            // infoBar1
            // 
            this.infoBar1.BackColor = System.Drawing.SystemColors.Window;
            this.infoBar1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("infoBar1.BackgroundImage")));
            this.infoBar1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.infoBar1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.infoBar1.ContainsText = "label5";
            this.infoBar1.CreatedDateText = "label6";
            this.infoBar1.Cursor = System.Windows.Forms.Cursors.Default;
            this.infoBar1.DetailMode = DiscManageMaster.Controls.InfoBarDetailMode.File;
            this.infoBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoBar1.Image = null;
            this.infoBar1.Location = new System.Drawing.Point(0, 0);
            this.infoBar1.ModifiedDateText = "label4";
            this.infoBar1.Name = "infoBar1";
            this.infoBar1.NameText = "Goods.txt";
            this.infoBar1.PathText = "label6";
            this.infoBar1.Size = new System.Drawing.Size(1079, 174);
            this.infoBar1.SizeText = "label5";
            this.infoBar1.TabIndex = 9;
            this.infoBar1.TypeText = "label2";
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer4.IsSplitterFixed = true;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.splitContainer1);
            this.splitContainer4.Panel1MinSize = 73;
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer4.Size = new System.Drawing.Size(1079, 682);
            this.splitContainer4.SplitterDistance = 73;
            this.splitContainer4.TabIndex = 13;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1079, 682);
            this.Controls.Add(this.splitContainer4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 650);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.DiscTreeView discTreeView1;
        private Controls.TravelBar travelBar1;
        private Controls.MyToolBar myToolBar1;
        private Controls.InfoBar infoBar1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private Controls.RightPanel rightPanel1;

    }
}