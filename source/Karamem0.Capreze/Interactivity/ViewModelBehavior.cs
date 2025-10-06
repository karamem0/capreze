//
// Copyright (c) 2019-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/main/LICENSE
//

using Karamem0.Capreze.Infrastructure;
using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Karamem0.Capreze.Interactivity;

public class ViewModelBehavior : Behavior<FrameworkElement>
{

    protected override void OnAttached()
    {
        this.AssociatedObject.Loaded += this.OnAssociatedObjectLoaded;
        this.AssociatedObject.Unloaded += this.OnAssociatedObjectUnloaded;
    }

    protected override void OnDetaching()
    {
        this.AssociatedObject.Loaded -= this.OnAssociatedObjectLoaded;
        this.AssociatedObject.Unloaded -= this.OnAssociatedObjectUnloaded;
    }

    private void OnAssociatedObjectLoaded(object sender, RoutedEventArgs e)
    {
        if (this.AssociatedObject.DataContext is ViewModelBase viewModel)
        {
            viewModel.OnLoaded();
        }
    }

    private void OnAssociatedObjectUnloaded(object sender, RoutedEventArgs e)
    {
        if (this.AssociatedObject.DataContext is ViewModelBase viewModel)
        {
            viewModel.OnUnloaded();
        }
    }

}
