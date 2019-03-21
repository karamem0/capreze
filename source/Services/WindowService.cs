//
// Copyright (c) 2019 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/Capreze/blob/master/LICENSE
//

using Karamem0.Capreze.Infrastructure;
using Karamem0.Capreze.Models;
using Karamem0.Capreze.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Capreze.Services
{

    public interface IWindowService
    {

        Task<int> GetOffisetXAsync();

        Task<int> GetOffisetYAsync();

        Task<IEnumerable<WindowInformation>> GetWindowInformationsAsync();

        Task ResizeWindowAsync(IntPtr hwnd, int width, int height);

    }

    public class WindowService : ServiceBase, IWindowService
    {

        public WindowService()
        {
        }

        public async Task<int> GetOffisetXAsync()
        {
            return await Task.Run(() =>
            {
                var size = User32.GetSystemMetrics((int)User32.SystemMetricIndex.SM_CXSIZEFRAME);
                var padding = User32.GetSystemMetrics((int)User32.SystemMetricIndex.SM_CXPADDEDBORDER);
                return (size + padding) - 1;
            });
        }

        public async Task<int> GetOffisetYAsync()
        {
            return await Task.Run(() =>
            {
                var size = User32.GetSystemMetrics((int)User32.SystemMetricIndex.SM_CYSIZEFRAME);
                var padding = User32.GetSystemMetrics((int)User32.SystemMetricIndex.SM_CXPADDEDBORDER);
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
