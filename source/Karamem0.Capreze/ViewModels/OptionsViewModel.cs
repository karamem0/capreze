//
// Copyright (c) 2019-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/main/LICENSE
//

using Karamem0.Capreze.Configuration;
using Karamem0.Capreze.Infrastructure;
using Karamem0.Capreze.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Karamem0.Capreze.ViewModels;

public class OptionsViewModel(AppSettings appSettings) : ViewModelBase
{

    private readonly AppSettings appSettings = appSettings;

    public ObservableCollection<WindowSize> WindowSizes { get; } = [];

    public ICommand CloseCommand => new DelegateCommand<bool?>(async parameter =>
        {
            if (parameter is true)
            {
                this.appSettings.WindowSizes = [.. this.WindowSizes];
                await this.appSettings.SaveAsync();
            }
            this.InteractionRequest.Raise("Close", parameter);
        }
    );

    public ICommand LoadCommand => new DelegateCommand(async () =>
        {
            this.WindowSizes.Clear();
            foreach (var value in this.appSettings.WindowSizes)
            {
                this.WindowSizes.Add(value);
            }
        }
    );

    public override void OnLoaded()
    {
        this.LoadCommand.Execute(null);
    }

    public override void OnUnloaded() { }

}
