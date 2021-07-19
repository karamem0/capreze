//
// Copyright (c) 2021 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/master/LICENSE
//

using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Karamem0.Capreze.Configuration
{

    public class AppSettings
    {

        private static readonly string BasePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            ".capreze"
        );

        private static readonly string FileName = "settings.json";

        public AppSettings()
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

        public void Load()
        {
            try
            {
                var properties = typeof(AppSettings).GetProperties();
                foreach (var property in properties)
                {
                    var attribute = Attribute.GetCustomAttribute(property, typeof(DefaultValueAttribute)) as DefaultValueAttribute;
                    if (attribute != null)
                    {
                        property.SetValue(this, attribute.Value);
                    }
                    else
                    {
                        property.SetValue(this, null);
                    }
                }
                var file = new FileInfo(Path.Combine(BasePath, FileName));
                if (file.Exists)
                {
                    using var stream = file.OpenRead();
                    using var reader = new StreamReader(stream, Encoding.UTF8);
                    var json = reader.ReadToEnd();
                    var value = JsonSerializer.Deserialize<AppSettings>(json);
                    foreach (var property in properties)
                    {
                        property.SetValue(this, property.GetValue(value, null));
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        public void Save()
        {
            try
            {
                var file = new FileInfo(Path.Combine(BasePath, FileName));
                file.Directory.Create();
                using var stream = file.OpenWrite();
                using var writer = new StreamWriter(stream, Encoding.UTF8);
                var json = JsonSerializer.Serialize(this);
                writer.Write(json);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

    }

}
