using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DiscManageMaster.Controls.Properties;

namespace DiscManageMaster.Controls
{
    public partial class TravelButton : Control
    {
        #region Fields

        private readonly Image BackButton = Resources.LEFTBUTTON;
        private readonly Image BackGround = Resources.BACKGROUND;
        private readonly Image ForwardButton = Resources.RIGHTBUTTON;

        private readonly Rectangle BACK_BUTTON_RECT = new Rectangle(2, 2, 25, 25);
        private readonly Rectangle BACKGROUND_RECT = new Rectangle(0, 0, 74, 29);
        private readonly Rectangle DROPDOWN_ARROW_RECT = new Rectangle(57, 4, 17, 20);
        private readonly Rectangle FORWARD_BUTTON_RECT = new Rectangle(30, 2, 25, 25);

        private ControlState BackButtonState = ControlState.Normal;
        private ControlState DropDownArrowState = ControlState.Normal;
        private ControlState ForwardButtonState = ControlState.Normal;

        private string BackToolTip = null;
        private string ForwardToolTip = null;

        private readonly ContextMenu DropDownMenu = new ContextMenu();
        private int HistoryIndex = -1;
        public int MaxStep = 8;
        private bool ShowMenu = false;

        [Browsable(false)]
        public event PaintEventHandler PaintBackground;

        public delegate void HistoryChanged(object sender, HistoryEventArgs e);

        public event HistoryChanged OnHistoryChanged;

        private enum ControlState
        {
            Normal,
            Hover,
            Pressed,
            Disabled
        }

        #endregion

        #region Constructors

        public TravelButton()
        {
            SetStyle(ControlStyles.DoubleBuffer, true);
            DropDownMenu.Popup += OpenMenu;
            DropDownMenu.Collapse += CloseMenu;
            MouseCaptureChanged += CloseMenu;

            BackButtonState = DropDownMenu.MenuItems.Count == 0 ? ControlState.Disabled : ControlState.Normal;
            ForwardButtonState = DropDownMenu.MenuItems.Count == 0 ? ControlState.Disabled : ControlState.Normal;
            DropDownArrowState = DropDownMenu.MenuItems.Count == 0 ? ControlState.Disabled : ControlState.Normal;

            InitializeComponent();
        }

        #endregion

        #region Override

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            if (PaintBackground != null)
                PaintBackground(this, pevent);
            else
                base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            DrawItem(pe.Graphics, TravelButtonItem.BackGround, DropDownArrowState);
            DrawItem(pe.Graphics, TravelButtonItem.BackButton, BackButtonState);
            DrawItem(pe.Graphics, TravelButtonItem.ForwardButton, ForwardButtonState);

            base.OnPaint(pe);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (BackButtonState != ControlState.Disabled)
                BackButtonState = ControlState.Normal;

            if (ForwardButtonState != ControlState.Disabled)
                ForwardButtonState = ControlState.Normal;

            if (DropDownArrowState != ControlState.Disabled)
                if (ShowMenu)
                    DropDownArrowState = ControlState.Pressed;
                else
                    DropDownArrowState = ControlState.Normal;

            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs me)
        {
            base.OnMouseDown(me);

            if (Enabled)
            {
                if (me.Button == MouseButtons.Left && me.Clicks == 1)
                {
                    if (BACK_BUTTON_RECT.Contains(me.X, me.Y) && BackButtonState != ControlState.Disabled)
                        BackButtonState = ControlState.Pressed;
                    else if (FORWARD_BUTTON_RECT.Contains(me.X, me.Y) && ForwardButtonState != ControlState.Disabled)
                        ForwardButtonState = ControlState.Pressed;
                    else if (DROPDOWN_ARROW_RECT.Contains(me.X, me.Y) && DropDownArrowState != ControlState.Disabled)
                    {
                        DropDownMenu.Show(this, new Point(BACKGROUND_RECT.X, BACKGROUND_RECT.Height));
                        DropDownArrowState = ControlState.Pressed;
                    }

                    Invalidate();
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs me)
        {
            base.OnMouseMove(me);

            if (Enabled && !Capture)
            {
                if (BACK_BUTTON_RECT.Contains(me.X, me.Y))
                {
                    if (BackButtonState != ControlState.Disabled)
                    {
                        BackButtonState = ControlState.Hover;
                        SetButtonToolTip(TravelButtonItem.BackButton, DropDownMenu.MenuItems[HistoryIndex - 1].Text);
                    }
                    else
                        SetToolTip(TravelButtonItem.BackGround);

                    if (ForwardButtonState != ControlState.Disabled)
                        ForwardButtonState = ControlState.Normal;
                    if (DropDownArrowState != ControlState.Disabled)
                        DropDownArrowState = ControlState.Normal;
                }

                if (FORWARD_BUTTON_RECT.Contains(me.X, me.Y))
                {
                    if (ForwardButtonState != ControlState.Disabled)
                    {
                        ForwardButtonState = ControlState.Hover;
                        SetButtonToolTip(TravelButtonItem.ForwardButton, DropDownMenu.MenuItems[HistoryIndex + 1].Text);
                    }
                    else
                        SetToolTip(TravelButtonItem.BackGround);

                    if (BackButtonState != ControlState.Disabled)
                        BackButtonState = ControlState.Normal;
                    if (DropDownArrowState != ControlState.Disabled)
                        DropDownArrowState = ControlState.Normal;
                }

                if (DROPDOWN_ARROW_RECT.Contains(me.X, me.Y))
                {
                    if (DropDownArrowState != ControlState.Disabled)
                        DropDownArrowState = ControlState.Hover;
                    if (ForwardButtonState != ControlState.Disabled)
                        ForwardButtonState = ControlState.Normal;
                    if (BackButtonState != ControlState.Disabled)
                        BackButtonState = ControlState.Normal;

                    SetToolTip(TravelButtonItem.BackGround);
                }

                Invalidate();
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs me)
        {
            if (DROPDOWN_ARROW_RECT.Contains(me.X, me.Y))
                return;
            else
                base.OnMouseDoubleClick(me);
        }

        protected override void OnClick(EventArgs e)
        {
            if (Enabled)
            {
                Point p = PointToClient(MousePosition);

                if (BACK_BUTTON_RECT.Contains(p.X, p.Y) && BackButtonState != ControlState.Disabled)
                    MoveBack();
                else if (FORWARD_BUTTON_RECT.Contains(p.X, p.Y) && ForwardButtonState != ControlState.Disabled)
                    MoveForward();
                else if (DROPDOWN_ARROW_RECT.Contains(p.X, p.Y) && DropDownArrowState != ControlState.Disabled)
                    DropDownArrowState = ControlState.Pressed;

                Invalidate();
            }

            base.OnClick(e);
        }

        #endregion

        #region Pulbic

        private void OpenMenu(object sender, EventArgs e)
        {
            ShowMenu = true;
        }

        private void CloseMenu(object sender, EventArgs e)
        {
            if (!Capture)
            {
                ShowMenu = false;
                if (DropDownArrowState != ControlState.Disabled)
                    DropDownArrowState = ControlState.Normal;
                Refresh();
            }
        }

        public void SetButtonToolTip(TravelButtonItem item, string tip)
        {
            if (item == TravelButtonItem.BackButton)
                BackToolTip = "Back to " + tip;
            else if (item == TravelButtonItem.ForwardButton)
                ForwardToolTip = "Forward to " + tip;

            SetToolTip(item);
        }

        public void AddStep(string text, object data)
        {
            int oldindex = HistoryIndex;
            bool IsExist = false;
            for(int i = 0; i<DropDownMenu.MenuItems.Count; i++)
            {
                if(DropDownMenu.MenuItems[i].Text == text && DropDownMenu.MenuItems[i].Tag == data)
                {
                    HistoryIndex = i;
                    IsExist = true;
                    break;
                }
            }

            if (oldindex == HistoryIndex && !IsExist)
            {
                MenuItem mi = new MenuItem(text);
                mi.Tag = data;
                mi.Click += MenuClicked;
                DropDownMenu.MenuItems.Add(mi);
                if (DropDownMenu.MenuItems.Count > MaxStep)
                    DropDownMenu.MenuItems.RemoveAt(0);

                HistoryIndex = DropDownMenu.MenuItems.Count - 1;
            }

            if (HistoryIndex >= 0)
                DropDownArrowState = ControlState.Normal;

            MoveTo(HistoryIndex);
        }

        public void RemoveStep(int index)
        {
            if (index >= 0 && index < DropDownMenu.MenuItems.Count)
            {
                DropDownMenu.MenuItems.RemoveAt(index);
                if(index == HistoryIndex)
                {
                    if (index == DropDownMenu.MenuItems.Count)
                        MoveTo(DropDownMenu.MenuItems.Count - 1);
                    else
                        MoveTo(index);
                }
            }
        }

        public void RemoveAllSteps()
        {
            DropDownMenu.MenuItems.Clear();
        }

        public int FindStep(object data)
        {
            for (int i = 0; i < DropDownMenu.MenuItems.Count; i++)
                if (DropDownMenu.MenuItems[i].Tag == data)
                    return i;

            return -1;
        }

        public void MoveBack()
        {
            MoveTo(--HistoryIndex);
        }

        public void MoveForward()
        {
            MoveTo(++HistoryIndex);
        }

        public void MoveTo(int index)
        {
            if (index >= 0 && index <= DropDownMenu.MenuItems.Count - 1)
            {
                HistoryIndex = index;
                SetCheckedStep(HistoryIndex);

                ForwardButtonState = HistoryIndex == DropDownMenu.MenuItems.Count - 1
                                         ? ControlState.Disabled
                                         : ControlState.Normal;
                BackButtonState = HistoryIndex == 0 ? ControlState.Disabled : ControlState.Normal;

                Refresh();
                OnHistoryChanged(this, new HistoryEventArgs(DropDownMenu.MenuItems[index].Tag));
            }
        }

        public void MenuClicked(object sender, EventArgs e)
        {
            MoveTo(((MenuItem) sender).Index);
        }

        private void SetCheckedStep(int index)
        {
            if (index <= DropDownMenu.MenuItems.Count - 1)
                for (int i = 0; i < DropDownMenu.MenuItems.Count; i++)
                    if (i == index)
                        DropDownMenu.MenuItems[i].Checked = true;
                    else
                        DropDownMenu.MenuItems[i].Checked = false;
        }

        #endregion

        #region Private

        private void SetToolTip(TravelButtonItem item)
        {
            string curToolTip = toolTip.GetToolTip(this);

            if (item == TravelButtonItem.BackButton)
            {
                if (curToolTip != BackToolTip)
                {
                    toolTip.SetToolTip(this, BackToolTip);
                }
            }
            else if (item == TravelButtonItem.ForwardButton)
            {
                if (curToolTip != ForwardToolTip)
                {
                    toolTip.SetToolTip(this, ForwardToolTip);
                }
            }
            else
            {
                toolTip.Hide(this);
            }
        }

        private void DrawItem(Graphics g, TravelButtonItem item, ControlState state)
        {
            Rectangle srcRect;
            switch (item)
            {
                case TravelButtonItem.BackButton:
                    srcRect = new Rectangle(new Point(0, 0), BACK_BUTTON_RECT.Size);
                    break;
                case TravelButtonItem.ForwardButton:
                    srcRect = new Rectangle(new Point(0, 0), FORWARD_BUTTON_RECT.Size);
                    break;
                default:
                    srcRect = BACKGROUND_RECT;
                    break;
            }

            int xOffset = 0;
            switch (state)
            {
                case ControlState.Normal:
                    xOffset = 0;
                    break;
                case ControlState.Pressed:
                    xOffset = srcRect.Width;
                    break;
                case ControlState.Hover:
                    xOffset = srcRect.Width*2;
                    break;
                case ControlState.Disabled:
                    xOffset = srcRect.Width*3;
                    break;
            }

            srcRect.X = xOffset;

            if (item == TravelButtonItem.BackButton)
            {
                g.DrawImage(BackButton, BACK_BUTTON_RECT, srcRect, GraphicsUnit.Pixel);
            }
            else if (item == TravelButtonItem.ForwardButton)
            {
                g.DrawImage(ForwardButton, FORWARD_BUTTON_RECT, srcRect, GraphicsUnit.Pixel);
            }
            else if (item == TravelButtonItem.BackGround)
            {
                g.DrawImage(BackGround, BACKGROUND_RECT, srcRect, GraphicsUnit.Pixel);
            }
        }

        #endregion
    }

    public class HistoryEventArgs : EventArgs
    {
        public object Data;

        public HistoryEventArgs(object data)
        {
            Data = data;
        }
    }

    public enum TravelButtonItem
    {
        BackGround,
        BackButton,
        ForwardButton,
        DropDownArrow
    }
}