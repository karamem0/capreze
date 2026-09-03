//
// Copyright (c) 2019-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/main/LICENSE
//

using Karamem0.Capreze.Infrastructure;
using Karamem0.Capreze.Models;
using Karamem0.Capreze.Properties;
using Karamem0.Capreze.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Karamem0.Capreze.ViewModels;

public class MainViewModel(
    IConfigurationService configurationService,
    IProcessService processService,
    IWindowService windowService
) : ViewModelBase
{

    private readonly IConfigurationService configurationService = configurationService;

    private readonly IProcessService processService = processService;

    private readonly IWindowService windowService = windowService;

    public IntPtr WindowHandle
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                this.RaisePropertyChanged(nameof(this.WindowHandle));
            }
        }
    }

    public int ActualHeight
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                this.RaisePropertyChanged(nameof(this.ActualHeight));
            }
        }
    }

    public int ActualWidth
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                this.RaisePropertyChanged(nameof(this.ActualWidth));
            }
        }
    }

    public int CaptureHeight
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                this.RaisePropertyChanged(nameof(this.CaptureHeight));
            }
        }
    }

    public int CaptureWidth
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                this.RaisePropertyChanged(nameof(this.CaptureWidth));
            }
        }
    }

    public bool IsOffsetChanged
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                this.RaisePropertyChanged(nameof(this.IsOffsetChanged));
            }
        }
    }

    public bool IsOffsetEnabled
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                this.RaisePropertyChanged(nameof(this.IsOffsetEnabled));
            }
        }
    }

    public int OffsetX
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                this.RaisePropertyChanged(nameof(this.OffsetX));
            }
        }
    }

    public int OffsetY
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                this.RaisePropertyChanged(nameof(this.OffsetY));
            }
        }
    }

    public int SelectedHeight
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                this.RaisePropertyChanged(nameof(this.SelectedHeight));
            }
        }
    }

    public int SelectedWidth
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                this.RaisePropertyChanged(nameof(this.SelectedWidth));
            }
        }
    }

    public bool IsTopmost
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                this.RaisePropertyChanged(nameof(this.IsTopmost));
            }
        }
    }

    public bool AutoResize
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                this.RaisePropertyChanged(nameof(this.AutoResize));
            }
        }
    }

    public WindowInformation? SelectedInformation
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                this.RaisePropertyChanged(nameof(this.SelectedInformation));
            }
        }
    }

    public Visibility SelectedInformationVisibility
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                this.RaisePropertyChanged(nameof(this.SelectedInformationVisibility));
            }
        }
    }

    public ObservableCollection<WindowInformation> WindowInformations { get; } = [];

    public ObservableCollection<WindowSize> WindowSizes { get; } = [];

    public InteractionRequest ErrorRequest { get; } = new();

    public ICommand ApplyToCaptureSizeCommand => new DelegateCommand(() =>
        {
            this.CaptureHeight = this.SelectedHeight;
            this.CaptureWidth = this.SelectedWidth;
        }
    );

    public ICommand LoadWindowInformationsCommand => new DelegateCommand(async () =>
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
        }
    );

    public ICommand LoadWindowSizesCommand => new DelegateCommand(async () =>
        {
            this.WindowSizes.Clear();
            var values = await this.configurationService.GetWindowSizesAsync();
            if (values is not null)
            {
                foreach (var value in values)
                {
                    this.WindowSizes.Add(value);
                }
            }
        }
    );

    public ICommand LoadOffsetCommand => new DelegateCommand(async () =>
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
        }
    );

    public ICommand MaximizeWindowCommand => new DelegateCommand(async () =>
        {
            if (this.SelectedInformation is not null)
            {
                await this.windowService.MaximizeWindowAsync(this.SelectedInformation.Hwnd);
            }
        }
    );

    public ICommand MinimizeWindowCommand => new DelegateCommand(async () =>
        {
            if (this.SelectedInformation is not null)
            {
                await this.windowService.MinimizeWindowAsync(this.SelectedInformation.Hwnd);
            }
        }
    );

    public ICommand OffsetChangedCommand => new DelegateCommand(() =>
        {
            if (this.SelectedInformation is not null)
            {
                this.IsOffsetChanged = true;
            }
        }
    );

    public ICommand OpenUriCommand => new DelegateCommand<Uri>(async (parameter) =>
        {
            try
            {
                if (parameter is not null)
                {
                    await this.processService.OpenUriAsync(parameter);
                }
            }
            catch
            {
                this.ErrorRequest.Raise(
                    new ErrorViewModel()
                    {
                        Content = Resources.OpenUriErrorText
                    }
                );
            }
        }
    );

    public ICommand PresetCommand => new DelegateCommand<WindowSize>(parameter =>
        {
            if (parameter is not null)
            {
                this.CaptureHeight = parameter.Height;
                this.CaptureWidth = parameter.Width;
                if (this.AutoResize)
                {
                    this.ResizeCommand.Execute(null);
                }
            }
        }
    );

    public ICommand ResizeCommand => new DelegateCommand(async () =>
        {
            if (this.SelectedInformation is not null)
            {
                await this.windowService.ResizeWindowAsync(
                    this.SelectedInformation.Hwnd,
                    this.ActualWidth,
                    this.ActualHeight
                );
            }
        }
    );

    public ICommand ShowWindowToTopCommand => new DelegateCommand(async () =>
        {
            if (this.SelectedInformation is not null)
            {
                await this.windowService.BringWindowToTopAsync(this.SelectedInformation.Hwnd);
            }
        }
    );

    public override void OnLoaded()
    {
        this.LoadWindowSizesCommand.Execute(null);
        this.LoadWindowInformationsCommand.Execute(null);
        this.LoadOffsetCommand.Execute(null);
        this.SelectedInformationVisibility = Visibility.Hidden;
    }

    public override void OnUnloaded() { }

    protected override async void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.PropertyName is nameof(this.CaptureHeight) or nameof(this.IsOffsetEnabled) or nameof(this.OffsetY))
        {
            var size = this.CaptureHeight;
            var offset = this.IsOffsetEnabled ? this.OffsetY : 0;
            this.ActualHeight = size + offset;
        }
        if (e.PropertyName is nameof(this.CaptureWidth) or nameof(this.IsOffsetEnabled) or nameof(this.OffsetX))
        {
            var size = this.CaptureWidth;
            var offset = this.IsOffsetEnabled ? this.OffsetX * 2 : 0;
            this.ActualWidth = size + offset;
        }
        if (e.PropertyName is nameof(this.SelectedInformation))
        {
            if (this.SelectedInformation is null)
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
