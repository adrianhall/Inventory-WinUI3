using Microsoft.UI.Xaml.Controls;

namespace Inventory.Core.Services;

/// <summary>
/// The concrete implementation of the navigation view service.
/// </summary>
/// <param name="navigationService">The navigation service to use.</param>
/// <param name="viewMapper">The view to view model mapper</param>
public class NavigationViewService(INavigationService navigationService, IViewMapper viewMapper) : INavigationViewService
{
    private NavigationView _navigationView = default!;

    #region INavigationViewService Implementation
    /// <summary>
    /// The menu items to display in the navigation view.
    /// </summary>
    public IList<object> MenuItems
        => _navigationView.MenuItems;

    /// <summary>
    /// Gets the selected navigation view item.
    /// </summary>
    /// <param name="pageType">The page type selected.</param>
    /// <returns>The navigation view item.</returns>
    public NavigationViewItem? GetSelectedItem(Type pageType)
        => GetSelectedItem(_navigationView.MenuItems, pageType) ?? GetSelectedItem(_navigationView.FooterMenuItems, pageType);

    /// <summary>
    /// Initializes the navigation view service using an underlying navigation view.
    /// </summary>
    /// <param name="navigationView">The navigation view driving the service.</param>
    public void Initialize(NavigationView navigationView)
    {
        _navigationView = navigationView;
        _navigationView.BackRequested += OnBackRequested;
        _navigationView.ItemInvoked += OnItemInvoked;
    }

    /// <summary>
    /// Unregisters any events registered by the service.
    /// </summary>
    public void UnregisterEvents()
    {
        _navigationView.BackRequested -= OnBackRequested;
        _navigationView.ItemInvoked -= OnItemInvoked;
    }
    #endregion

    /// <summary>
    /// Event handler called when the back button is pressed on the navigation.
    /// </summary>
    private void OnBackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
    {
        navigationService.GoBack();
    }

    /// <summary>
    /// Event handler called when an item is selected in the navigation view.
    /// </summary>
    private void OnItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
    {
        if (args.IsSettingsInvoked)
        {
            // Navigate to the settings view
            // navigationService.Navigate(typeof(SettingsViewModel));
        }
        else
        {
            var selectedItem = args.InvokedItemContainer as NavigationViewItem;
            if (selectedItem?.GetValue(NavigationViewItem.TagProperty) is string pageType)
            {
                navigationService.Navigate(viewMapper.GetViewModelFromKey(pageType));
            }
        }
    }

    private NavigationViewItem? GetSelectedItem(IEnumerable<object> menuItems, Type pageType)
    {
        foreach (var item in menuItems.OfType<NavigationViewItem>())
        {
            if (item.GetValue(NavigationViewItem.TagProperty) is string key && viewMapper.GetViewModelFromKey(key) == pageType)
            {
                return item;
            }

            var selectedChild = GetSelectedItem(item.MenuItems, pageType);
            if (selectedChild is not null)
            {
                return selectedChild;
            }
        }
        return null;
    }
}
