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

        private bool isOffsetEnabled;

        private int offsetX;

        private int offsetY;

        private bool isTopmost;

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

        public bool IsOffsetEnabled
        {
            get { return this.isOffsetEnabled; }
            set
            {
                if (this.isOffsetEnabled != value)
                {
                    this.isOffsetEnabled = value;
                    this.RaisePropertyChanged(nameof(this.IsOffsetEnabled));
                }
            }
        }

        public int OffsetX
        {
            get { return this.offsetX; }
            set
            {
                if (this.offsetX != value)
                {
                    this.offsetX = value;
                    this.RaisePropertyChanged(nameof(this.OffsetX));
                }
            }
        }

        public int OffsetY
        {
            get { return this.offsetY; }
            set
            {
                if (this.offsetY != value)
                {
                    this.offsetY = value;
                    this.RaisePropertyChanged(nameof(this.OffsetY));
                }
            }
        }

        public bool IsTopmost
        {
            get { return this.isTopmost; }
            set
            {
                if (this.isTopmost != value)
                {
                    this.isTopmost = value;
                    this.RaisePropertyChanged(nameof(this.IsTopmost));
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

        public ICommand PresetCommand =>
            new DelegateCommand<Size>(parameter =>
                {
                    this.CaptureHeight = (int)parameter.Height;
                    this.CaptureWidth = (int)parameter.Width;
                });

        public ICommand ResizeCommand =>
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

        public override async void OnLoaded()
        {
            this.LoadCommand.Execute(null);
            this.OffsetX = await this.windowService.GetOffsetXAsync();
            this.OffsetY = await this.windowService.GetOffsetYAsync();
        }

        public override void OnUnloaded()
        {
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.PropertyName == nameof(this.CaptureHeight) ||
                e.PropertyName == nameof(this.IsOffsetEnabled) ||
                e.PropertyName == nameof(this.OffsetY))
            {
                var size = this.CaptureHeight;
                var offset = this.IsOffsetEnabled ? this.OffsetY : 0;
                this.ActualHeight = size + offset;
            }
            if (e.PropertyName == nameof(this.CaptureWidth) ||
                e.PropertyName == nameof(this.IsOffsetEnabled) ||
                e.PropertyName == nameof(this.OffsetX))
            {
                var size = this.CaptureWidth;
                var offset = this.IsOffsetEnabled ? this.OffsetX * 2 : 0;
                this.ActualWidth = size + offset;
            }
        }

    }

}

