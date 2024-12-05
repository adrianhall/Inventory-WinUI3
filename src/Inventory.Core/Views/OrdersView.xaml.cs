using Inventory.Core.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace Inventory.Core.Views;

public sealed partial class OrdersView : Page
{
    public OrdersView()
    {
        InitializeComponent();
        ViewModel = App.Current.Services.GetRequiredService<OrdersViewModel>();
    }

    public OrdersViewModel ViewModel { get; }
}