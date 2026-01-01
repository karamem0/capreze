//
// Copyright (c) 2019-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/main/LICENSE
//

using System.Windows.Input;

namespace Karamem0.Capreze.Infrastructure;

public class DelegateCommand(Action onExecute, Func<bool>? onCanExecute = null) : ICommand
{

    public event EventHandler? CanExecuteChanged;

    private readonly Action onExecute = onExecute;

    private readonly Func<bool> onCanExecute = onCanExecute ?? delegate { return true; };

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
