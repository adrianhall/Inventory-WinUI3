using CommunityToolkit.Mvvm.ComponentModel;

namespace Inventory.Core.ViewModels;

public partial class CustomersViewModel : ObservableObject
{
    [ObservableProperty]
    private string myText = "customers view model";
}
