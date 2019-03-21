//
// Copyright (c) 2019 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/Capreze/blob/master/LICENSE
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Capreze.Runtime.InteropServices
{

    public static class Gdi32
    {

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hwnd);

        [DllImport("gdi32.dll")]
        public static extern int GetDeviceCaps(IntPtr hdc, int msg);

        public enum DeviceCapsIndex
        {

            LOGPIXELSX = 88,

            LOGPIXELSY = 90,

        }

    }

}
