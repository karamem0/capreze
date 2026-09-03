//
// Copyright (c) 2019-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/main/LICENSE
//

using Karamem0.Capreze.Configuration;
using Karamem0.Capreze.ViewModels;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Karamem0.Capreze;

public static class ServiceExtensions
{

    public static IServiceCollection AddMapster(this IServiceCollection services)
    {
        _ = TypeAdapterConfig.GlobalSettings.NewConfig<AppSettings, MainViewModel>();
        _ = TypeAdapterConfig.GlobalSettings.NewConfig<MainViewModel, AppSettings>();
        _ = services.AddTransient<IMapper, Mapper>();
        return services;
    }

}
