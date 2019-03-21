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
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Capreze.Configuration
{

    public class AppSettings : AppSettingsBase
    {

        public static readonly AppSettings Instance = new AppSettings();

        private AppSettings()
        {
        }

        public int CaptureHeight { get; set; }

        public int CaptureWidth { get; set; }

    }

}
