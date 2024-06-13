//
// Copyright (c) 2019-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/main/LICENSE
//

using Karamem0.Capreze.Infrastructure;
using Karamem0.Capreze.Models;
using Karamem0.Capreze.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Karamem0.Capreze.Services;

public interface IWindowService
{

    Task<int> GetOffsetXAsync(IntPtr hwnd, CancellationToken cancellationToken = default);

    Task<int> GetOffsetYAsync(IntPtr hwnd, CancellationToken cancellationToken = default);

    Task<IEnumerable<WindowInformation>> GetWindowInformationsAsync(CancellationToken cancellationToken = default);

    Task<User32.Rectangle> GetWindowRectangleAsync(IntPtr hwnd, CancellationToken cancellationToken = default);

    Task ResizeWindowAsync(IntPtr hwnd, int width, int height, CancellationToken cancellationToken = default);

}

public class WindowService : ServiceBase, IWindowService
{

    public WindowService()
    {
    }

    public async Task<int> GetOffsetXAsync(IntPtr hwnd, CancellationToken cancellationToken = default)
    {
        return await Task.Run(() =>
        {
            var dpi = User32.GetDpiForWindow(hwnd);
            var size = User32.GetSystemMetricsForDpi((int)User32.SystemMetricIndex.SM_CXSIZEFRAME, dpi);
            var padding = User32.GetSystemMetricsForDpi((int)User32.SystemMetricIndex.SM_CXPADDEDBORDER, dpi);
            return size + padding - 1;
        }, cancellationToken);
    }

    public async Task<int> GetOffsetYAsync(IntPtr hwnd, CancellationToken cancellationToken = default)
    {
        return await Task.Run(() =>
        {
            var dpi = User32.GetDpiForWindow(hwnd);
            var size = User32.GetSystemMetricsForDpi((int)User32.SystemMetricIndex.SM_CYSIZEFRAME, dpi);
            var padding = User32.GetSystemMetricsForDpi((int)User32.SystemMetricIndex.SM_CXPADDEDBORDER, dpi);
            return size + padding - 1;
        }, cancellationToken);
    }

    public async Task<IEnumerable<WindowInformation>> GetWindowInformationsAsync(CancellationToken cancellationToken = default)
    {
        return await Task.Run(() =>
        {
            var result = new List<WindowInformation>();
            foreach (var process in Process.GetProcesses())
            {
                if (process.MainWindowHandle != IntPtr.Zero)
                {
                    try
                    {
                        result.Add(new WindowInformation()
                        {
                            Id = process.Id,
                            Hwnd = process.MainWindowHandle,
                            FilePath = process.MainModule?.FileName,
                            FileName = Path.GetFileName(process.MainModule?.FileName),
                            Title = string.Join("", process.MainWindowTitle)
                        });
                    }
                    catch { }
                }
            }
            return result;
        }, cancellationToken);
    }

    public async Task<User32.Rectangle> GetWindowRectangleAsync(IntPtr hwnd, CancellationToken cancellationToken = default)
    {
        return await Task.Run(() =>
        {
            var wi = new User32.WindowInfo();
            wi.Size = Marshal.SizeOf(wi);
            _ = User32.GetWindowInfo(hwnd, ref wi);
            return wi.Window;
        }, cancellationToken);
    }

    public async Task ResizeWindowAsync(IntPtr hwnd, int width, int height, CancellationToken cancellationToken = default)
    {
        await Task.Run(() =>
        {
            _ = User32.ShowWindow(hwnd, (uint)User32.ShowWindowFlags.SW_RESTORE);
            _ = User32.SetWindowPos(
                hwnd,
                (IntPtr)User32.WindowOrder.HWND_TOP,
                0, 0,
                width, height,
                (uint)User32.SetWindowPosFlags.SWP_NOMOVE);
        }, cancellationToken);
    }

}
