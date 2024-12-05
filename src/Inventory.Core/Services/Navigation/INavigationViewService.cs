using Microsoft.UI.Xaml.Controls;

namespace Inventory.Core.Services;

/// <summary>
/// The service description for the navigation view service.
/// </summary>
public interface INavigationViewService
{
    /// <summary>
    /// The menu items to display in the navigation view.
    /// </summary>
    IList<object> MenuItems { get; }

    /// <summary>
    /// Gets the selected navigation view item.
    /// </summary>
    /// <param name="pageType">The page type selected.</param>
    /// <returns>The navigation view item.</returns>
    NavigationViewItem? GetSelectedItem(Type pageType);

    /// <summary>
    /// Initializes the navigation view service using an underlying navigation view.
    /// </summary>
    /// <param name="navigationView">The navigation view driving the service.</param>
    void Initialize(NavigationView navigationView);

    /// <summary>
    /// Unregisters any events registered by the service.
    /// </summary>
    void UnregisterEvents();
}
