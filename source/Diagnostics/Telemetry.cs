//
// Copyright (c) 2019 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/Capreze/blob/master/LICENSE
//

using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.WindowsServer.TelemetryChannel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Karamem0.Capreze.Diagnostics
{

    public class Telemetry
    {

        public static readonly Telemetry Instance = new Telemetry();

        private readonly TelemetryClient client;

        private Telemetry()
        {
            this.client = new TelemetryClient(new TelemetryConfiguration()
            {
                InstrumentationKey = TelemetryConstants.TelemetryKey,
                TelemetryChannel = new ServerTelemetryChannel()
                {
                    DeveloperMode = Debugger.IsAttached
                },
            });
            this.client.Context.Component.Version = Assembly.GetEntryAssembly().GetName().Version.ToString();
            this.client.Context.Session.Id = Guid.NewGuid().ToString();
            this.client.Context.User.Id = $"{Environment.MachineName}:{Environment.UserName}".GetHashCode().ToString();
            this.client.Context.Device.OperatingSystem = Environment.OSVersion.ToString();
        }

        public void TrackEvent(string key, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            this.client.TrackEvent(key, properties, metrics);
        }

        public void TrackException(Exception exception)
        {
            if (exception != null)
            {
                this.client.TrackException(exception);
                this.client.Flush();
            }
        }

    }

}
