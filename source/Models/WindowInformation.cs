//
// Copyright (c) 2021 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/Capreze/blob/master/LICENSE
//

using Karamem0.Capreze.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Capreze.Models
{

    public class WindowInformation : ModelBase
    {

        public WindowInformation()
        {
        }

        public IntPtr Hwnd { get; set; }

        public string FilePath { get; set; }

        public string FileName { get; set; }

        public string Title { get; set; }

    }

}
