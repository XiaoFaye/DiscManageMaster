using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DiscManageMaster.Controls
{
    public partial class WelcomePage : UserControl
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        private void WelcomePage_Resize(object sender, EventArgs e)
        {
            panel1.Location = new Point((Width - panel1.Width)/2, 10);
        }

        public Button NewFolder
        {
            get { return button1; }
        }

        public Button ViewFiles
        {
            get { return button2; }
        }

        public Button ChangeSettings
        {
            get { return button3; }
        }
    }
}
