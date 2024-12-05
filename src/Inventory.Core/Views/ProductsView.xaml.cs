using Inventory.Core.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace Inventory.Core.Views;

public sealed partial class ProductsView : Page
{
    public ProductsView()
    {
        InitializeComponent();
        ViewModel = App.Current.Services.GetRequiredService<ProductsViewModel>();
    }

    public ProductsViewModel ViewModel { get; }
}