//
// Copyright (c) 2019-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/main/LICENSE
//

using System.ComponentModel;

namespace Karamem0.Capreze.Infrastructure;

public abstract class ViewModelBase : INotifyPropertyChanged
{

    public event PropertyChangedEventHandler? PropertyChanged;

    public abstract void OnLoaded();

    public abstract void OnUnloaded();

    public void RaisePropertyChanged(string propertyName)
    {
        this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        this.PropertyChanged?.Invoke(this, e);
    }

}
