using Inventory.Core.Services;
using Inventory.Core.ViewModels;
using Inventory.Core.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using Microsoft.UI.Xaml;

namespace Inventory.Core;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : Application
{
    private readonly IHost _host;

    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        InitializeComponent();

        _host = new HostBuilder()
            .UseContentRoot(AppContext.BaseDirectory)
            .ConfigureAppConfiguration((ctx, builder) =>
            {
                builder.AddJsonFile("appsettings.json", optional: true);
                builder.AddJsonFile($"appsettings.{ctx.HostingEnvironment.EnvironmentName}.json", optional: true);
                builder.AddEnvironmentVariables();
            })
            .ConfigureLogging((ctx, builder) =>
            {
                builder.AddConsole();
                if (ctx.HostingEnvironment.IsDevelopment())
                {
                    builder.AddDebug();
                }
                else
                {
                    builder.AddEventLog(new EventLogSettings { SourceName = "Inventory", LogName = "Application" });
                }
            })
            .ConfigureServices((ctx, services) =>
            {
                // Activation
                services.AddSingleton<IActivationService, ActivationService>();

                // Navigation
                services.AddSingleton<IViewMapper, ViewMapper>();
                services.AddSingleton<INavigationService, NavigationService>();
                services.AddSingleton<INavigationViewService, NavigationViewService>();
                services.AddSingleton<Window, MainWindow>();
                services.AddSingleton<ShellView>();

                // Services

                // View Models
                services.AddTransient<ShellViewModel>();
                services.AddTransient<DashboardViewModel>();
                services.AddTransient<CustomersViewModel>();
                services.AddTransient<OrdersViewModel>();
                services.AddTransient<ProductsViewModel>();
            })
            .Build();

        UnhandledException += UnhandledExceptionHandler;
    }

    /// <summary>
    /// Gets the current instance of the <see cref="App"/> class.
    /// </summary>
    public static new App Current => (App)Application.Current;

    /// <summary>
    /// Gets the services provided by the host.
    /// </summary>
    public IServiceProvider Services { get => _host.Services; }

    /// <summary>
    /// Invoked when the application is launched.
    /// </summary>
    /// <param name="args">Details about the launch request and process.</param>
    protected override async void OnLaunched(LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);
        ShellView view = Services.GetRequiredService<ShellView>();
        IActivationService activationService = Services.GetRequiredService<IActivationService>();
        await activationService.ActivateAsync(view, args);
    }

    /// <summary>
    /// Event handler that is called as a last resort when an unhandled exception occurs.
    /// </summary>
    private void UnhandledExceptionHandler(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
    {
        var logger = Services.GetRequiredService<ILogger<App>>();
        logger.LogError(e.Exception, "An unhandled exception occurred in {sender}", sender.GetType().FullName);
    }
}
