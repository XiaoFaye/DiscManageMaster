using System;
using System.Drawing;
using System.Runtime.InteropServices;

public class GlassText
{
    [DllImport("dwmapi.dll", PreserveSig = false)]
    public static extern void DwmEnableBlurBehindWindow(IntPtr hWnd, DWM_BLURBEHIND pBlurBehind);

    [DllImport("dwmapi.dll", PreserveSig = false)]
    public static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, MARGINS pMargins);

    [DllImport("dwmapi.dll", PreserveSig = false)]
    public static extern bool DwmIsCompositionEnabled();

    [DllImport("dwmapi.dll", PreserveSig = false)]
    public static extern void DwmGetColorizationColor(
        out int pcrColorization,
        [MarshalAs(UnmanagedType.Bool)] out bool pfOpaqueBlend);

    [DllImport("dwmapi.dll", PreserveSig = false)]
    public static extern void DwmEnableComposition(bool bEnable);

    [DllImport("dwmapi.dll", PreserveSig = false)]
    public static extern IntPtr DwmRegisterThumbnail(IntPtr dest, IntPtr source);

    [DllImport("dwmapi.dll", PreserveSig = false)]
    public static extern void DwmUnregisterThumbnail(IntPtr hThumbnail);

    [DllImport("dwmapi.dll", PreserveSig = false)]
    public static extern void DwmUpdateThumbnailProperties(IntPtr hThumbnail, DWM_THUMBNAIL_PROPERTIES props);

    [DllImport("dwmapi.dll", PreserveSig = false)]
    public static extern void DwmQueryThumbnailSourceSize(IntPtr hThumbnail, out Size size);

    #region Nested type: DWM_BLURBEHIND

    [StructLayout(LayoutKind.Sequential)]
    public class DWM_BLURBEHIND
    {
        public const uint DWM_BB_BLURREGION = 0x00000002;
        public const uint DWM_BB_ENABLE = 0x00000001;
        public const uint DWM_BB_TRANSITIONONMAXIMIZED = 0x00000004;
        public uint dwFlags;
        [MarshalAs(UnmanagedType.Bool)] public bool fEnable;

        [MarshalAs(UnmanagedType.Bool)] public bool fTransitionOnMaximized;

        public IntPtr hRegionBlur;
    }

    #endregion

    #region Nested type: DWM_THUMBNAIL_PROPERTIES

    [StructLayout(LayoutKind.Sequential)]
    public class DWM_THUMBNAIL_PROPERTIES
    {
        public const uint DWM_TNP_OPACITY = 0x00000004;
        public const uint DWM_TNP_RECTDESTINATION = 0x00000001;
        public const uint DWM_TNP_RECTSOURCE = 0x00000002;
        public const uint DWM_TNP_SOURCECLIENTAREAONLY = 0x00000010;
        public const uint DWM_TNP_VISIBLE = 0x00000008;
        public uint dwFlags;

        [MarshalAs(UnmanagedType.Bool)] public bool fSourceClientAreaOnly;

        [MarshalAs(UnmanagedType.Bool)] public bool fVisible;

        public byte opacity;
        public RECT rcDestination;
        public RECT rcSource;
    }

    #endregion

    #region Nested type: MARGINS

    [StructLayout(LayoutKind.Sequential)]
    public class MARGINS
    {
        public int cxLeftWidth, cxRightWidth;
        public int cyBottomHeight;
        public int cyTopHeight;

        public MARGINS(int left, int top, int right, int bottom)
        {
            cxLeftWidth = left;
            cyTopHeight = top;
            cxRightWidth = right;
            cyBottomHeight = bottom;
        }
    }

    #endregion

    #region Nested type: RECT

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int bottom;
        public int left;
        public int right;
        public int top;

        public RECT(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }
    }

    #endregion
}