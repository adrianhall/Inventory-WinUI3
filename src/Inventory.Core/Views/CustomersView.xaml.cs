using Inventory.Core.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace Inventory.Core.Views;

public sealed partial class CustomersView : Page
{
    public CustomersView()
    {
        InitializeComponent();
        ViewModel = App.Current.Services.GetRequiredService<CustomersViewModel>();
    }

    public CustomersViewModel ViewModel { get; }
}
