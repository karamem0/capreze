//
// Copyright (c) 2019-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/main/LICENSE
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Karamem0.Capreze.Infrastructure;

public class DelegateCommand : ICommand
{

    public event EventHandler? CanExecuteChanged;

    private readonly Action onExecute;

    private readonly Func<bool> onCanExecute;

    public DelegateCommand(Action onExecute)
    {
        this.onExecute = onExecute;
        this.onCanExecute = delegate { return true; };
    }

    public DelegateCommand(Action onExecute, Func<bool> onCanExecute)
    {
        this.onExecute = onExecute;
        this.onCanExecute = onCanExecute;
    }

    public void RaiseCanExecuteChanged()
    {
        this.OnCanExecuteChanged(new EventArgs());
    }

    protected virtual void OnCanExecuteChanged(EventArgs e)
    {
        this.CanExecuteChanged?.Invoke(this, e);
    }

    void ICommand.Execute(object? parameter)
    {
        this.onExecute?.Invoke();
    }

    bool ICommand.CanExecute(object? parameter)
    {
        return this.onCanExecute is not null && this.onCanExecute.Invoke();
    }

}
