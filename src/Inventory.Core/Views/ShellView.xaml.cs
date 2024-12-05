using Inventory.Core.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Inventory.Core.Views;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class ShellView : Page
{
    public ShellView(ShellViewModel viewModel, Window mainWindow)
    {
        ViewModel = viewModel;

        // TODO: Can we initialize the menu items from the view mapper?
        // var menuItems = viewMapper.GetMenuItems();
        // foreach (var item in menuItems)
        // {
        //     NavigationView.MenuItems.Add(new NavigationViewItem...);
        // }

        InitializeComponent();
        
        ViewModel.InitializeView(NavigationFrame, NavigationViewControl);
        mainWindow.SetTitleBar(AppTitleBar);
    }

    /// <summary>
    /// The view model for this component.
    /// </summary>
    public ShellViewModel ViewModel { get; }

    /// <summary>
    /// Event handler called when the display mode changes on the navigation view control.
    /// </summary>
    private void NavigationViewControl_DisplayModeChanged(NavigationView sender, NavigationViewDisplayModeChangedEventArgs args)
    {
        AppTitleBar.Margin = new Thickness()
        {
            Left = sender.CompactPaneLength * (sender.DisplayMode == NavigationViewDisplayMode.Minimal ? 2 : 1),
            Top = AppTitleBar.Margin.Top,
            Right = AppTitleBar.Margin.Right,
            Bottom = AppTitleBar.Margin.Bottom
        };
    }
}
