//
// Copyright (c) 2019-2024 karamem0
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
using Nelibur.ObjectMapper;
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
                        .AddTransient<IConfigurationService, ConfigurationService>()
                        .AddTransient<IWindowService, WindowService>()
                        .AddTransient<MainViewModel>()
                        .AddTinyMapper())
                .Build();

        private readonly TelemetryClient telemetryClient;

        private readonly AppSettings appSettings;

        public Application()
        {
            this.telemetryClient = Host.Services.GetService<TelemetryClient>() ?? throw new ArgumentNullException(nameof(this.telemetryClient));
            this.appSettings = Host.Services.GetService<AppSettings>() ?? throw new ArgumentNullException(nameof(this.appSettings));
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Current.DispatcherUnhandledException += this.OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += this.OnUnhandledException;
            if (this.TryFindResource(nameof(ViewModelLocator)) is ViewModelLocator viewModelLocator)
            {
                if (viewModelLocator.MainViewModel is MainViewModel mainViewModel)
                {
                    this.appSettings.Load();
                    _ = TinyMapper.Map(this.appSettings, mainViewModel);
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
                    _ = TinyMapper.Map(mainViewModel, this.appSettings);
                    this.appSettings.Save();
                }
            }
            this.telemetryClient.TrackEvent("Application.OnExit");
            this.telemetryClient.Flush();
            Current.DispatcherUnhandledException -= this.OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException -= this.OnUnhandledException;
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
