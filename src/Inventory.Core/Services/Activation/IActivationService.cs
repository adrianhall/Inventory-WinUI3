using Microsoft.UI.Xaml;

namespace Inventory.Core.Services;

/// <summary>
/// The service description for the activation service.
/// </summary>
public interface IActivationService
{
    /// <summary>
    /// Activates the provided view with the provided arguments.
    /// </summary>
    /// <param name="view">The view to activate.</param>
    /// <param name="activationArgs">The arguments for activation.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe</param>
    Task ActivateAsync(UIElement view, object? activationArgs, CancellationToken cancellationToken = default);
}
