//
// Copyright (c) 2022 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/main/LICENSE
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

        private readonly IWindowService windowService;

        public MainViewModel()
        {
            this.WindowInformations = new ObservableCollection<WindowInformation>();
        }

        public MainViewModel(IWindowService windowService) : this()
        {
            this.windowService = windowService;
        }

        private IntPtr windowHandle;

        public IntPtr WindowHandle
        {
            get => this.windowHandle;
            set
            {
                if (this.windowHandle != value)
                {
                    this.windowHandle = value;
                    this.RaisePropertyChanged(nameof(this.WindowHandle));
                }
            }
        }

        private int actualHeight;

        public int ActualHeight
        {
            get => this.actualHeight;
            set
            {
                if (this.actualHeight != value)
                {
                    this.actualHeight = value;
                    this.RaisePropertyChanged(nameof(this.ActualHeight));
                }
            }
        }

        private int actualWidth;

        public int ActualWidth
        {
            get => this.actualWidth;
            set
            {
                if (this.actualWidth != value)
                {
                    this.actualWidth = value;
                    this.RaisePropertyChanged(nameof(this.ActualWidth));
                }
            }
        }

        private int captureHeight;

        public int CaptureHeight
        {
            get => this.captureHeight;
            set
            {
                if (this.captureHeight != value)
                {
                    this.captureHeight = value;
                    this.RaisePropertyChanged(nameof(this.CaptureHeight));
                }
            }
        }

        private int captureWidth;

        public int CaptureWidth
        {
            get => this.captureWidth;
            set
            {
                if (this.captureWidth != value)
                {
                    this.captureWidth = value;
                    this.RaisePropertyChanged(nameof(this.CaptureWidth));
                }
            }
        }

        private bool isOffsetChanged;

        public bool IsOffsetChanged
        {
            get => this.isOffsetChanged;
            set
            {
                if (this.isOffsetChanged != value)
                {
                    this.isOffsetChanged = value;
                    this.RaisePropertyChanged(nameof(this.IsOffsetChanged));
                }
            }
        }

        private bool isOffsetEnabled;

        public bool IsOffsetEnabled
        {
            get => this.isOffsetEnabled;
            set
            {
                if (this.isOffsetEnabled != value)
                {
                    this.isOffsetEnabled = value;
                    this.RaisePropertyChanged(nameof(this.IsOffsetEnabled));
                }
            }
        }

        private int offsetX;

        public int OffsetX
        {
            get => this.offsetX;
            set
            {
                if (this.offsetX != value)
                {
                    this.offsetX = value;
                    this.RaisePropertyChanged(nameof(this.OffsetX));
                }
            }
        }

        private int offsetY;

        public int OffsetY
        {
            get => this.offsetY;
            set
            {
                if (this.offsetY != value)
                {
                    this.offsetY = value;
                    this.RaisePropertyChanged(nameof(this.OffsetY));
                }
            }
        }

        private int selectedHeight;

        public int SelectedHeight
        {
            get => this.selectedHeight;
            set
            {
                if (this.selectedHeight != value)
                {
                    this.selectedHeight = value;
                    this.RaisePropertyChanged(nameof(this.SelectedHeight));
                }
            }
        }

        private int selectedWidth;

        public int SelectedWidth
        {
            get => this.selectedWidth;
            set
            {
                if (this.selectedWidth != value)
                {
                    this.selectedWidth = value;
                    this.RaisePropertyChanged(nameof(this.SelectedWidth));
                }
            }
        }

        private bool isTopmost;

        public bool IsTopmost
        {
            get => this.isTopmost;
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
            get => this.selectedInformation;
            set
            {
                if (this.selectedInformation != value)
                {
                    this.selectedInformation = value;
                    this.RaisePropertyChanged(nameof(this.SelectedInformation));
                }
            }
        }

        private Visibility selectedInformationVisibility;

        public Visibility SelectedInformationVisibility
        {
            get => this.selectedInformationVisibility;
            set
            {
                if (this.selectedInformationVisibility != value)
                {
                    this.selectedInformationVisibility = value;
                    this.RaisePropertyChanged(nameof(this.SelectedInformationVisibility));
                }
            }
        }

        public ObservableCollection<WindowInformation> WindowInformations { get; }

        public ICommand LoadWindowCommand =>
            new DelegateCommand(async () =>
            {
                var oldValues = this.WindowInformations;
                var newValues = await this.windowService.GetWindowInformationsAsync();
                for (var index = oldValues.Count - 1; index >= 0; index--)
                {
                    var newValue = newValues.SingleOrDefault(item => item.Hwnd == oldValues[index].Hwnd);
                    if (newValue is null)
                    {
                        oldValues.RemoveAt(index);
                    }
                }
                foreach (var newValue in newValues)
                {
                    var oldValue = oldValues.SingleOrDefault(item => item.Hwnd == newValue.Hwnd);
                    if (oldValue is null)
                    {
                        oldValues.Add(newValue);
                    }
                }
                if (this.SelectedInformation is not null)
                {
                    var wi = await this.windowService.GetWindowRectangleAsync(this.SelectedInformation.Hwnd);
                    this.SelectedHeight = wi.Height;
                    this.SelectedWidth = wi.Width;
                    this.LoadOffsetCommand.Execute(null);
                }
            });

        public ICommand LoadOffsetCommand =>
            new DelegateCommand(async () =>
            {
                if (this.IsOffsetChanged is not true)
                {
                    if (this.SelectedInformation is null)
                    {
                        this.OffsetX = await this.windowService.GetOffsetXAsync(this.WindowHandle);
                        this.OffsetY = await this.windowService.GetOffsetYAsync(this.WindowHandle);
                    }
                    else
                    {
                        this.OffsetX = await this.windowService.GetOffsetXAsync(this.SelectedInformation.Hwnd);
                        this.OffsetY = await this.windowService.GetOffsetYAsync(this.SelectedInformation.Hwnd);
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
                if (this.SelectedInformation is not null)
                {
                    var hwnd = this.SelectedInformation.Hwnd;
                    var width = this.ActualWidth;
                    var height = this.ActualHeight;
                    await this.windowService.ResizeWindowAsync(hwnd, width, height);
                }
            });

        public ICommand OffsetChangedCommand =>
            new DelegateCommand(() =>
            {
                if (this.SelectedInformation is not null)
                {
                    this.IsOffsetChanged = true;
                }
            });

        public override void OnLoaded()
        {
            this.LoadWindowCommand.Execute(null);
            this.LoadOffsetCommand.Execute(null);
            this.SelectedInformationVisibility = Visibility.Hidden;
        }

        public override void OnUnloaded()
        {
        }

        protected override async void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.PropertyName is
                nameof(this.CaptureHeight) or
                nameof(this.IsOffsetEnabled) or
                nameof(this.OffsetY))
            {
                var size = this.CaptureHeight;
                var offset = this.IsOffsetEnabled ? this.OffsetY : 0;
                this.ActualHeight = size + offset;
            }
            if (e.PropertyName is
                nameof(this.CaptureWidth) or
                nameof(this.IsOffsetEnabled) or
                nameof(this.OffsetX))
            {
                var size = this.CaptureWidth;
                var offset = this.IsOffsetEnabled ? this.OffsetX * 2 : 0;
                this.ActualWidth = size + offset;
            }
            if (e.PropertyName is nameof(this.SelectedInformation))
            {
                if (this.selectedInformation is null)
                {
                    this.SelectedInformationVisibility = Visibility.Hidden;
                }
                else
                {
                    this.IsOffsetChanged = false;
                    this.SelectedInformationVisibility = Visibility.Visible;
                    var wi = await this.windowService.GetWindowRectangleAsync(this.SelectedInformation.Hwnd);
                    this.SelectedHeight = wi.Height;
                    this.SelectedWidth = wi.Width;
                    this.LoadOffsetCommand.Execute(null);
                }
            }
        }

    }

}
