//
// Copyright (c) 2019-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/main/LICENSE
//

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Karamem0.Capreze.Services;

public interface IProcessService
{

    Task OpenBrowserAsync(Uri uri, CancellationToken cancellationToken = default);

}

public class ProcessService : IProcessService
{

    public Task OpenBrowserAsync(Uri uri, CancellationToken cancellationToken = default)
    {
        return Task.Run(
            () =>
            {
                _ = Process.Start(
                    new ProcessStartInfo
                    {
                        FileName = uri.ToString(),
                        UseShellExecute = true
                    }
                );
            },
            cancellationToken
        );
    }

}
