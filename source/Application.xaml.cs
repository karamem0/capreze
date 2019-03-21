//
// Copyright (c) 2019 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/Capreze/blob/master/LICENSE
//

using Karamem0.Capreze.Configuration;
using Karamem0.Capreze.Interactivity;
using Karamem0.Capreze.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Karamem0.Capreze
{

    public partial class Application : System.Windows.Application
    {

        public Application()
        {
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var viewModelLocator = this.TryFindResource(nameof(ViewModelLocator)) as ViewModelLocator;
            if (viewModelLocator != null)
            {
                var mainViewModel = viewModelLocator.MainViewModel as MainViewModel;
                if (mainViewModel != null)
                {
                    AppSettings.Instance.Load();
                    mainViewModel.CaptureHeight = AppSettings.Instance.CaptureHeight;
                    mainViewModel.CaptureWidth = AppSettings.Instance.CaptureWidth;
                }
            }
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            var viewModelLocator = this.TryFindResource(nameof(ViewModelLocator)) as ViewModelLocator;
            if (viewModelLocator != null)
            {
                var mainViewModel = viewModelLocator.MainViewModel as MainViewModel;
                if (mainViewModel != null)
                {
                    AppSettings.Instance.CaptureHeight = mainViewModel.CaptureHeight;
                    AppSettings.Instance.CaptureWidth = mainViewModel.CaptureWidth;
                    AppSettings.Instance.Save();
                }
            }
            base.OnExit(e);
        }

    }

}
