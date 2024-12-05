using Inventory.Core.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace Inventory.Core.Views;

public sealed partial class DashboardView : Page
{
    public DashboardView()
    {
        InitializeComponent();
        ViewModel = App.Current.Services.GetRequiredService<DashboardViewModel>();
    }

    public DashboardViewModel ViewModel { get; }
}
