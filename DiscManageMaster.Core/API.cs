using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DiscManageMaster.Core
{
    public static class API
    {
        #region Common...

        public const int WM_USER = 0x400;
        public const int WM_PAINT = 0x0F;
        public const int WM_LBUTTONDOWN = 0x201;
        public const int WM_MOUSELEAVE = 0x2A3;
        public const int WM_LBUTTONUP = 0x202;
        public const int WM_KILLFOCUS = 0x08;
        public const int WM_NOTIFY = 0x004E;

        public const int BCM_FIRST = 0x1600;
        public const int BCM_SETDROPDOWNSTATE = 0x1606;

        #endregion

        #region ListView...

        //Listview universal constants
        public const int LVM_FIRST = 0x1000;
        //Listview messages
        public const int LVM_SETEXTENDEDLISTVIEWSTYLE = LVM_FIRST + 54;
        public const int LVM_GETEXTENDEDLISTVIEWSTYLE = LVM_FIRST + 55;
        public const int LVM_SETVIEW = LVM_FIRST + 142;
        public const int LVM_SETIMAGELIST = (LVM_FIRST + 3);
        public const int LVM_SETITEMSTATE = (LVM_FIRST + 43);

        public const uint LVIF_TEXT = 0x0001;
        public const uint LVIF_IMAGE = 0x0002;
        public const uint LVIF_PARAM = 0x0004;
        public const uint LVIF_STATE = 0x0008;

        public const int LVSIL_NORMAL = 0;
        public const int LVSIL_SMALL = 1;
        public const int LVSIL_STATE = 2;

        public const uint LVCF_FMT = 0x0001;
        public const uint LVCF_WIDTH = 0x0002;
        public const uint LVCF_TEXT = 0x0004;
        public const uint LVCF_SUBITEM = 0x0008;
        public const uint LVCF_IMAGE = 0x0010;
        public const uint LVCF_ORDER = 0x0020;

        public const int LVCFMT_LEFT = 0x0000;
        public const int LVCFMT_RIGHT = 0x0001;
        public const int LVCFMT_CENTER = 0x0002;
        public const int LVCFMT_IMAGE = 0x0800;
        public const int LVCFMT_BITMAP_ON_RIGHT = 0x1000;
        public const int LVCFMT_COL_HAS_IMAGES = 0x8000;

        public const int LVIS_FOCUSED = 0x0001;
        public const int LVIS_SELECTED = 0x0002;
        public const int LVIS_CUT = 0x0004;
        public const int LVIS_DROPHILITED = 0x0008;
        public const int LVIS_GLOW = 0x0010;
        public const int LVIS_ACTIVATING = 0x0020;
        public const int LVIS_OVERLAYMASK = 0x0F00;
        public const int LVIS_STATEIMAGEMASK = 0xF000;

        public const int LVS_ICON = 0x0000;
        public const int LVS_REPORT = 0x0001;
        public const int LVS_SMALLICON = 0x0002;
        public const int LVS_LIST = 0x0003;
        public const int LVS_TYPEMASK = 0x0003;
        public const int LVS_SINGLESEL = 0x0004;
        public const int LVS_SHOWSELALWAYS = 0x0008;
        public const int LVS_SORTASCENDING = 0x0010;
        public const int LVS_SORTDESCENDING = 0x0020;
        public const int LVS_SHAREIMAGELISTS = 0x0040;
        public const int LVS_NOLABELWRAP = 0x0080;
        public const int LVS_AUTOARRANGE = 0x0100;
        public const int LVS_EDITLABELS = 0x0200;
        public const int LVS_OWNERDATA = 0x1000;
        public const int LVS_NOSCROLL = 0x2000;
        public const int LVS_TYPESTYLEMASK = 0xfc00;
        public const int LVS_ALIGNTOP = 0x0000;
        public const int LVS_ALIGNLEFT = 0x0800;
        public const int LVS_ALIGNMASK = 0x0c00;
        public const int LVS_OWNERDRAWFIXED = 0x0400;
        public const int LVS_NOCOLUMNHEADER = 0x4000;
        public const int LVS_NOSORTHEADER = 0x8000;

        //Listview extended styles
        public const int LVS_EX_AUTOAUTOARRANGE = 0x01000000;
        public const int LVS_EX_AUTOSIZECOLUMNS = 0x10000000;
        public const int LVS_EX_COLUMNSNAPPOINTS = 0x40000000;
        public const uint LVS_EX_COLUMNOVERFLOW = 0x80000000;
        public const int LVS_EX_JUSTIFYCOLUMNS = 0x00200000;
        public const int LVS_EX_HEADERINALLVIEWS = 0x02000000;
        public const int LVS_EX_GRIDLINES = 0x00000001;
        public const int LVS_EX_SUBITEMIMAGES = 0x00000002;
        public const int LVS_EX_CHECKBOXES = 0x00000004;
        public const int LVS_EX_TRACKSELECT = 0x00000008;
        public const int LVS_EX_HEADERDRAGDROP = 0x00000010;
        public const int LVS_EX_FULLROWSELECT = 0x00000020;
        public const int LVS_EX_ONECLICKACTIVATE = 0x00000040;
        public const int LVS_EX_TWOCLICKACTIVATE = 0x00000080;
        public const int LVS_EX_FLATSB = 0x00000100;
        public const int LVS_EX_REGIONAL = 0x00000200;
        public const int LVS_EX_INFOTIP = 0x00000400;
        public const int LVS_EX_UNDERLINEHOT = 0x00000800;
        public const int LVS_EX_UNDERLINECOLD = 0x00001000;
        public const int LVS_EX_MULTIWORKAREAS = 0x00002000;
        public const int LVS_EX_LABELTIP = 0x00004000;
        public const int LVS_EX_BORDERSELECT = 0x00008000;
        public const int LVS_EX_DOUBLEBUFFER = 0x00010000;
        public const int LVS_EX_HIDELABELS = 0x00020000;
        public const int LVS_EX_SINGLEROW = 0x00040000;
        public const int LVS_EX_SNAPTOGRID = 0x00080000;
        public const int LVS_EX_SIMPLESELECT = 0x00100000;

        public const int LV_VIEW_ICON = 0x0;
        public const int LV_VIEW_DETAILS = 0x1;
        public const int LV_VIEW_SMALLICON = 0x2;
        public const int LV_VIEW_LIST = 0x3;
        public const int LV_VIEW_TILE = 0x4;
        public const int LV_VIEW_MAX = 0x4;

        #endregion

        #region TreeView...

        //Treeview universal constants
        public const int TV_FIRST = 0x1100;
        public const int TVM_GETEXTENDEDSTYLE = TV_FIRST + 45;
        public const int TVM_SETAUTOSCROLLINFO = TV_FIRST + 59;
        public const int TVM_SETEXTENDEDSTYLE = TV_FIRST + 44;

        public const int TVM_SETIMAGELIST = (TV_FIRST + 9);

        public const int TVSIL_NORMAL = 0;
        public const int TVSIL_STATE = 2;

        public const int TVS_EX_AUTOHSCROLL = 0x0020;
        public const int TVS_EX_DOUBLEBUFFER = 0x0004;
        public const int TVS_EX_FADEINOUTEXPANDOS = 0x0040;
        public const int TVS_NOHSCROLL = 0x8000;

        #endregion

        [StructLayout(LayoutKind.Sequential)]
        public struct LVITEM
        {
            public int mask;
            public int iItem;
            public int iSubItem;
            public int state;
            public int stateMask;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszText;
            public string cchTextMax;
            public int iImage;
            public int lParam;
            public int iIndent;
            public int iGroupId;
            public int cColumns;
            public int puColumns;
        }

        #region Functions

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(
            IntPtr hWnd,
            int wMsg,
            IntPtr wParam,
            IntPtr lParam);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int SendMessage(
            IntPtr hWnd,
            int wMsg,
            int wParam,
            ref LVITEM lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, uint lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern uint SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        #endregion
    }
}
