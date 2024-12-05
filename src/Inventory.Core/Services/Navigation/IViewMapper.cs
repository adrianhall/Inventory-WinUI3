namespace Inventory.Core.Services;

/// <summary>
/// A mapper between views and view models.
/// </summary>
public interface IViewMapper
{
    /// <summary>
    /// Given the view model type, return the view type.
    /// </summary>
    /// <param name="viewModelType">The view model type.</param>
    /// <returns>The view type.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the view model type is not registered.</exception>
    Type GetViewFromViewModel(Type viewModelType);

    /// <summary>
    /// Gets the ViewModel type based on the key, which is usedf
    /// to translate the Tag on the NavigationViewItem to a ViewModel type.
    /// </summary>
    /// <param name="key">The key to translate.</param>
    /// <returns>The view model</returns>
    /// <exception cref="InvalidOperationException">Thrown if there is not tag registered.</exception>
    Type GetViewModelFromKey(string key);

    /// <summary>
    /// Given the view type, return the view model type.
    /// </summary>
    /// <param name="viewType">The view type.</param>
    /// <returns>The view model type.</returns>
    /// <exception cref="NotImplementedException">Thrown if the view type is not registered.</exception>
    Type GetViewModelFromView(Type viewType);
}
