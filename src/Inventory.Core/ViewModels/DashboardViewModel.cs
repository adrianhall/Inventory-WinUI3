using CommunityToolkit.Mvvm.ComponentModel;

namespace Inventory.Core.ViewModels;

public partial class DashboardViewModel : ObservableObject
{
    [ObservableProperty]
    private string myText = "dashboard view model";
}
