using Inventory.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Xaml.Interactivity;

namespace Inventory.Core.Behaviors;

/// <summary>
/// The possible values for the <see cref="NavigationViewHeaderBehavior.HeaderMode"/>.
/// </summary>
public enum NavigationViewHeaderMode
{
    Always,
    Never,
    Minimal
}

/// <summary>
/// A behavior that can be attached to a <see cref="NavigationView"/> to control the header visibility.
/// </summary>
public class NavigationViewHeaderBehavior : Behavior<NavigationView>
{
    private static NavigationViewHeaderBehavior? _current;
    private Page? _currentPage;

    #region DefaultHeader XAML Property
    public static readonly DependencyProperty DefaultHeaderProperty =
        DependencyProperty.Register("DefaultHeader", typeof(object), typeof(NavigationViewHeaderBehavior), new PropertyMetadata(null, UpdateHeader));

    public object DefaultHeader
    {
        get => GetValue(DefaultHeaderProperty);
        set => SetValue(DefaultHeaderProperty, value);
    }
    #endregion

    #region HeaderMode XAML Property
    public static readonly DependencyProperty HeaderModeProperty =
        DependencyProperty.RegisterAttached("HeaderMode", typeof(bool), typeof(NavigationViewHeaderBehavior), new PropertyMetadata(NavigationViewHeaderMode.Always, UpdateHeader));

    public static NavigationViewHeaderMode GetHeaderMode(Page item)
        => (NavigationViewHeaderMode)item.GetValue(HeaderModeProperty);

    public static void SetHeaderMode(Page item, NavigationViewHeaderMode value)
        => item.SetValue(HeaderModeProperty, value);
    #endregion

    #region HeaderContext XAML Property
    public static readonly DependencyProperty HeaderContextProperty =
        DependencyProperty.RegisterAttached("HeaderContext", typeof(object), typeof(NavigationViewHeaderBehavior), new PropertyMetadata(null, UpdateHeader));

    public static object GetHeaderContext(Page item)
        => item.GetValue(HeaderContextProperty);

    public static void SetHeaderContext(Page item, object value)
        => item.SetValue(HeaderContextProperty, value);
    #endregion

    #region HeaderTemplate XAML Property
    public static readonly DependencyProperty HeaderTemplateProperty =
        DependencyProperty.RegisterAttached("HeaderTemplate", typeof(DataTemplate), typeof(NavigationViewHeaderBehavior), new PropertyMetadata(null, UpdateHeader));

    public static DataTemplate GetHeaderTemplate(Page item)
        => (DataTemplate)item.GetValue(HeaderTemplateProperty);

    public static void SetHeaderTemplate(Page item, DataTemplate value)
        => item.SetValue(HeaderTemplateProperty, value);
    #endregion

    #region XAML Property Change Handlers
    private static void UpdateHeader(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => _current!.UpdateHeader();
    #endregion

    public DataTemplate? DefaultHeaderTemplate { get; set; }

    /// <inheritdoc />
    protected override void OnAttached()
    {
        base.OnAttached();
        var navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        navigationService.Navigated += OnNavigated;
        _current = this;
    }

    /// <inheritdoc />
    protected override void OnDetaching()
    {
        base.OnDetaching();
        var navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        navigationService.Navigated -= OnNavigated;
    }

    /// <summary>
    /// Event handler that updates the navigation header when navigation occurs.
    /// </summary>
    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        if (sender is Frame frame && frame.Content is Page page)
        {
            _currentPage = page;
            UpdateHeader();
        }
    }

    private void UpdateHeader()
    {
        if (_currentPage is not null)
        {
            var headerMode = GetHeaderMode(_currentPage);
            AssociatedObject.Header = (headerMode is NavigationViewHeaderMode.Never) ? null : GetHeaderContext(_currentPage) ?? DefaultHeader;
            AssociatedObject.HeaderTemplate = GetHeaderTemplate(_currentPage) ?? DefaultHeaderTemplate;
            AssociatedObject.AlwaysShowHeader = headerMode is NavigationViewHeaderMode.Always;

        }
    }
}
