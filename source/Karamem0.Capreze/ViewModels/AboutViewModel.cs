//
// Copyright (c) 2019-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/main/LICENSE
//

using Karamem0.Capreze.Infrastructure;
using Karamem0.Capreze.Services;
using System.Reflection;
using System.Windows.Input;

namespace Karamem0.Capreze.ViewModels;

public class AboutViewModel(IProcessService processService) : ViewModelBase
{

    private readonly IProcessService processService = processService;

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

    public ICommand OpenUriCommand => new DelegateCommand<Uri>(async (parameter) =>
        {
            if (parameter is not null)
            {
                await this.processService.OpenUriAsync(parameter);
            }
        }
    );

    public override void OnLoaded() { }

    public override void OnUnloaded() { }

}
