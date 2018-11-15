using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DiscManageMaster.Core;

namespace DiscManageMaster.Controls
{
    public partial class AboutPage : UserControl
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private void AboutPage_Resize(object sender, EventArgs e)
        {
            panel1.Location = new Point((Width - panel1.Width) / 2, 20);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", linkLabel1.Text);
        }

        public string RegisterName
        {
            set 
            {
                IsRegisted = true;
                label6.Text = value; 
            }
        }

        private bool isregisted = false;
        public bool IsRegisted
        {
            get { return isregisted; }
            set 
            {
                if (value)
                {
                    panel2.Visible = true;
                    panel3.Visible = false;
                }
                else
                {
                    panel2.Visible = false;
                    panel3.Visible = true;
                }
                isregisted = value; 
            }
        }

        private void AboutPage_Load(object sender, EventArgs e)
        {
            if (IsRegisted)
            {
                panel2.Visible = true;
                panel3.Visible = false;
            }
            else
            {
                panel2.Visible = false;
                panel3.Visible = true;
            }
        }

        public delegate void RegNow(object sender, EventArgs e);
        public event RegNow OnRegNow;
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OnRegNow(this, new EventArgs());
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:" + linkLabel3.Text);
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:" + linkLabel4.Text);
        }
    }
}
