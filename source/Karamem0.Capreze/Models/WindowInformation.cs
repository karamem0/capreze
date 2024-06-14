//
// Copyright (c) 2019-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/main/LICENSE
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Capreze.Models;

public record WindowInformation
{

    public WindowInformation()
    {
    }

    public int Id { get; set; }

    public IntPtr Hwnd { get; set; }

    public string? FilePath { get; set; }

    public string? FileName { get; set; }

    public string? Title { get; set; }

}
