//
// Copyright (c) 2021 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/master/LICENSE
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
using System.Threading.Tasks;
using static Karamem0.Capreze.Runtime.InteropServices.User32;

namespace Karamem0.Capreze.Services
{

    public interface IWindowService
    {

        Task<int> GetOffsetXAsync(IntPtr hwnd);

        Task<int> GetOffsetYAsync(IntPtr hwnd);

        Task<IEnumerable<WindowInformation>> GetWindowInformationsAsync();

        Task<Rectangle> GetWindowRectangleAsync(IntPtr hwnd);

        Task ResizeWindowAsync(IntPtr hwnd, int width, int height);

    }

    public class WindowService : ServiceBase, IWindowService
    {

        public WindowService()
        {
        }

        public async Task<int> GetOffsetXAsync(IntPtr hwnd)
        {
            return await Task.Run(() =>
            {
                var dpi = User32.GetDpiForWindow(hwnd);
                var size = User32.GetSystemMetricsForDpi((int)User32.SystemMetricIndex.SM_CXSIZEFRAME, dpi);
                var padding = User32.GetSystemMetricsForDpi((int)User32.SystemMetricIndex.SM_CXPADDEDBORDER, dpi);
                return (size + padding) - 1;
            });
        }

        public async Task<int> GetOffsetYAsync(IntPtr hwnd)
        {
            return await Task.Run(() =>
            {
                var dpi = User32.GetDpiForWindow(hwnd);
                var size = User32.GetSystemMetricsForDpi((int)User32.SystemMetricIndex.SM_CYSIZEFRAME, dpi);
                var padding = User32.GetSystemMetricsForDpi((int)User32.SystemMetricIndex.SM_CXPADDEDBORDER, dpi);
                return (size + padding) - 1;
            });
        }

        public async Task<IEnumerable<WindowInformation>> GetWindowInformationsAsync()
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
                                Hwnd = process.MainWindowHandle,
                                FilePath = process.MainModule.FileName,
                                FileName = Path.GetFileName(process.MainModule.FileName),
                                Title = string.Join("", process.MainWindowTitle)
                            });
                        }
                        catch { }
                    }
                }
                return result;
            });
        }

        public async Task<Rectangle> GetWindowRectangleAsync(IntPtr hwnd)
        {
            return await Task.Run(() =>
            {
                var wi = new WindowInfo();
                wi.Size = Marshal.SizeOf(wi);
                User32.GetWindowInfo(hwnd, ref wi);
                return wi.Window;
            });
        }

        public async Task ResizeWindowAsync(IntPtr hwnd, int width, int height)
        {
            await Task.Run(() =>
            {
                User32.ShowWindow(hwnd, (uint)User32.ShowWindowFlags.SW_RESTORE);
                User32.SetWindowPos(
                    hwnd,
                    (IntPtr)User32.WindowOrder.HWND_TOP,
                    0, 0,
                    width, height,
                    (uint)User32.SetWindowPosFlags.SWP_NOMOVE);
            });
        }

    }

}
