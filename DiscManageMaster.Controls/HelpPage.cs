using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DiscManageMaster.Controls
{
    public partial class HelpPage : UserControl
    {
        public HelpPage()
        {
            InitializeComponent();
        }

        public string HelpFilePath
        {
            get { return webBrowser1.Url.ToString(); }
            set { webBrowser1.Url = value == null ? new Uri("about:blank") : new Uri(value); }
        }
    }
}
