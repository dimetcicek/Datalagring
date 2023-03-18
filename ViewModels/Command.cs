namespace Datalagring.ViewModels;

using System;
using System.Windows.Input;

public class Command : ICommand
{
    private readonly Action _action;

    public Command(Action action)
    {
        _action = action;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        _action?.Invoke();
    }
}