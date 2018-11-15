using System;
using System.Windows.Forms;

namespace DiscManageMaster.Controls
{
    public partial class BgMyToolBar : UserControl
    {
        public BgMyToolBar()
        {
            InitializeComponent();
        }

        private readonly MyToolBar toolbar = new MyToolBar();
        public MyToolBar TB
        {
            get { return toolbar; }
        }

        private void BgMyToolBar_Load(object sender, EventArgs e)
        {
            BackgroundImageLayout = ImageLayout.Stretch;
            BackgroundImage = DiscManageMaster.Controls.Properties.Resources.tbbg;
            Controls.Add(TB);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Height = TB.Height;
            Width = TB.Width;
        }
    }
}
