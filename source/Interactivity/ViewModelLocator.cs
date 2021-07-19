//
// Copyright (c) 2021 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/master/LICENSE
//

using Karamem0.Capreze.Infrastructure;
using Karamem0.Capreze.Services;
using Karamem0.Capreze.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Karamem0.Capreze.Interactivity
{

    public class ViewModelLocator : DependencyObject
    {

        private static readonly Lazy<IServiceProvider> ServiceProvider =
            new Lazy<IServiceProvider>(() => new ServiceCollection()
                .AddTransient<IWindowService, WindowService>()
                .AddTransient<MainViewModel>()
                .BuildServiceProvider());

        public static readonly DependencyProperty MainViewModelProperty =
            DependencyProperty.Register(
                "MainViewModel",
                typeof(ViewModelBase),
                typeof(ViewModelLocator),
                new PropertyMetadata(ServiceProvider.Value.GetService<MainViewModel>()));

        public ViewModelLocator()
        {
        }

        public ViewModelBase MainViewModel
        {
            get => (ViewModelBase)this.GetValue(MainViewModelProperty);
            set => this.SetValue(MainViewModelProperty, value);
        }

    }

}
