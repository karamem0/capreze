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
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace Karamem0.Capreze.Interactivity;

public class WindowHandleBehavior : Behavior<Window>
{

    protected override void OnAttached()
    {
        this.AssociatedObject.Loaded += this.OnAssociatedObjectLoaded;
    }

    protected override void OnDetaching()
    {
        this.AssociatedObject.Loaded -= this.OnAssociatedObjectLoaded;
    }

    private void OnAssociatedObjectLoaded(object sender, RoutedEventArgs e)
    {
        WindowHandle.SetHandle(this.AssociatedObject, new WindowInteropHelper(this.AssociatedObject).Handle);
    }

}
