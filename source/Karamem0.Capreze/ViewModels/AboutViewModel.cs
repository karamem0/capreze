//
// Copyright (c) 2019-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/main/LICENSE
//

using Karamem0.Capreze.Infrastructure;
using System.Reflection;

namespace Karamem0.Capreze.ViewModels;

public class AboutViewModel : ViewModelBase
{

    public string? Company
    {
        get
        {
            var type = this.GetType();
            var assembly = type.Assembly;
            var attribute = assembly.GetCustomAttribute<AssemblyCompanyAttribute>();
            return attribute?.Company;
        }
    }

    public string? Copyright
    {
        get
        {
            var type = this.GetType();
            var assembly = type.Assembly;
            var attribute = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>();
            return attribute?.Copyright;
        }
    }

    public string? Product
    {
        get
        {
            var assembly = this.GetType()
                .Assembly;
            var attribute = assembly.GetCustomAttribute<AssemblyProductAttribute>();
            return attribute?.Product;
        }
    }

    public string? Version
    {
        get
        {
            var type = this.GetType();
            var assembly = type.Assembly;
            var attribute = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>();
            return attribute?.Version;
        }
    }

    public override void OnLoaded() { }

    public override void OnUnloaded() { }

}
