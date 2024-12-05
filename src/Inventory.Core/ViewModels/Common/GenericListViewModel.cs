using CommunityToolkit.Mvvm.ComponentModel;
using Inventory.Core.Models.UI;
using System.Collections.ObjectModel;

namespace Inventory.Core.ViewModels.Common;

/// <summary>
/// The view model for a list of items.
/// </summary>
/// <typeparam name="TModel">The <see cref="UIModel"/> for the data being displayed in the view.</typeparam>
public partial class GenericListViewModel<TModel> : ObservableObject where TModel : UIModel
{
    private readonly string _singular = typeof(TModel).Name.Replace("Model", string.Empty).ToLower();

    /// <summary>
    /// The items in the list.
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<TModel> _items = [];

    /// <summary>
    /// A count of the items.
    /// </summary>
    [ObservableProperty]
    private int _itemsCount = 0;

    /// <summary>
    /// The selected item in the list.
    /// </summary>
    [ObservableProperty]
    private TModel? _selectedItem = default;

    /// <summary>
    /// The title (after the prefix).
    /// </summary>
    public string Title
    {
        get => $" ({Pluralize(ItemsCount, _singular)})";
    }

    /// <summary>
    /// Makes a string that says 
    /// </summary>
    /// <param name="count"></param>
    /// <param name="singular"></param>
    /// <returns></returns>
    private static string Pluralize(int count, string singular)
    {
        return count == 1 ? $"{count} {singular}" : $"{count} {singular}s";
    }
}
