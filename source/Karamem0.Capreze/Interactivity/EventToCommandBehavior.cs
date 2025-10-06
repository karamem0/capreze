//
// Copyright (c) 2019-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/main/LICENSE
//

using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Karamem0.Capreze.Interactivity;

public class EventToCommandBehavior : Behavior<DependencyObject>
{

    public static readonly DependencyProperty EventNameProperty = DependencyProperty.Register(
        "EventName",
        typeof(string),
        typeof(EventToCommandBehavior)
    );

    public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
        "Command",
        typeof(ICommand),
        typeof(EventToCommandBehavior)
    );

    public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
        "CommandParameter",
        typeof(object),
        typeof(EventToCommandBehavior)
    );

    private EventInfo? eventInfo;

    private Delegate? eventDelegate;

    public string EventName
    {
        get => (string)this.GetValue(EventNameProperty);
        set => this.SetValue(EventNameProperty, value);
    }

    public ICommand Command
    {
        get => (ICommand)this.GetValue(CommandProperty);
        set => this.SetValue(CommandProperty, value);
    }

    public object CommandParameter
    {
        get => this.GetValue(CommandParameterProperty);
        set => this.SetValue(CommandParameterProperty, value);
    }

    protected override void OnAttached()
    {
        if (this.EventName is not null)
        {
            this.eventInfo = this
                .AssociatedObject.GetType()
                .GetEvent(this.EventName);
            if (this.eventInfo is not null && this.eventInfo.EventHandlerType is not null)
            {
                this.eventDelegate = this
                    .GetType()
                    .GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                    .Single(x => x.Name is "OnEventRaised")
                    .CreateDelegate(this.eventInfo.EventHandlerType, this);
                this.eventInfo.AddEventHandler(this.AssociatedObject, this.eventDelegate);
            }
        }
    }

    protected override void OnDetaching()
    {
        this.eventInfo?.RemoveEventHandler(this.AssociatedObject, this.eventDelegate);
    }

    protected void OnEventRaised(object sender, EventArgs e)
    {
        if (this.Command is not null)
        {
            if (this.Command.CanExecute(this.CommandParameter))
            {
                this.Command.Execute(this.CommandParameter);
            }
        }
    }

}
