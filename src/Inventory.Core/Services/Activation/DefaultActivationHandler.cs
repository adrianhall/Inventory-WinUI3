using Inventory.Core.ViewModels;
using Microsoft.UI.Xaml;

namespace Inventory.Core.Services;

/// <summary>
/// The <see cref="ActivationHandler{TParameter}"/> for the launch event.  This is
/// launched from within the <see cref="App.OnLaunched(LaunchActivatedEventArgs)"/> method.
/// </summary>
public class DefaultActivationHandler(INavigationService navigationService) : ActivationHandler<LaunchActivatedEventArgs>
{
    /// <inheritdoc />
    protected override bool TypedCanHandle(LaunchActivatedEventArgs? args)
        => navigationService.NavigationFrame?.Content is not null;

    /// <inheritdoc />
    protected override Task TypedHandleAsync(LaunchActivatedEventArgs? args, CancellationToken cancellationToken = default)
    {
        navigationService.Navigate<DashboardViewModel>(args?.Arguments);
        return Task.CompletedTask;
    }
}
