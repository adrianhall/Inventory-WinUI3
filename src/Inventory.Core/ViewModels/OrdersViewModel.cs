using CommunityToolkit.Mvvm.ComponentModel;

namespace Inventory.Core.ViewModels;

public partial class OrdersViewModel : ObservableObject
{
    [ObservableProperty]
    private string myText = "orders view model";
}
