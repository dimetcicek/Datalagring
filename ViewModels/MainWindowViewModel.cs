namespace Datalagring.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly CreateTicketViewModel _createTicketViewModel;
    private readonly MainViewModel _mainViewModel;

    private ViewModelBase _currentViewModel;

    public MainWindowViewModel(CreateTicketViewModel createTicketViewModel, MainViewModel mainViewModel)
    {
        _createTicketViewModel = createTicketViewModel;
        _mainViewModel = mainViewModel;

        _createTicketViewModel.NavigationRequest += OnNavigationRequest;
        _mainViewModel.NavigationRequest += OnNavigationRequest;

        CurrentViewModel = _mainViewModel;
        CurrentViewModel.OnNavigatedTo();
    }

    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;

        set
        {
            _currentViewModel = value;
            RaisePropertyChanged();
        }
    }

    private void OnNavigationRequest(object? sender, NavigationEventArgs args)
    {
        if (args.ViewModelType == typeof(CreateTicketViewModel))
        {
            CurrentViewModel = _createTicketViewModel;
        }
        else if (args.ViewModelType == typeof(MainViewModel))
        {
            CurrentViewModel = _mainViewModel;
        }

        CurrentViewModel.OnNavigatedTo();
    }
}