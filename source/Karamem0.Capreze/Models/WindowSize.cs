//
// Copyright (c) 2022 karamem0
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

namespace Karamem0.Capreze.Models
{

    public record WindowSize
    {

        public WindowSize()
        {
        }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string? DisplayName => this.Name + (this.Description is null ? null : $"({this.Description})");

    }

}
