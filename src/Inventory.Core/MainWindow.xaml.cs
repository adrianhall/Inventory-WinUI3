using Microsoft.UI.Xaml;

namespace Inventory.Core;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ExtendsContentIntoTitleBar = true;
        AppWindow.SetIcon(Path.Combine(AppContext.BaseDirectory, "Assets", "WindowIcon.ico"));
        Content = null;
    }
}
