namespace Datalagring;

using System.Windows;
using Datalagring.ViewModels;
using Datalagring.Views;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private MainWindow _mainWindow;
    private MainWindowViewModel _mainWindowViewModel;
    private CreateTicketViewModel _createTicketViewModel;
    private MainViewModel _mainViewModel;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        _createTicketViewModel = new CreateTicketViewModel();
        _mainViewModel = new MainViewModel();

        _mainWindowViewModel = new MainWindowViewModel(_createTicketViewModel, _mainViewModel);
        _mainWindow = new MainWindow
        {
            DataContext = _mainWindowViewModel
        };

        _mainWindow.Show();
    }
}