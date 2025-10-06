//
// Copyright (c) 2019-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/main/LICENSE
//

using Karamem0.Capreze.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Karamem0.Capreze.Services;

public interface IConfigurationService
{

    Task<IEnumerable<WindowSize>?> GetWindowSizesAsync(CancellationToken cancellationToken = default);

}

public class ConfigurationService(IConfiguration configuration) : IConfigurationService
{

    private readonly IConfiguration configuration = configuration;

    public async Task<IEnumerable<WindowSize>?> GetWindowSizesAsync(CancellationToken cancellationToken = default)
    {
        return await Task.Run(
            () => this
                .configuration.GetSection(nameof(WindowSize))
                .Get<IEnumerable<WindowSize>?>(),
            cancellationToken
        );
    }

}
