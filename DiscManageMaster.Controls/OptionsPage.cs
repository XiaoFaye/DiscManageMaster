using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DiscManageMaster.Core;

namespace DiscManageMaster.Controls
{
    public partial class OptionsPage : UserControl
    {
        public OptionsPage()
        {
            InitializeComponent();

            comboBox1.SelectedIndexChanged += SettingChanged;
            comboBox2.SelectedIndexChanged += SettingChanged;
            comboBox2.SelectedIndexChanged += CheckView;
            checkBox1.CheckedChanged += SettingChanged;
            checkBox2.CheckedChanged += SettingChanged;
        }

        private void OptionsPage_Resize(object sender, EventArgs e)
        {
            groupBox1.Location = new Point((Width - groupBox1.Width)/2, 20);
        }

        public Button ButtonSave
        {
            get { return button1; }
        }

        public Button ButtonCancel
        {
            get { return button2; }
        }

        public int StartupPage
        {
            get { return comboBox1.SelectedIndex; }
            set
            {
                 if(value >=0 && value <=2)
                     comboBox1.SelectedIndex = value;
            }
        }

        public int ViewSetting
        {
            get { return comboBox2.SelectedIndex; }
            set { comboBox2.SelectedIndex = value; }
        }

        public bool SaveOnExit
        {
            get { return checkBox1.Checked; }
            set
            {
                checkBox1.Checked = value;
            }
        }

        //public bool ShowHidden
        //{
        //    get { return checkBox2.Checked; }
        //    set
        //    {
        //        checkBox2.Checked = value;
        //    }
        //}

        private void SettingChanged(object sender, EventArgs e)
        {
            if (Visible)
                button1.Enabled = true;
        }

        private void CheckView(object sender, EventArgs e)
        {
            if (!CommVar.IsVista && comboBox2.SelectedIndex == 2)
            {
                MessageBox.Show("View \"LargeIcon(Vista)\" is only for Windows Vista.", "Options", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                comboBox2.SelectedIndex = 0;
            }
        }
    }
}
