using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Inventory.Core.Services;

/// <summary>
/// The concrete implementation of the <see cref="IActivationService"/>.  This is used
/// during startup to activate the first view.
/// </summary>
public class ActivationService(
    ActivationHandler<LaunchActivatedEventArgs> defaultHandler,
    IEnumerable<IActivationHandler> activationHandlers,
    Window window,
    ILogger<ActivationService> logger
    ) : IActivationService
{
    #region IActivationService Implementation
    /// <summary>
    /// Activates the provided view with the provided arguments.
    /// </summary>
    /// <param name="view">The view to activate.</param>
    /// <param name="activationArgs">The arguments for activation.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe.</param>
    public async Task ActivateAsync(UIElement view, object? activationArgs, CancellationToken cancellationToken = default)
    {
        using var scope = logger.BeginScope("ActivationService: {ActivationArgs}", activationArgs);

        // Execute anything that needs to be done before activation.
        logger.LogTrace("Initializing application");
        await InitializeApplicationAsync(cancellationToken);

        // Set up the MainWindow content
        logger.LogTrace("Setting up main window content");
        window.Content ??= view ?? new Frame();

        // Handle activation via the activation handlers.
        logger.LogTrace("Handling activation");
        foreach (var handler in activationHandlers.Where(handler => handler.CanHandle(activationArgs)))
        {
            logger.LogTrace("Handling activation via {Handler}", handler.GetType().Name);
            await handler.HandleAsync(activationArgs, cancellationToken);
        }

        // Use the default activation handler.
        if (defaultHandler.CanHandle(activationArgs))
        {
            logger.LogTrace("Handling activation via {Handler}", defaultHandler.GetType().Name);
            await defaultHandler.HandleAsync(activationArgs, cancellationToken);
        }

        // Activate the window
        logger.LogTrace("Activating main window");
        window.Activate();

        // Execute tasks after activation.
        logger.LogTrace("Starting application post-activation");
        await StartupApplicationAsync(cancellationToken);
    }
    #endregion

    /// <summary>
    /// Initializes the application pre-window activation.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe.</param>
    /// <returns>A task that resolves when the operation is complete.</returns>
    private Task InitializeApplicationAsync(CancellationToken cancellationToken = default)
    {
        // Initialize the application here
        return Task.CompletedTask;
    }

    private Task StartupApplicationAsync(CancellationToken cancellationToken = default)
    {
        // Start the application here
        return Task.CompletedTask;
    }
}
