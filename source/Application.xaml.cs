//
// Copyright (c) 2022 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/main/LICENSE
//

using Karamem0.Capreze.Configuration;
using Karamem0.Capreze.Interactivity;
using Karamem0.Capreze.Services;
using Karamem0.Capreze.ViewModels;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

        public static readonly IHost Host =
            new HostBuilder()
                .ConfigureAppConfiguration((context, configuration) =>
                    configuration.AddJsonFile("Capreze.config.json"))
                .ConfigureServices((context, services) =>
                    services
                        .AddApplicationInsightsTelemetryWorkerService()
                        .AddSingleton<AppSettings>()
                        .AddTransient<IWindowService, WindowService>()
                        .AddTransient<MainViewModel>())
                .Build();

        private readonly TelemetryClient telemetryClient = Host.Services.GetService<TelemetryClient>();

        private readonly AppSettings appSettings = Host.Services.GetService<AppSettings>();

        public Application()
        {
            Current.DispatcherUnhandledException += this.OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += this.OnUnhandledException;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            if (this.TryFindResource(nameof(ViewModelLocator)) is ViewModelLocator viewModelLocator)
            {
                if (viewModelLocator.MainViewModel is MainViewModel mainViewModel)
                {
                    this.appSettings.Load();
                    mainViewModel.CaptureHeight = this.appSettings.CaptureHeight;
                    mainViewModel.CaptureWidth = this.appSettings.CaptureWidth;
                    mainViewModel.IsOffsetEnabled = this.appSettings.IsOffsetEnabled;
                    mainViewModel.IsTopmost = this.appSettings.IsTopmost;
                }
            }
            this.telemetryClient.TrackEvent("Application.OnStartup");
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (this.TryFindResource(nameof(ViewModelLocator)) is ViewModelLocator viewModelLocator)
            {
                if (viewModelLocator.MainViewModel is MainViewModel mainViewModel)
                {
                    this.appSettings.CaptureHeight = mainViewModel.CaptureHeight;
                    this.appSettings.CaptureWidth = mainViewModel.CaptureWidth;
                    this.appSettings.IsOffsetEnabled = mainViewModel.IsOffsetEnabled;
                    this.appSettings.IsTopmost = mainViewModel.IsTopmost;
                    this.appSettings.Save();
                }
            }
            this.telemetryClient.TrackEvent("Application.OnExit");
            this.telemetryClient.Flush();
            base.OnExit(e);
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            this.telemetryClient.TrackException(e.Exception);
            this.telemetryClient.Flush();
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            this.telemetryClient.TrackException(e.ExceptionObject as Exception);
            this.telemetryClient.Flush();
        }

    }

}
