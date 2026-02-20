//
// Copyright (c) 2019-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/main/LICENSE
//

namespace Karamem0.Capreze.Infrastructure;

public class InteractionRequestedEventArgs(object? parameter, Action<object?> callback) : EventArgs
{

    public object? Parameter { get; } = parameter;

    public Action<object?> Callback { get; } = callback;

}
