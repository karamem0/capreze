//
// Copyright (c) 2019-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/main/LICENSE
//

using Karamem0.Capreze.Infrastructure;
using Karamem0.Capreze.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Karamem0.Capreze.Interactivity;

public class ViewModelLocator : DependencyObject
{

    public static readonly DependencyProperty AboutViewModelProperty = DependencyProperty.Register(
        "AboutViewModel",
        typeof(ViewModelBase),
        typeof(ViewModelLocator),
        new PropertyMetadata(Application.Host.Services.GetService<AboutViewModel>())
    );

    public ViewModelBase? AboutViewModel
    {
        get => (ViewModelBase?)this.GetValue(AboutViewModelProperty);
        set => this.SetValue(AboutViewModelProperty, value);
    }

    public static readonly DependencyProperty MainViewModelProperty = DependencyProperty.Register(
        "MainViewModel",
        typeof(ViewModelBase),
        typeof(ViewModelLocator),
        new PropertyMetadata(Application.Host.Services.GetService<MainViewModel>())
    );

    public ViewModelBase? MainViewModel
    {
        get => (ViewModelBase?)this.GetValue(MainViewModelProperty);
        set => this.SetValue(MainViewModelProperty, value);
    }

}
