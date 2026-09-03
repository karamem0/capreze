//
// Copyright (c) 2019-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/main/LICENSE
//

using System.Windows;

namespace Karamem0.Capreze.Infrastructure;

public class InteractionRequest : DependencyObject
{

    public event EventHandler<InteractionRequestedEventArgs>? Raised;

    public void Raise(
        string name,
        object? parameter = null,
        Action<object?>? callback = null
    )
    {
        this.OnRaised(
            new InteractionRequestedEventArgs(
                name,
                parameter,
                callback ?? (_ => { })
            )
        );
    }

    protected virtual void OnRaised(InteractionRequestedEventArgs e)
    {
        this.Raised?.Invoke(this, e);
    }

}
