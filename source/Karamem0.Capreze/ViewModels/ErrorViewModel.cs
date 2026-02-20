//
// Copyright (c) 2019-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/main/LICENSE
//

using Karamem0.Capreze.Infrastructure;

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

    public override void OnLoaded() { }

    public override void OnUnloaded() { }

}
