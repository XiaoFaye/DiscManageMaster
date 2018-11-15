namespace DiscManageMaster.Controls
{
    partial class InfoBar
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.DetailPanel = new System.Windows.Forms.TableLayoutPanel();
            this.MemoPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.LabelMemo = new System.Windows.Forms.Label();
            this.ButtonUpdate = new System.Windows.Forms.Button();
            this.label5 = new DiscManageMaster.Controls.TransparentLabel();
            this.LabelModifiedDate = new DiscManageMaster.Controls.TransparentLabel();
            this.LabelSize = new DiscManageMaster.Controls.TransparentLabel();
            this.LabelCreatedDate = new DiscManageMaster.Controls.TransparentLabel();
            this.LabelContains = new DiscManageMaster.Controls.TransparentLabel();
            this.label3 = new DiscManageMaster.Controls.TransparentLabel();
            this.label4 = new DiscManageMaster.Controls.TransparentLabel();
            this.label2 = new DiscManageMaster.Controls.TransparentLabel();
            this.label1 = new DiscManageMaster.Controls.TransparentLabel();
            this.LabelPath = new DiscManageMaster.Controls.TransparentLabel();
            this.LabelType = new DiscManageMaster.Controls.TransparentLabel();
            this.LabelName = new DiscManageMaster.Controls.TransparentLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.DetailPanel.SuspendLayout();
            this.MemoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBox1.Location = new System.Drawing.Point(36, 33);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(119, 99);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // DetailPanel
            // 
            this.DetailPanel.BackColor = System.Drawing.Color.Transparent;
            this.DetailPanel.ColumnCount = 2;
            this.DetailPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.5955F));
            this.DetailPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.4045F));
            this.DetailPanel.Controls.Add(this.label5, 0, 4);
            this.DetailPanel.Controls.Add(this.LabelModifiedDate, 1, 1);
            this.DetailPanel.Controls.Add(this.LabelSize, 1, 2);
            this.DetailPanel.Controls.Add(this.LabelCreatedDate, 1, 3);
            this.DetailPanel.Controls.Add(this.LabelContains, 1, 0);
            this.DetailPanel.Controls.Add(this.label3, 0, 3);
            this.DetailPanel.Controls.Add(this.label4, 0, 0);
            this.DetailPanel.Controls.Add(this.label2, 0, 2);
            this.DetailPanel.Controls.Add(this.label1, 0, 1);
            this.DetailPanel.Controls.Add(this.LabelPath, 1, 4);
            this.DetailPanel.Location = new System.Drawing.Point(308, 24);
            this.DetailPanel.Name = "DetailPanel";
            this.DetailPanel.RowCount = 5;
            this.DetailPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.DetailPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.DetailPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.DetailPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.DetailPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.DetailPanel.Size = new System.Drawing.Size(356, 108);
            this.DetailPanel.TabIndex = 4;
            // 
            // MemoPanel
            // 
            this.MemoPanel.BackColor = System.Drawing.Color.Transparent;
            this.MemoPanel.ColumnCount = 2;
            this.MemoPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.40936F));
            this.MemoPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.59064F));
            this.MemoPanel.Controls.Add(this.label6, 0, 0);
            this.MemoPanel.Controls.Add(this.LabelMemo, 1, 0);
            this.MemoPanel.Location = new System.Drawing.Point(719, 24);
            this.MemoPanel.Name = "MemoPanel";
            this.MemoPanel.RowCount = 1;
            this.MemoPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.MemoPanel.Size = new System.Drawing.Size(171, 104);
            this.MemoPanel.TabIndex = 5;
            this.MemoPanel.Visible = false;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label6.Location = new System.Drawing.Point(10, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Memo:";
            // 
            // LabelMemo
            // 
            this.LabelMemo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelMemo.Location = new System.Drawing.Point(55, 0);
            this.LabelMemo.Name = "LabelMemo";
            this.LabelMemo.Size = new System.Drawing.Size(113, 104);
            this.LabelMemo.TabIndex = 1;
            // 
            // ButtonUpdate
            // 
            this.ButtonUpdate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ButtonUpdate.Location = new System.Drawing.Point(397, 138);
            this.ButtonUpdate.Name = "ButtonUpdate";
            this.ButtonUpdate.Size = new System.Drawing.Size(87, 23);
            this.ButtonUpdate.TabIndex = 6;
            this.ButtonUpdate.Text = "Update Folder";
            this.ButtonUpdate.UseVisualStyleBackColor = true;
            this.ButtonUpdate.Visible = false;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label5.Location = new System.Drawing.Point(48, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Path:";
            // 
            // LabelModifiedDate
            // 
            this.LabelModifiedDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LabelModifiedDate.AutoSize = true;
            this.LabelModifiedDate.BackColor = System.Drawing.Color.Transparent;
            this.LabelModifiedDate.Location = new System.Drawing.Point(86, 26);
            this.LabelModifiedDate.Name = "LabelModifiedDate";
            this.LabelModifiedDate.Size = new System.Drawing.Size(35, 13);
            this.LabelModifiedDate.TabIndex = 3;
            this.LabelModifiedDate.Text = "label4";
            // 
            // LabelSize
            // 
            this.LabelSize.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LabelSize.AutoSize = true;
            this.LabelSize.BackColor = System.Drawing.Color.Transparent;
            this.LabelSize.Location = new System.Drawing.Point(86, 48);
            this.LabelSize.Name = "LabelSize";
            this.LabelSize.Size = new System.Drawing.Size(35, 13);
            this.LabelSize.TabIndex = 4;
            this.LabelSize.Text = "label5";
            // 
            // LabelCreatedDate
            // 
            this.LabelCreatedDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LabelCreatedDate.AutoSize = true;
            this.LabelCreatedDate.BackColor = System.Drawing.Color.Transparent;
            this.LabelCreatedDate.Location = new System.Drawing.Point(86, 70);
            this.LabelCreatedDate.Name = "LabelCreatedDate";
            this.LabelCreatedDate.Size = new System.Drawing.Size(35, 13);
            this.LabelCreatedDate.TabIndex = 5;
            this.LabelCreatedDate.Text = "label6";
            // 
            // LabelContains
            // 
            this.LabelContains.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LabelContains.AutoSize = true;
            this.LabelContains.BackColor = System.Drawing.Color.Transparent;
            this.LabelContains.Location = new System.Drawing.Point(86, 4);
            this.LabelContains.Name = "LabelContains";
            this.LabelContains.Size = new System.Drawing.Size(35, 13);
            this.LabelContains.TabIndex = 7;
            this.LabelContains.Text = "label5";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label3.Location = new System.Drawing.Point(8, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Date created:";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label4.Location = new System.Drawing.Point(29, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Contains:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label2.Location = new System.Drawing.Point(50, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Size:";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label1.Location = new System.Drawing.Point(5, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date modified:";
            // 
            // LabelPath
            // 
            this.LabelPath.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LabelPath.AutoSize = true;
            this.LabelPath.BackColor = System.Drawing.Color.Transparent;
            this.LabelPath.Location = new System.Drawing.Point(86, 91);
            this.LabelPath.Name = "LabelPath";
            this.LabelPath.Size = new System.Drawing.Size(35, 13);
            this.LabelPath.TabIndex = 9;
            this.LabelPath.Text = "label6";
            // 
            // LabelType
            // 
            this.LabelType.AutoSize = true;
            this.LabelType.BackColor = System.Drawing.Color.Transparent;
            this.LabelType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabelType.Location = new System.Drawing.Point(218, 50);
            this.LabelType.Name = "LabelType";
            this.LabelType.Size = new System.Drawing.Size(46, 17);
            this.LabelType.TabIndex = 2;
            this.LabelType.Text = "label2";
            // 
            // LabelName
            // 
            this.LabelName.AutoSize = true;
            this.LabelName.BackColor = System.Drawing.Color.Transparent;
            this.LabelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabelName.Location = new System.Drawing.Point(218, 24);
            this.LabelName.Name = "LabelName";
            this.LabelName.Size = new System.Drawing.Size(68, 17);
            this.LabelName.TabIndex = 1;
            this.LabelName.Text = "Goods.txt";
            // 
            // InfoBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImage = global::DiscManageMaster.Controls.Properties.Resources.bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.ButtonUpdate);
            this.Controls.Add(this.MemoPanel);
            this.Controls.Add(this.DetailPanel);
            this.Controls.Add(this.LabelType);
            this.Controls.Add(this.LabelName);
            this.Controls.Add(this.pictureBox1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.Name = "InfoBar";
            this.Size = new System.Drawing.Size(922, 164);
            this.Load += new System.EventHandler(this.InfoBar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.DetailPanel.ResumeLayout(false);
            this.DetailPanel.PerformLayout();
            this.MemoPanel.ResumeLayout(false);
            this.MemoPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private TransparentLabel LabelName;
        private TransparentLabel LabelType;
        private TransparentLabel label3;
        private TransparentLabel label2;
        private TransparentLabel label1;
        private TransparentLabel LabelCreatedDate;
        private TransparentLabel LabelSize;
        private TransparentLabel LabelModifiedDate;
        private TransparentLabel LabelContains;
        private TransparentLabel label4;
        private System.Windows.Forms.TableLayoutPanel DetailPanel;
        private TransparentLabel label5;
        private TransparentLabel LabelPath;
        private System.Windows.Forms.TableLayoutPanel MemoPanel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label LabelMemo;
        private System.Windows.Forms.Button ButtonUpdate;
    }
}
