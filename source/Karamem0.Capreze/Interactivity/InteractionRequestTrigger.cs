//
// Copyright (c) 2019-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/main/LICENSE
//

using Karamem0.Capreze.Infrastructure;
using System.Windows;

namespace Karamem0.Capreze.Interactivity;

public class InteractionRequestTrigger : Microsoft.Xaml.Behaviors.EventTrigger
{

    public static readonly DependencyProperty NameProperty = DependencyProperty.Register(
        nameof(Name),
        typeof(string),
        typeof(InteractionRequestTrigger),
        new PropertyMetadata(null)
    );

    public string? Name
    {
        get => (string?)this.GetValue(NameProperty);
        set => this.SetValue(NameProperty, value);
    }

    protected override string GetEventName()
    {
        return "Raised";
    }

    protected override void OnEvent(EventArgs e)
    {
        if (e is InteractionRequestedEventArgs args)
        {
            if (this.Name == args.Name)
            {
                base.OnEvent(args);
            }
        }
    }

}
