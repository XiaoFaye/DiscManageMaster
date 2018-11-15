using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DiscManageMaster.Core;

namespace DiscManageMaster.Controls
{
    public class MyToolBar : ToolBar
    {
        private readonly ImageList IL = new ImageList();

        public MyToolBar()
        {
            IL.ColorDepth = ColorDepth.Depth32Bit;
            IL.ImageSize = new Size(24, 24);
            Divider = false;
            TextAlign = ToolBarTextAlign.Right;
            Appearance = ToolBarAppearance.Flat;
            Dock = DockStyle.None;
            ImageList = IL;

            SetStyle(ControlStyles.DoubleBuffer,true);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            //if (CommVar.IsVista)
            //{
            //    VistaApi.DWM_BLURBEHIND bbhOff = new VistaApi.DWM_BLURBEHIND
            //                                         {
            //                                             dwFlags =
            //                                                 (VistaApi.DWM_BLURBEHIND.DWM_BB_ENABLE |
            //                                                  VistaApi.DWM_BLURBEHIND.DWM_BB_BLURREGION),
            //                                             fEnable = false,
            //                                             hRegionBlur = IntPtr.Zero
            //                                         };
            //    VistaApi.DwmEnableBlurBehindWindow(Handle, bbhOff);
            //    VistaApi.EnableBlurBehindWindow(this, true);
            //}
        }

        public int AddButton(string text, Image image, string imagekey)
        {
            int index = Buttons.Add(text);
            if (IL.Images[imagekey] == null)
                IL.Images.Add(imagekey, image);

            Buttons[index].ImageKey = imagekey;
            return index;
        }

        public int AddButton(string text, Image image, string imagekey, ToolBarButtonStyle style)
        {
            int index = Buttons.Add(text);
            if (IL.Images[imagekey] == null)
                IL.Images.Add(imagekey, image);

            Buttons[index].ImageKey = imagekey;
            Buttons[index].Style = style;

            return index;
        }

        public int AddButton(string name, string text, Image image, string imagekey, ToolBarButtonStyle style, Menu menu)
        {
            int index = Buttons.Add(text);
            if (IL.Images[imagekey] == null)
                IL.Images.Add(imagekey, image);

            Buttons[index].Name = name;
            Buttons[index].ImageKey = imagekey;
            Buttons[index].Style = style;
            Buttons[index].DropDownMenu = menu;
            
            return index;
        }

        public void SetButtonImage(int index, Image image, string imagekey)
        {
            if (IL.Images[imagekey] == null)
                IL.Images.Add(imagekey, image);

            Buttons[index].ImageKey = imagekey;
        }

        public int GetButtonIndex(ToolBarButton button)
        {
            return Buttons.IndexOf(button);
        }
    }
}
