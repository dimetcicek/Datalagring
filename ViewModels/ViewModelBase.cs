namespace Datalagring.ViewModels;

using System.ComponentModel;
using System;
using System.Runtime.CompilerServices;

public class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public event EventHandler<NavigationEventArgs> NavigationRequest;

    public void NavigateTo(Type viewModelType)
    {
        NavigationRequest?.Invoke(this, new NavigationEventArgs(viewModelType));
    }

    public virtual void OnNavigatedTo()
    {
    }

    protected void RaisePropertyChanged([CallerMemberName]string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}