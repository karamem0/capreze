//
// Copyright (c) 2019 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/Capreze/blob/master/LICENSE
//

using Karamem0.Capreze.Configuration;
using Karamem0.Capreze.Diagnostics;
using Karamem0.Capreze.Interactivity;
using Karamem0.Capreze.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Karamem0.Capreze
{

    public partial class Application : System.Windows.Application
    {

        public Application()
        {
            Application.Current.DispatcherUnhandledException += this.OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += this.OnUnhandledException;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Telemetry.Instance.TrackEvent("Applicaton Startup");
            var viewModelLocator = this.TryFindResource(nameof(ViewModelLocator)) as ViewModelLocator;
            if (viewModelLocator != null)
            {
                var mainViewModel = viewModelLocator.MainViewModel as MainViewModel;
                if (mainViewModel != null)
                {
                    AppSettings.Instance.Load();
                    mainViewModel.CaptureHeight = AppSettings.Instance.CaptureHeight;
                    mainViewModel.CaptureWidth = AppSettings.Instance.CaptureWidth;
                    mainViewModel.IsOffsetEnabled = AppSettings.Instance.IsOffsetEnabled;
                    mainViewModel.IsTopmost = AppSettings.Instance.IsTopmost;
                }
            }
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Telemetry.Instance.TrackEvent("Applicaton Exit");
            var viewModelLocator = this.TryFindResource(nameof(ViewModelLocator)) as ViewModelLocator;
            if (viewModelLocator != null)
            {
                var mainViewModel = viewModelLocator.MainViewModel as MainViewModel;
                if (mainViewModel != null)
                {
                    AppSettings.Instance.CaptureHeight = mainViewModel.CaptureHeight;
                    AppSettings.Instance.CaptureWidth = mainViewModel.CaptureWidth;
                    AppSettings.Instance.IsOffsetEnabled = mainViewModel.IsOffsetEnabled;
                    AppSettings.Instance.IsTopmost = mainViewModel.IsTopmost;
                    AppSettings.Instance.Save();
                }
            }
            base.OnExit(e);
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Telemetry.Instance.TrackException(e.Exception);
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Telemetry.Instance.TrackException(e.ExceptionObject as Exception);
        }

    }

}
