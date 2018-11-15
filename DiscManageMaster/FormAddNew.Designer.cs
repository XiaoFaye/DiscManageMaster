namespace DiscManageMaster
{
    partial class FormAddNew
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddNew));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Server.2008 (C:)", 5, 5);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Document (D:)", 6, 6);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Backup (E:)", 6, 6);
            this.TabAddNew = new System.Windows.Forms.TabControl();
            this.TabAddNewCD = new System.Windows.Forms.TabPage();
            this.ListViewCD = new DiscManageMaster.Controls.DiscListView();
            this.TabAddNewRemovable = new System.Windows.Forms.TabPage();
            this.ListViewRemovable = new DiscManageMaster.Controls.DiscListView();
            this.TabAddNewLocalFolder = new System.Windows.Forms.TabPage();
            this.TreeViewLocalFolder = new DiscManageMaster.Controls.LocalDiskTreeView();
            this.TextBoxMemo = new System.Windows.Forms.TextBox();
            this.ButtonOK = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.ButtonRefresh = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ListViewPlugins = new DiscManageMaster.Controls.DiscListView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TabAddNew.SuspendLayout();
            this.TabAddNewCD.SuspendLayout();
            this.TabAddNewRemovable.SuspendLayout();
            this.TabAddNewLocalFolder.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabAddNew
            // 
            this.TabAddNew.Controls.Add(this.TabAddNewCD);
            this.TabAddNew.Controls.Add(this.TabAddNewRemovable);
            this.TabAddNew.Controls.Add(this.TabAddNewLocalFolder);
            this.TabAddNew.Location = new System.Drawing.Point(6, 19);
            this.TabAddNew.Name = "TabAddNew";
            this.TabAddNew.SelectedIndex = 0;
            this.TabAddNew.Size = new System.Drawing.Size(274, 253);
            this.TabAddNew.TabIndex = 0;
            this.TabAddNew.Selected += new System.Windows.Forms.TabControlEventHandler(this.TabAddNew_Selected);
            // 
            // TabAddNewCD
            // 
            this.TabAddNewCD.Controls.Add(this.ListViewCD);
            this.TabAddNewCD.Location = new System.Drawing.Point(4, 22);
            this.TabAddNewCD.Name = "TabAddNewCD";
            this.TabAddNewCD.Padding = new System.Windows.Forms.Padding(3);
            this.TabAddNewCD.Size = new System.Drawing.Size(266, 227);
            this.TabAddNewCD.TabIndex = 0;
            this.TabAddNewCD.Text = "CD";
            this.TabAddNewCD.UseVisualStyleBackColor = true;
            // 
            // ListViewCD
            // 
            //this.ListViewCD.Content = ((DiscManageMaster.Core.Classes.CFolder)(resources.GetObject("ListViewCD.Content")));
            this.ListViewCD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListViewCD.Location = new System.Drawing.Point(3, 3);
            this.ListViewCD.MultiSelect = false;
            this.ListViewCD.Name = "ListViewCD";
            this.ListViewCD.Size = new System.Drawing.Size(260, 221);
            this.ListViewCD.TabIndex = 0;
            this.ListViewCD.UseCompatibleStateImageBehavior = false;
            this.ListViewCD.UseVistaIcon = false;
            // 
            // TabAddNewRemovable
            // 
            this.TabAddNewRemovable.Controls.Add(this.ListViewRemovable);
            this.TabAddNewRemovable.Location = new System.Drawing.Point(4, 22);
            this.TabAddNewRemovable.Name = "TabAddNewRemovable";
            this.TabAddNewRemovable.Padding = new System.Windows.Forms.Padding(3);
            this.TabAddNewRemovable.Size = new System.Drawing.Size(266, 227);
            this.TabAddNewRemovable.TabIndex = 1;
            this.TabAddNewRemovable.Text = "Removable";
            this.TabAddNewRemovable.UseVisualStyleBackColor = true;
            // 
            // ListViewRemovable
            // 
            //this.ListViewRemovable.Content = ((DiscManageMaster.Core.Classes.CFolder)(resources.GetObject("ListViewRemovable.Content")));
            this.ListViewRemovable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListViewRemovable.Location = new System.Drawing.Point(3, 3);
            this.ListViewRemovable.MultiSelect = false;
            this.ListViewRemovable.Name = "ListViewRemovable";
            this.ListViewRemovable.Size = new System.Drawing.Size(260, 221);
            this.ListViewRemovable.TabIndex = 0;
            this.ListViewRemovable.UseCompatibleStateImageBehavior = false;
            this.ListViewRemovable.UseVistaIcon = false;
            // 
            // TabAddNewLocalFolder
            // 
            this.TabAddNewLocalFolder.Controls.Add(this.TreeViewLocalFolder);
            this.TabAddNewLocalFolder.Location = new System.Drawing.Point(4, 22);
            this.TabAddNewLocalFolder.Name = "TabAddNewLocalFolder";
            this.TabAddNewLocalFolder.Padding = new System.Windows.Forms.Padding(3);
            this.TabAddNewLocalFolder.Size = new System.Drawing.Size(266, 227);
            this.TabAddNewLocalFolder.TabIndex = 2;
            this.TabAddNewLocalFolder.Text = "LocalFolder";
            this.TabAddNewLocalFolder.UseVisualStyleBackColor = true;
            // 
            // TreeViewLocalFolder
            // 
            this.TreeViewLocalFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeViewLocalFolder.HotTracking = true;
            this.TreeViewLocalFolder.Location = new System.Drawing.Point(3, 3);
            this.TreeViewLocalFolder.Name = "TreeViewLocalFolder";
            treeNode1.ImageIndex = 5;
            treeNode1.Name = "C:\\";
            treeNode1.SelectedImageIndex = 5;
            treeNode1.Text = "Server.2008 (C:)";
            treeNode2.ImageIndex = 6;
            treeNode2.Name = "D:\\";
            treeNode2.SelectedImageIndex = 6;
            treeNode2.Text = "Document (D:)";
            treeNode3.ImageIndex = 6;
            treeNode3.Name = "E:\\";
            treeNode3.SelectedImageIndex = 6;
            treeNode3.Text = "Backup (E:)";
            this.TreeViewLocalFolder.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            this.TreeViewLocalFolder.ShowLines = false;
            this.TreeViewLocalFolder.Size = new System.Drawing.Size(260, 221);
            this.TreeViewLocalFolder.TabIndex = 0;
            // 
            // TextBoxMemo
            // 
            this.TextBoxMemo.Location = new System.Drawing.Point(6, 19);
            this.TextBoxMemo.Multiline = true;
            this.TextBoxMemo.Name = "TextBoxMemo";
            this.TextBoxMemo.Size = new System.Drawing.Size(278, 112);
            this.TextBoxMemo.TabIndex = 2;
            // 
            // ButtonOK
            // 
            this.ButtonOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ButtonOK.Location = new System.Drawing.Point(491, 409);
            this.ButtonOK.Name = "ButtonOK";
            this.ButtonOK.Size = new System.Drawing.Size(93, 33);
            this.ButtonOK.TabIndex = 3;
            this.ButtonOK.Text = "OK";
            this.ButtonOK.UseVisualStyleBackColor = true;
            this.ButtonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ButtonCancel.Location = new System.Drawing.Point(361, 409);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(93, 33);
            this.ButtonCancel.TabIndex = 4;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            // 
            // ButtonRefresh
            // 
            this.ButtonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ButtonRefresh.Location = new System.Drawing.Point(231, 409);
            this.ButtonRefresh.Name = "ButtonRefresh";
            this.ButtonRefresh.Size = new System.Drawing.Size(93, 33);
            this.ButtonRefresh.TabIndex = 5;
            this.ButtonRefresh.Text = "Refresh";
            this.ButtonRefresh.UseVisualStyleBackColor = true;
            this.ButtonRefresh.Click += new System.EventHandler(this.ButtonRefresh_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(83, 278);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(169, 21);
            this.comboBox1.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TabAddNew);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Location = new System.Drawing.Point(10, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(285, 367);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Get new folder from";
            // 
            // label3
            // 
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(21, 316);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(252, 44);
            this.label3.TabIndex = 10;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 281);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Add to:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.ListViewPlugins);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox2.Location = new System.Drawing.Point(313, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(292, 223);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Select plugins";
            // 
            // label1
            // 
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.Location = new System.Drawing.Point(15, 181);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(269, 36);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // ListViewPlugins
            // 
            this.ListViewPlugins.CheckBoxes = true;
            //this.ListViewPlugins.Content = ((DiscManageMaster.Core.Classes.CFolder)(resources.GetObject("ListViewPlugins.Content")));
            this.ListViewPlugins.Location = new System.Drawing.Point(6, 19);
            this.ListViewPlugins.Name = "ListViewPlugins";
            this.ListViewPlugins.Size = new System.Drawing.Size(278, 146);
            this.ListViewPlugins.TabIndex = 1;
            this.ListViewPlugins.UseCompatibleStateImageBehavior = false;
            this.ListViewPlugins.UseVistaIcon = false;
            this.ListViewPlugins.View = System.Windows.Forms.View.Details;
            this.ListViewPlugins.SelectedIndexChanged += new System.EventHandler(this.ListViewPlugins_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.TextBoxMemo);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox3.Location = new System.Drawing.Point(313, 241);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(292, 138);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Folder memo";
            // 
            // FormAddNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 463);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ButtonRefresh);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormAddNew";
            this.Text = "Add New Folder...";
            this.Load += new System.EventHandler(this.FormAddNew_Load);
            this.TabAddNew.ResumeLayout(false);
            this.TabAddNewCD.ResumeLayout(false);
            this.TabAddNewRemovable.ResumeLayout(false);
            this.TabAddNewLocalFolder.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabAddNew;
        private System.Windows.Forms.TabPage TabAddNewCD;
        private System.Windows.Forms.TabPage TabAddNewRemovable;
        private System.Windows.Forms.TabPage TabAddNewLocalFolder;
        private DiscManageMaster.Controls.DiscListView ListViewCD;
        private DiscManageMaster.Controls.DiscListView ListViewRemovable;
        private DiscManageMaster.Controls.LocalDiskTreeView TreeViewLocalFolder;
        private DiscManageMaster.Controls.DiscListView ListViewPlugins;
        private System.Windows.Forms.TextBox TextBoxMemo;
        private System.Windows.Forms.Button ButtonOK;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Button ButtonRefresh;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
    }
}