//
// Copyright (c) 2019-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/main/LICENSE
//

using Karamem0.Capreze.Configuration;
using Karamem0.Capreze.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Nelibur.ObjectMapper;

namespace Karamem0.Capreze;

public static class ServiceExtensions
{

    public static IServiceCollection AddTinyMapper(this IServiceCollection services)
    {
        TinyMapper.Bind<AppSettings, MainViewModel>();
        TinyMapper.Bind<MainViewModel, AppSettings>();
        return services;
    }

}
