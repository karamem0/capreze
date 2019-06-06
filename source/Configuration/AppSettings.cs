//
// Copyright (c) 2019 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/Capreze/blob/master/LICENSE
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [DefaultValue(600)]
        public int CaptureHeight { get; set; }

        [DefaultValue(800)]
        public int CaptureWidth { get; set; }

        [DefaultValue(true)]
        public bool IsOffsetEnabled { get; set; }

        [DefaultValue(false)]
        public bool IsTopmost { get; set; }

    }

}
