//
// Copyright (c) 2019-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/main/LICENSE
//

namespace Karamem0.Capreze.Models;

public record WindowSize
{

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public string? DisplayName => this.Name + (this.Description is null ? null : $"({this.Description})");

}
