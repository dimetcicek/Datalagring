namespace Datalagring.ViewModels;

using System;

public class NavigationEventArgs : EventArgs
{
    public NavigationEventArgs(Type viewModelType)
    {
        ViewModelType = viewModelType;
    }

    public Type ViewModelType { get; }
}