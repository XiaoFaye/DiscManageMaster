namespace DiscManageMaster.Controls
{
    partial class RightPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RightPanel));
            this.optionsPage1 = new DiscManageMaster.Controls.OptionsPage();
            this.welcomePage1 = new DiscManageMaster.Controls.WelcomePage();
            this.discListView1 = new DiscManageMaster.Controls.DiscListView();
            this.helpPage1 = new DiscManageMaster.Controls.HelpPage();
            this.aboutPage1 = new DiscManageMaster.Controls.AboutPage();
            this.SuspendLayout();
            // 
            // optionsPage1
            // 
            this.optionsPage1.Location = new System.Drawing.Point(406, 52);
            this.optionsPage1.Name = "optionsPage1";
            this.optionsPage1.SaveOnExit = false;
            this.optionsPage1.Size = new System.Drawing.Size(211, 113);
            this.optionsPage1.StartupPage = -1;
            this.optionsPage1.TabIndex = 1;
            // 
            // welcomePage1
            // 
            this.welcomePage1.BackColor = System.Drawing.Color.White;
            this.welcomePage1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("welcomePage1.BackgroundImage")));
            this.welcomePage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.welcomePage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.welcomePage1.Location = new System.Drawing.Point(185, 207);
            this.welcomePage1.Name = "welcomePage1";
            this.welcomePage1.Size = new System.Drawing.Size(184, 109);
            this.welcomePage1.TabIndex = 2;
            // 
            // discListView1
            // 
            this.discListView1.Content = null;
            this.discListView1.Location = new System.Drawing.Point(58, 15);
            this.discListView1.Name = "discListView1";
            this.discListView1.Size = new System.Drawing.Size(226, 150);
            this.discListView1.TabIndex = 3;
            this.discListView1.UseCompatibleStateImageBehavior = false;
            this.discListView1.UseVistaIcon = false;
            // 
            // helpPage1
            // 
            this.helpPage1.Location = new System.Drawing.Point(674, 15);
            this.helpPage1.Name = "helpPage1";
            this.helpPage1.Size = new System.Drawing.Size(150, 150);
            this.helpPage1.TabIndex = 4;
            // 
            // aboutPage1
            // 
            this.aboutPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.aboutPage1.Location = new System.Drawing.Point(646, 225);
            this.aboutPage1.Name = "aboutPage1";
            this.aboutPage1.Size = new System.Drawing.Size(150, 150);
            this.aboutPage1.TabIndex = 5;
            // 
            // RightPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.aboutPage1);
            this.Controls.Add(this.helpPage1);
            this.Controls.Add(this.discListView1);
            this.Controls.Add(this.welcomePage1);
            this.Controls.Add(this.optionsPage1);
            this.DoubleBuffered = true;
            this.Name = "RightPanel";
            this.Size = new System.Drawing.Size(921, 462);
            this.Load += new System.EventHandler(this.RightPanel_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private OptionsPage optionsPage1;
        private WelcomePage welcomePage1;
        private DiscListView discListView1;
        private HelpPage helpPage1;
        private AboutPage aboutPage1;
    }
}
