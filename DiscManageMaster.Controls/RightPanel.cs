using System;
using System.Windows.Forms;

namespace DiscManageMaster.Controls
{
    public partial class RightPanel : UserControl
    {
        public RightPanel()
        {
            InitializeComponent();
        }

        private void RightPanel_Load(object sender, EventArgs e)
        {
            View = RightPanelView.Welcome;
        }

        private RightPanelView thisView;
        public RightPanelView View
        {
            get { return thisView; }
            set
            {
                thisView = value;
                switch (thisView)
                {
                    case RightPanelView.Welcome:
                        {
                            welcomePage1.Dock = DockStyle.Fill;
                            welcomePage1.Visible = true;

                            discListView1.Visible = false;
                            optionsPage1.Visible = false;
                            helpPage1.Visible = false;
                            aboutPage1.Visible = false;
                        }
                        break;
                    case RightPanelView.Content:
                        {
                            discListView1.Dock = DockStyle.Fill;
                            discListView1.Visible = true;

                            welcomePage1.Visible = false;
                            optionsPage1.Visible = false;
                            helpPage1.Visible = false;
                            aboutPage1.Visible = false;
                        }
                        break;
                    case RightPanelView.Options:
                        {
                            optionsPage1.Dock = DockStyle.Fill;
                            optionsPage1.Visible = true;

                            welcomePage1.Visible = false;
                            discListView1.Visible = false;
                            helpPage1.Visible = false;
                            aboutPage1.Visible = false;
                        }
                        break;
                    case RightPanelView.Help:
                        {
                            helpPage1.Dock = DockStyle.Fill;
                            helpPage1.Visible = true;

                            welcomePage1.Visible = false;
                            discListView1.Visible = false;
                            optionsPage1.Visible = false;
                            aboutPage1.Visible = false;
                        }
                        break;
                    case RightPanelView.About:
                        {
                            aboutPage1.Dock = DockStyle.Fill;
                            aboutPage1.Visible = true;

                            welcomePage1.Visible = false;
                            discListView1.Visible = false;
                            optionsPage1.Visible = false;
                            helpPage1.Visible = false;
                        }
                        break;
                }
            }
        }

        public DiscListView ListView
        {
            get { return discListView1; }
        }

        public WelcomePage Welcome
        {
            get { return welcomePage1; }
        }

        public OptionsPage Option
        {
            get { return optionsPage1; }
        }

        public HelpPage Help
        {
            get { return helpPage1; }
        }

        public AboutPage About
        {
            get { return aboutPage1; }
        }
    }

    public enum RightPanelView
    {
        Welcome,
        Content,
        Options,
        Help,
        About
    }
}
