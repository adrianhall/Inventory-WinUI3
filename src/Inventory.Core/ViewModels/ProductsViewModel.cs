using CommunityToolkit.Mvvm.ComponentModel;

namespace Inventory.Core.ViewModels;

public partial class ProductsViewModel : ObservableObject
{
    [ObservableProperty]
    private string myText = "products view model";
}
