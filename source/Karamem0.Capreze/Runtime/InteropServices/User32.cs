//
// Copyright (c) 2019-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/main/LICENSE
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Capreze.Runtime.InteropServices
{

    public static partial class User32
    {

        [LibraryImport("user32.dll")]
        internal static partial uint GetDpiForWindow(IntPtr hwnd);

        [LibraryImport("user32.dll")]
        internal static partial int GetSystemMetricsForDpi(int index, uint dpi);

        [LibraryImport("user32.dll")]
        internal static partial int GetWindowInfo(IntPtr hwnd, ref WindowInfo wi);

        [LibraryImport("user32.dll")]
        internal static partial IntPtr SetWindowPos(IntPtr hwnd, IntPtr order, int x, int y, int width, int height, uint flags);

        [LibraryImport("user32.dll")]
        internal static partial int ShowWindow(IntPtr hwnd, uint msg);

        internal enum SystemMetricIndex
        {

            SM_CXSIZEFRAME = 32,

            SM_CYSIZEFRAME = 33,

            SM_CXPADDEDBORDER = 92,

        }

        [Flags()]
        internal enum SetWindowPosFlags
        {

            SWP_NOSIZE = 0x0001,

            SWP_NOMOVE = 0x0002,

            SWP_NOZORDER = 0x0004,

            SWP_NOREDRAW = 0x0008,

            SWP_NOACTIVATE = 0x0010,

            SWP_FRAMECHANGED = 0x0020,

            SWP_SHOWWINDOW = 0x0040,

        }

        public enum ShowWindowFlags
        {

            SW_HIDE = 0,

            SW_SHOWNORMAL = 1,

            SW_SHOWMINIMIZED = 2,

            SW_MAXIMIZE = 3,

            SW_SHOWMAXIMIZED = 3,

            SW_SHOWNOACTIVATE = 4,

            SW_SHOW = 5,

            SW_MINIMIZE = 6,

            SW_SHOWMINNOACTIVE = 7,

            SW_SHOWNA = 8,

            SW_RESTORE = 9,

            SW_SHOWDEFAULT = 10,

            SW_FORCEMINIMIZE = 11,

        }

        public enum WindowOrder
        {

            HWND_TOP = 0,

            HWND_BOTTOM = 1,

            HWND_TOPMOST = -1,

            HWND_NOTOPMOST = -2,

        }

        public struct WindowInfo
        {

            public int Size { get; set; }

            public Rectangle Window { get; set; }

            public Rectangle Client { get; set; }

            public int Style { get; set; }

            public int ExStyle { get; set; }

            public int WindowStatus { get; set; }

            public uint WindowBordersX { get; set; }

            public uint WindowBordersY { get; set; }

            public short WindowType { get; set; }

            public short CreatorVersion { get; set; }

        }

        public struct Rectangle
        {

            public int Left { get; set; }

            public int Top { get; set; }

            public int Right { get; set; }

            public int Bottom { get; set; }

            public readonly int Width => this.Right - this.Left;

            public readonly int Height => this.Bottom - this.Top;

        }

    }

}
