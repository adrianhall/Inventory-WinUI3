namespace Inventory.Core.Services;

/// <summary>
/// An interface that you can implement on a view model to be notified of navigation events.
/// </summary>
public interface INavigationLifecycle
{
    /// <summary>
    /// The method that is called when the view model is navigated from.
    /// </summary>
    void OnNavigatedFrom();

    /// <summary>
    /// The method that is called when the view model is navigated to.
    /// </summary>
    void OnNavigatedTo();
}
