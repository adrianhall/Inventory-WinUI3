using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Inventory.Core.Services;

/// <summary>
/// The service definition for the navigation service.
/// </summary>
public interface INavigationService
{
    /// <summary>
    /// An event handler that can be subscribed to for navigation events.
    /// </summary>
    event NavigatedEventHandler? Navigated;

    /// <summary>
    /// <c>true</c> when the navigation service can navigate back.
    /// </summary>
    bool CanGoBack { get; }

    /// <summary>
    /// Navigates backwards in the navigation stack.
    /// </summary>
    /// <returns><c>true</c> if navigation occurred.</returns>
    bool GoBack();

    /// <summary>
    /// The <see cref="Frame"/> to use for navigation events.
    /// </summary>
    Frame? NavigationFrame { get; set; }

    /// <summary>
    /// Navigates to the page associated with the specified view model type
    /// </summary>
    /// <param name="viewModelType">The view model type.</param>
    /// <param name="parameter">The parameter for the view model/page.</param>
    /// <param name="clearNavigation">If true, clears the navigation stack.</param>
    /// <returns><c>true</c> if navigation occurred.</returns>
    bool Navigate(Type viewModelType, object? parameter = null, bool clearNavigation = false);
}

public static class INavigationServiceExtensions
{
    /// <summary>
    /// Navigates to the page associated with the specified view model type
    /// </summary>
    /// <typeparam name="TViewModel">The view model type.</typeparam>
    /// <param name="parameter">The parameter for the view model/page.</param>
    /// <param name="clearNavigation">If true, clears the navigation stack.</param>
    public static bool Navigate<TViewModel>(this INavigationService navigationService, object? parameter = null, bool clearNavigation = false) where TViewModel : ObservableObject
        => navigationService.Navigate(typeof(TViewModel), parameter, clearNavigation);
}
