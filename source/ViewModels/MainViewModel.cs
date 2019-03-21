//
// Copyright (c) 2019 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/Capreze/blob/master/LICENSE
//

using Karamem0.Capreze.Infrastructure;
using Karamem0.Capreze.Models;
using Karamem0.Capreze.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Karamem0.Capreze.ViewModels
{

    public class MainViewModel : ViewModelBase
    {

        private IWindowService windowService;

        private int actualHeight;

        private int actualWidth;

        private int captureHeight;

        private int captureWidth;

        public MainViewModel()
        {
            this.WindowInformations = new ObservableCollection<WindowInformation>();
        }

        public MainViewModel(IWindowService windowService) : this()
        {
            this.windowService = windowService;
        }

        public int ActualHeight
        {
            get { return this.actualHeight; }
            set
            {
                if (this.actualHeight != value)
                {
                    this.actualHeight = value;
                    this.RaisePropertyChanged(nameof(this.ActualHeight));
                }
            }
        }

        public int ActualWidth
        {
            get { return this.actualWidth; }
            set
            {
                if (this.actualWidth != value)
                {
                    this.actualWidth = value;
                    this.RaisePropertyChanged(nameof(this.ActualWidth));
                }
            }
        }

        public int CaptureHeight
        {
            get { return this.captureHeight; }
            set
            {
                if (this.captureHeight != value)
                {
                    this.captureHeight = value;
                    this.RaisePropertyChanged(nameof(this.CaptureHeight));
                }
            }
        }

        public int CaptureWidth
        {
            get { return this.captureWidth; }
            set
            {
                if (this.captureWidth != value)
                {
                    this.captureWidth = value;
                    this.RaisePropertyChanged(nameof(this.CaptureWidth));
                }
            }
        }

        private WindowInformation selectedInformation;

        public WindowInformation SelectedInformation
        {
            get { return this.selectedInformation; }
            set
            {
                if (this.selectedInformation != value)
                {
                    this.selectedInformation = value;
                    this.RaisePropertyChanged(nameof(this.SelectedInformation));
                }
            }
        }

        public ObservableCollection<WindowInformation> WindowInformations { get; }

        public ICommand LoadCommand =>
            new DelegateCommand(async () =>
                {
                    var oldValues = this.WindowInformations;
                    var newValues = await this.windowService.GetWindowInformationsAsync();
                    for (var index = oldValues.Count - 1; index >= 0; index--)
                    {
                        var newValue = newValues.SingleOrDefault(item => item.Hwnd == oldValues[index].Hwnd);
                        if (newValue == null)
                        {
                            oldValues.RemoveAt(index);
                        }
                    }
                    foreach (var newValue in newValues)
                    {
                        var oldValue = oldValues.SingleOrDefault(item => item.Hwnd == newValue.Hwnd);
                        if (oldValue == null)
                        {
                            oldValues.Add(newValue);
                        }
                    }
                });

        public ICommand ResizeCommand =>
            new DelegateCommand<Size>(parameter =>
                {
                    this.CaptureHeight = (int)parameter.Height;
                    this.CaptureWidth = (int)parameter.Width;
                });

        public ICommand ExecuteCommand =>
            new DelegateCommand(async () =>
                {
                    if (this.SelectedInformation != null)
                    {
                        var hwnd = this.SelectedInformation.Hwnd;
                        var width = this.ActualWidth;
                        var height = this.ActualHeight;
                        await this.windowService.ResizeWindowAsync(hwnd, width, height);
                    }
                });

        public override void OnLoaded()
        {
            this.LoadCommand.Execute(null);
        }

        public override void OnUnloaded()
        {
        }

        protected override async void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.PropertyName == nameof(this.CaptureHeight))
            {
                var offset = await this.windowService.GetOffisetYAsync();
                var height = this.CaptureHeight + offset;
                this.ActualHeight = height;
            }
            if (e.PropertyName == nameof(this.CaptureWidth))
            {
                var offset = await this.windowService.GetOffisetXAsync();
                var width = this.CaptureWidth + offset * 2;
                this.ActualWidth = width;
            }
        }

    }

}

