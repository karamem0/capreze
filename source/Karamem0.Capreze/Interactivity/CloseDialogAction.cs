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

public class CloseDialogAction : TriggerAction<Window>
{

    public static readonly DependencyProperty DialogResultProperty = DependencyProperty.Register(
        "DialogResult",
        typeof(bool?),
        typeof(CloseDialogAction)
    );

    public bool? DialogResult
    {
        get => (bool?)this.GetValue(DialogResultProperty);
        set => this.SetValue(DialogResultProperty, value);
    }

    protected override void Invoke(object? parameter)
    {
        if (this.AssociatedObject is not null)
        {
            var dataContext = this.DialogResult ?? parameter;
            if (dataContext is InteractionRequestedEventArgs args)
            {
                this.AssociatedObject.DialogResult = args.Parameter as bool?;
                this.AssociatedObject.Close();
                args.Callback(args.Parameter);
            }
            else
            {
                this.AssociatedObject.DialogResult = dataContext as bool?;
                this.AssociatedObject.Close();
            }
        }
    }

}
