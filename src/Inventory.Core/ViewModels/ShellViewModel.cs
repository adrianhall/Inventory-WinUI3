using CommunityToolkit.Mvvm.ComponentModel;
using Inventory.Core.Services;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Inventory.Core.ViewModels;

public partial class ShellViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly INavigationViewService _viewService;
    private readonly ILogger _logger;

    [ObservableProperty]
    private bool _isBackEnabled;

    [ObservableProperty]
    private object? _selected;

    public ShellViewModel(INavigationService navigationService, INavigationViewService viewService, ILogger<ShellViewModel> logger)
    {
        _navigationService = navigationService;
        _viewService = viewService;
        _logger = logger;
        _navigationService.Navigated += OnNavigated;
    }

    public void InitializeView(Frame frame, NavigationView navigationView)
    {
        _logger.LogTrace("InitializeView");
        _navigationService.NavigationFrame = frame;
        _viewService.Initialize(navigationView);
    }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        _logger.LogTrace("OnNavigated: {SourcePageType}", e.SourcePageType.Name);
        IsBackEnabled = _navigationService.CanGoBack;

        var selectedItem = _viewService.GetSelectedItem(e.SourcePageType);
        if (selectedItem is not null)
        {
            _logger.LogDebug("Setting selected to {selectedItem}", selectedItem.Tag);
            Selected = selectedItem;
        }
    }

}
