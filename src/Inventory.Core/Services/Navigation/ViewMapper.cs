using Inventory.Core.ViewModels;
using Inventory.Core.Views;

namespace Inventory.Core.Services;

/// <summary>
/// This is a lookup table to map between views and view models.
/// </summary>
public class ViewMapper : IViewMapper
{
    private static readonly List<ViewMapItem> viewmap = [
        new ViewMapItem { View = typeof(DashboardView), ViewModel = typeof(DashboardViewModel) },
        new ViewMapItem { View = typeof(CustomersView), ViewModel = typeof(CustomersViewModel) },
        new ViewMapItem { View = typeof(OrdersView),    ViewModel = typeof(OrdersViewModel)    },
        new ViewMapItem { View = typeof(ProductsView),  ViewModel = typeof(ProductsViewModel)  }
    ];

    /// <summary>
    /// Given the view model type, return the view type.
    /// </summary>
    /// <param name="viewModelType">The view model type.</param>
    /// <returns>The view type.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the view model type is not registered.</exception>
    public Type GetViewFromViewModel(Type viewModelType)
        => viewmap.FirstOrDefault(x => x.ViewModel == viewModelType)?.View
        ?? throw new InvalidOperationException($"ViewModel '{viewModelType.Name}' is not registered in the ViewMapper");

    /// <summary>
    /// Gets the ViewModel type based on the key, which is usedf
    /// to translate the Tag on the NavigationViewItem to a ViewModel type.
    /// </summary>
    /// <param name="key">The key to translate.</param>
    /// <returns>The view model</returns>
    /// <exception cref="InvalidOperationException">Thrown if there is not tag registered.</exception>
    public Type GetViewModelFromKey(string key)
        => viewmap.FirstOrDefault(x => TypeNameMatches(x.ViewModel, key))?.ViewModel
        ?? throw new InvalidOperationException($"Key '{key}' is not registered in the ViewMapper");

    /// <summary>
    /// Given the view type, return the view model type.
    /// </summary>
    /// <param name="viewType">The view type.</param>
    /// <returns>The view model type.</returns>
    /// <exception cref="NotImplementedException">Thrown if the view type is not registered.</exception>
    public Type GetViewModelFromView(Type viewType)
        => viewmap.FirstOrDefault(x => x.View == viewType)?.ViewModel
        ?? throw new NotImplementedException($"View '{viewType.Name}' is not registered in the ViewMapper");

    /// <summary>
    /// Helper method to compare a provided key against the type name.
    /// </summary>
    /// <param name="type">The type to use.</param>
    /// <param name="key">The key for comparison.</param>
    /// <returns>true if the name matches; false otherwise.</returns>
    private static bool TypeNameMatches(Type type, string key)
        => type.Name?.Equals(key, StringComparison.OrdinalIgnoreCase) == true || type.FullName?.Equals(key, StringComparison.OrdinalIgnoreCase) == true;

    /// <summary>
    /// A class to hold the data necessary for the view map.
    /// </summary>
    public class ViewMapItem
    {
        public required Type View { get; set; }
        public required Type ViewModel { get; set; }
    }
}
