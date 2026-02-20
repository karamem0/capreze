//
// Copyright (c) 2019-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/main/LICENSE
//

using Karamem0.Capreze.Infrastructure;
using Microsoft.Xaml.Behaviors;
using System.Windows;

namespace Karamem0.Capreze.Interactivity;

public class ShowDialogAction : TriggerAction<DependencyObject>
{

    public static readonly DependencyProperty DialogTypeProperty = DependencyProperty.Register(
        "DialogType",
        typeof(Type),
        typeof(ShowDialogAction)
    );

    public static readonly DependencyProperty DataContextProperty = DependencyProperty.Register(
        "DataContext",
        typeof(object),
        typeof(ShowDialogAction)
    );

    public Type? DialogType
    {
        get => (Type?)this.GetValue(DialogTypeProperty);
        set => this.SetValue(DialogTypeProperty, value);
    }

    public object? DataContext
    {
        get => this.GetValue(DataContextProperty);
        set => this.SetValue(DataContextProperty, value);
    }

    protected override void Invoke(object? parameter)
    {
        if (this.DialogType is not null)
        {
            var dialog = (Window?)Activator.CreateInstance(this.DialogType);
            if (dialog is not null)
            {
                dialog.Owner = Window.GetWindow(this.AssociatedObject);
                var dataContext = this.DataContext ?? parameter;
                if (dataContext is InteractionRequestedEventArgs args)
                {
                    dialog.DataContext = args.Parameter;
                    args.Callback(dialog.ShowDialog());
                }
                else
                {
                    dialog.DataContext = dataContext;
                    _ = dialog.ShowDialog();
                }
            }
        }
    }

}
