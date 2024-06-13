//
// Copyright (c) 2019-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/main/LICENSE
//

using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Karamem0.Capreze.Configuration;

public class AppSettings(TelemetryClient telemetryClient)
{

    private readonly FileInfo fileInfo = new(Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        ".capreze",
        "settings.json"
    ));

    private readonly TelemetryClient telemetryClient = telemetryClient;

    [DefaultValue(600)]
    public int CaptureHeight { get; set; }

    [DefaultValue(800)]
    public int CaptureWidth { get; set; }

    [DefaultValue(true)]
    public bool IsOffsetEnabled { get; set; }

    [DefaultValue(false)]
    public bool IsTopmost { get; set; }

    [DefaultValue(false)]
    public bool AutoResize { get; set; }

    public async Task LoadAsync()
    {
        try
        {
            var properties = typeof(AppSettings).GetProperties();
            foreach (var property in properties)
            {
                if (Attribute.GetCustomAttribute(property, typeof(DefaultValueAttribute)) is DefaultValueAttribute attribute)
                {
                    property.SetValue(this, attribute.Value);
                }
                else
                {
                    property.SetValue(this, null);
                }
            }
            if (this.fileInfo.Exists)
            {
                using var stream = this.fileInfo.Open(FileMode.Open, FileAccess.Read);
                using var reader = new StreamReader(stream, Encoding.UTF8);
                var json = await reader.ReadToEndAsync();
                var values = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json);
                if (values is not null)
                {
                    foreach (var property in properties)
                    {
                        if (values.TryGetValue(property.Name, out var value))
                        {
                            if (value.ValueKind is JsonValueKind.True)
                            {
                                property.SetValue(this, value.GetBoolean());
                            }
                            if (value.ValueKind is JsonValueKind.False)
                            {
                                property.SetValue(this, value.GetBoolean());
                            }
                            if (value.ValueKind is JsonValueKind.Number)
                            {
                                property.SetValue(this, value.GetInt32());
                            }
                            if (value.ValueKind is JsonValueKind.String)
                            {
                                property.SetValue(this, value.GetString());
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            this.telemetryClient.TrackException(ex);
        }
    }

    public async Task SaveAsync()
    {
        _ = this.fileInfo.Directory ?? throw new DirectoryNotFoundException();
        try
        {
            this.fileInfo.Directory.Create();
            using var stream = this.fileInfo.Open(FileMode.Create, FileAccess.Write);
            using var writer = new StreamWriter(stream, Encoding.UTF8);
            var json = JsonSerializer.Serialize(this);
            await writer.WriteAsync(json);
        }
        catch (Exception ex)
        {
            this.telemetryClient.TrackException(ex);
        }
    }

}
