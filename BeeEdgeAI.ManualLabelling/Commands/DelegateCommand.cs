using BeeEdgeAI.ManualLabelling.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BeeEdgeAI.ManualLabelling.Commands;

public abstract class DelegateCommand : BaseViewModel , ICommand
{
    public event EventHandler? CanExecuteChanged;
    public abstract bool CanExecute(object? parameter);
    public abstract void Execute(object? parameter);      

    public virtual void RaiseCanExecuteChanged() =>
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}
