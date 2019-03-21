//
// Copyright (c) 2019 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/Capreze/blob/master/LICENSE
//

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Capreze.Configuration
{

    public abstract class AppSettingsBase
    {

        protected AppSettingsBase()
        {
        }

        public void Load()
        {
            var serializer = new JsonSerializer();
            var type = this.GetType();
            var file = new FileInfo(Path.ChangeExtension(type.Assembly.Location, "json"));
            if (file.Exists)
            {
                using (var stream = file.Open(FileMode.Open, FileAccess.Read))
                using (var reader = new StreamReader(stream, new UTF8Encoding(false)))
                {
                    var value = serializer.Deserialize(reader, type);
                    foreach (var property in type.GetProperties())
                    {
                        property.SetValue(this, property.GetValue(value, null));
                    }
                }
            }
        }

        public void Save()
        {
            var serializer = new JsonSerializer();
            var type = this.GetType();
            var file = new FileInfo(Path.ChangeExtension(type.Assembly.Location, "json"));
            using (var stream = file.Open(FileMode.Create, FileAccess.Write))
            using (var writer = new StreamWriter(stream, new UTF8Encoding(false)))
            {
                serializer.Serialize(writer, this);
            }
        }

    }

}
