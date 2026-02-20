//
// Copyright (c) 2019-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/main/LICENSE
//

namespace Karamem0.Capreze.Infrastructure;

public class InteractionRequest
{

    public event EventHandler<InteractionRequestedEventArgs>? Raised;

    public void Raise(object? parameter)
    {
        this.OnRaised(new InteractionRequestedEventArgs(parameter, (parameter) => { }));
    }

    public void Raise(object? parameter, Action<object?> callback)
    {
        this.OnRaised(new InteractionRequestedEventArgs(parameter, callback));
    }

    protected virtual void OnRaised(InteractionRequestedEventArgs e)
    {
        this.Raised?.Invoke(this, e);
    }

}
