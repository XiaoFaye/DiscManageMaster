namespace DiscManageMaster.Controls
{
    partial class TravelBar
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
            this.components = new System.ComponentModel.Container();
            this.comboBox1 = new VistaControls.TextBox();
            this.SearchBar = new VistaControls.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.travelButton1 = new DiscManageMaster.Controls.TravelButton();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.Color.White;
            this.comboBox1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.comboBox1.Location = new System.Drawing.Point(79, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.ReadOnly = true;
            this.comboBox1.Size = new System.Drawing.Size(383, 20);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.WordWrap = false;
            // 
            // SearchBar
            // 
            this.SearchBar.CueBannerText = "Search";
            this.SearchBar.Location = new System.Drawing.Point(468, 6);
            this.SearchBar.Name = "SearchBar";
            this.SearchBar.Size = new System.Drawing.Size(152, 20);
            this.SearchBar.TabIndex = 2;
            this.SearchBar.TextChanged += new System.EventHandler(this.SearchBar_TextChanged);
            this.SearchBar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SearchBar_KeyPress);
            this.SearchBar.LostFocus += new System.EventHandler(this.SearchBar_LostFocus);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.toolTip1.ToolTipTitle = "Info";
            // 
            // travelButton1
            // 
            this.travelButton1.Location = new System.Drawing.Point(3, 0);
            this.travelButton1.Name = "travelButton1";
            this.travelButton1.Size = new System.Drawing.Size(74, 29);
            this.travelButton1.TabIndex = 0;
            this.travelButton1.TabStop = false;
            this.travelButton1.Text = "travelButton1";
            // 
            // TravelBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SearchBar);
            this.Controls.Add(this.travelButton1);
            this.Controls.Add(this.comboBox1);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Name = "TravelBar";
            this.Size = new System.Drawing.Size(646, 39);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TravelButton travelButton1;
        private VistaControls.TextBox comboBox1;
        private VistaControls.TextBox SearchBar;
        private System.Windows.Forms.ToolTip toolTip1;

    }
}
