//
// Copyright (c) 2019-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/main/LICENSE
//

using Karamem0.Capreze.Infrastructure;
using System.Windows.Input;

namespace Karamem0.Capreze.ViewModels;

public class ErrorViewModel : ViewModelBase
{

    public string? Content
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                this.RaisePropertyChanged(nameof(this.Content));
            }
        }
    }

    public ICommand CloseCommand => new DelegateCommand<bool?>(parameter =>
        {
            this.InteractionRequest.Raise("Close", parameter);
        }
    );

    public override void OnLoaded() { }

    public override void OnUnloaded() { }

}
