//
// Copyright (c) 2021 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/master/LICENSE
//

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Karamem0.Capreze.Configuration;
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
            AppCenter.Start("94420eb3-44fa-45ca-b832-0fbafb832112", typeof(Analytics), typeof(Crashes));
            var viewModelLocator = this.TryFindResource(nameof(ViewModelLocator)) as ViewModelLocator;
            if (viewModelLocator != null)
            {
                var mainViewModel = viewModelLocator.MainViewModel as MainViewModel;
                if (mainViewModel != null)
                {
                    var appSettings = new AppSettings();
                    appSettings.Load();
                    mainViewModel.CaptureHeight = appSettings.CaptureHeight;
                    mainViewModel.CaptureWidth = appSettings.CaptureWidth;
                    mainViewModel.IsOffsetEnabled = appSettings.IsOffsetEnabled;
                    mainViewModel.IsTopmost = appSettings.IsTopmost;
                }
            }
            Analytics.TrackEvent("application start");
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
                    var appSettings = new AppSettings()
                    {
                        CaptureHeight = mainViewModel.CaptureHeight,
                        CaptureWidth = mainViewModel.CaptureWidth,
                        IsOffsetEnabled = mainViewModel.IsOffsetEnabled,
                        IsTopmost = mainViewModel.IsTopmost
                    };
                    appSettings.Save();
                }
            }
            Analytics.TrackEvent("application exit");
            base.OnExit(e);
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Crashes.TrackError(e.Exception);
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Crashes.TrackError(e.ExceptionObject as Exception);
        }

    }

}
