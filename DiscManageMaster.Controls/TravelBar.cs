using System;
using System.Drawing;
using System.Windows.Forms;
using DiscManageMaster.Core;

namespace DiscManageMaster.Controls
{
    public partial class TravelBar : UserControl
    {
        public TravelButton TB
        {
            get { return travelButton1; }
        }

        public string SearchKeyword
        {
            get { return SearchBar.Text; }
            set
            {
                if (value == "")
                    SearchBar.Text = "";
            }
        }

        public string Address
        {
            get { return comboBox1.Text; }
            set { comboBox1.Text = value;}
        }

        public TravelBar()
        {
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer, true);
            comboBox1.Text = "";
        }

        //protected override void OnHandleCreated(EventArgs e)
        //{
        //    base.OnHandleCreated(e);
        //    if (CommVar.IsVista)
        //    {
        //        VistaApi.EnableBlurBehindWindow(this, true);
        //        VistaApi.DWM_BLURBEHIND bbhOff = new VistaApi.DWM_BLURBEHIND
        //                                             {
        //                                                 dwFlags =
        //                                                     (VistaApi.DWM_BLURBEHIND.DWM_BB_ENABLE |
        //                                                      VistaApi.DWM_BLURBEHIND.DWM_BB_BLURREGION),
        //                                                 fEnable = false,
        //                                                 hRegionBlur = IntPtr.Zero
        //                                             };
        //        //VistaApi.DwmEnableBlurBehindWindow(Handle, bbhOff);
        //    }
        //}

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);

            //if (VistaApi.DwmIsCompositionEnabled())
            //{
                //e.Graphics.FillRectangle(Brushes.Black,
                //                         Rectangle.FromLTRB(0, 0, ClientRectangle.Width,
                //                                            m_glassMargins.cyTopHeight));
                //e.Graphics.FillRectangle(Brushes.Black, 0,0,100,100);
            //e.Graphics.FillRectangle(Brushes.Black, comboBox1.ClientRectangle);
            //}
        //}

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            
            travelButton1.Location = new Point(0, 0);
            comboBox1.Location = new Point(travelButton1.Width, (travelButton1.Height - comboBox1.Height)/2);
            comboBox1.Width =(int) ((Width - travelButton1.Width)*0.75);
            comboBox1.Height = travelButton1.Height;

            SearchBar.Location = new Point(travelButton1.Width + comboBox1.Width + 5, (travelButton1.Height - comboBox1.Height) / 2);
            SearchBar.Width = Width - travelButton1.Width - comboBox1.Width - 15;
            SearchBar.Height = travelButton1.Height;
        }

        private void SearchBar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '/' || e.KeyChar == '\\' || e.KeyChar == ':' || e.KeyChar == '*' || e.KeyChar == '?' || e.KeyChar == '"' || e.KeyChar == '<' || e.KeyChar == '>' || e.KeyChar == '|')
            {
                e.Handled = true;
                toolTip1.Show("A search keyword cannot contain any of the following characters: \n          \\ / : * ? \" < > |", SearchBar, SearchBar.Location);
            }
            else 
                toolTip1.Hide(SearchBar);
        }

        public delegate void SearchBarKeyword(object sender, SearchBarEventArgs e);
        public delegate void SearchEnded(object sender, EventArgs e);

        public event SearchBarKeyword OnSearchBarKeyword;
        public event SearchEnded OnSearchEnded;

        private void SearchBar_TextChanged(object sender, EventArgs e)
        {
            if (SearchBar.Focused)
                OnSearchBarKeyword(this, new SearchBarEventArgs(SearchBar.Text.Trim()));
        }

        private void SearchBar_LostFocus(object sender, EventArgs e)
        {
            if (SearchBar.Text != "")
                OnSearchEnded(sender, e);
        }
    }

    public class SearchBarEventArgs : EventArgs
    {
        public string Keyword;
        public SearchBarEventArgs(string keyword)
        {
            Keyword = keyword;
        }
    }
}