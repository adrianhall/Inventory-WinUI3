using Inventory.Core.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Inventory.Core.Views;

public sealed partial class CustomersList : UserControl
{
    public CustomersList()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty ViewModelProperty
        = DependencyProperty.Register(nameof(ViewModel), typeof(CustomerListViewModel), typeof(CustomersList), new PropertyMetadata(null));

    public CustomerListViewModel ViewModel
    {
        get => (CustomerListViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }
}
