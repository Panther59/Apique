// <copyright file="InteropHelper.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>08-09-2017</date>

namespace RestClientLibrary.Common
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    #region Enums

    /// <summary>
    /// Defines the IDC_STANDARD_CURSORS
    /// </summary>
    enum IDC_STANDARD_CURSORS
    {
        /// <summary>
        /// Defines the IDC_ARROW
        /// </summary>
        IDC_ARROW = 32512,

        /// <summary>
        /// Defines the IDC_IBEAM
        /// </summary>
        IDC_IBEAM = 32513,

        /// <summary>
        /// Defines the IDC_WAIT
        /// </summary>
        IDC_WAIT = 32514,

        /// <summary>
        /// Defines the IDC_CROSS
        /// </summary>
        IDC_CROSS = 32515,

        /// <summary>
        /// Defines the IDC_UPARROW
        /// </summary>
        IDC_UPARROW = 32516,

        /// <summary>
        /// Defines the IDC_SIZE
        /// </summary>
        IDC_SIZE = 32640,

        /// <summary>
        /// Defines the IDC_ICON
        /// </summary>
        IDC_ICON = 32641,

        /// <summary>
        /// Defines the IDC_SIZENWSE
        /// </summary>
        IDC_SIZENWSE = 32642,

        /// <summary>
        /// Defines the IDC_SIZENESW
        /// </summary>
        IDC_SIZENESW = 32643,

        /// <summary>
        /// Defines the IDC_SIZEWE
        /// </summary>
        IDC_SIZEWE = 32644,

        /// <summary>
        /// Defines the IDC_SIZENS
        /// </summary>
        IDC_SIZENS = 32645,

        /// <summary>
        /// Defines the IDC_SIZEALL
        /// </summary>
        IDC_SIZEALL = 32646,

        /// <summary>
        /// Defines the IDC_NO
        /// </summary>
        IDC_NO = 32648,

        /// <summary>
        /// Defines the IDC_HAND
        /// </summary>
        IDC_HAND = 32649,

        /// <summary>
        /// Defines the IDC_APPSTARTING
        /// </summary>
        IDC_APPSTARTING = 32650,

        /// <summary>
        /// Defines the IDC_HELP
        /// </summary>
        IDC_HELP = 32651
    }

    #endregion

    public class InteropHelper
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        // http://msdn.microsoft.com/en-us/library/dd144871(VS.85).aspx
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        // http://msdn.microsoft.com/en-us/library/dd183370(VS.85).aspx
        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BitBlt(IntPtr hDestDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, Int32 dwRop);

        // http://msdn.microsoft.com/en-us/library/dd183488(VS.85).aspx
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

        // http://msdn.microsoft.com/en-us/library/dd183489(VS.85).aspx
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        // http://msdn.microsoft.com/en-us/library/dd162957(VS.85).aspx
        [DllImport("gdi32.dll", ExactSpelling = true, PreserveSig = true, SetLastError = true)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        // http://msdn.microsoft.com/en-us/library/dd183539(VS.85).aspx
        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        // http://msdn.microsoft.com/en-us/library/dd162920(VS.85).aspx
        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hwnd, IntPtr dc);

        [DllImport("user32.dll")]
        public static extern IntPtr LoadCursorFromFile(string lpFileName);

        [DllImport("user32.dll")]
        public static extern IntPtr SetCursor(IntPtr hCursor);

        [DllImport("user32.dll")]
        public static extern bool SetSystemCursor(IntPtr hcur, uint id);

        [DllImport("user32.dll")]
        public static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

        public const int SPIF_UPDATEINIFILE = 0x01;

        public const int SPIF_SENDCHANGE = 0x02;

        public const int SPI_SETCURSORS = 0x0057;

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        public static extern bool SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public const uint OCR_NORMAL = 32512;

        public const int SRCCOPY = 0xCC0020;

        public delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        public static BitmapSource CaptureRegion(IntPtr hWnd, int x, int y, int width, int height)
        {
            IntPtr sourceDC = IntPtr.Zero;
            IntPtr targetDC = IntPtr.Zero;
            IntPtr compatibleBitmapHandle = IntPtr.Zero;
            BitmapSource bitmap = null;

            try
            {
                // gets the main desktop and all open windows
                sourceDC = InteropHelper.GetDC(InteropHelper.GetDesktopWindow());

                //sourceDC = User32.GetDC(hWnd);
                targetDC = InteropHelper.CreateCompatibleDC(sourceDC);

                // create a bitmap compatible with our target DC
                compatibleBitmapHandle = InteropHelper.CreateCompatibleBitmap(sourceDC, width, height);

                // gets the bitmap into the target device context
                InteropHelper.SelectObject(targetDC, compatibleBitmapHandle);

                // copy from source to destination
                InteropHelper.BitBlt(targetDC, 0, 0, width, height, sourceDC, x, y, InteropHelper.SRCCOPY);

                // Here's the WPF glue to make it all work. It converts from an
                // hBitmap to a BitmapSource. Love the WPF interop functions
                bitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    compatibleBitmapHandle, IntPtr.Zero, Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());

            }
            catch (Exception ex)
            {

            }
            finally
            {
                DeleteObject(compatibleBitmapHandle);
                ReleaseDC(IntPtr.Zero, sourceDC);
                ReleaseDC(IntPtr.Zero, targetDC);
            }

            return bitmap;
        }

        public static string ConvertToString(Color color)
        {
            return string.Format("#{0}", color.ToString().Substring(3));
        }


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };

        public static Point GetMousePosition()
        {
            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        }
    }
}
