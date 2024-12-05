namespace Inventory.Core.Services;

/// <summary>
/// The abstraction of an activation handler.  This is used during
/// startup to initialize the application.
/// </summary>
public interface IActivationHandler
{
    /// <summary>
    /// Checks that the current handler can be activated using the arguments provided.
    /// </summary>
    /// <param name="args">The arguments for activation.</param>
    /// <returns><c>true</c> if this activation handler can process the activation.</returns>
    bool CanHandle(object? args);

    /// <summary>
    /// Tries to activate using the current handler.
    /// </summary>
    /// <param name="args">The arguments for activation.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe</param>
    /// <returns>A task that resolves when the operation is complete.</returns>
    Task HandleAsync(object? args, CancellationToken cancellationToken = default);
}

/// <summary>
/// The parent class for the activation handlers, using a typed parameter instead of object.
/// </summary>
/// <typeparam name="TParameter">The type of the arguments used for activation.</typeparam>
public abstract class ActivationHandler<TParameter> : IActivationHandler where TParameter : class
{
    /// <inheritdoc />
    public bool CanHandle(object? args)
        => args is TParameter && TypedCanHandle(args as TParameter);

    /// <inheritdoc />
    public Task HandleAsync(object? args, CancellationToken cancellationToken = default)
        => TypedHandleAsync(args as TParameter, cancellationToken);

    /// <summary>
    /// Determines if this handler can handle the activation event.  This is a typed version
    /// of the <see cref="IActivationHandler.CanHandle(object)"/> method.
    /// </summary>
    /// <param name="args">The arguments for the activation event.</param>
    /// <returns><c>true</c> if this activation handler can process the activation.</returns>
    protected virtual bool TypedCanHandle(TParameter? args)
        => true;

    /// <summary>
    /// Tries to activate using the current handler.  This is a typed version of the 
    /// <see cref="IActivationHandler.HandleAsync(object, CancellationToken)"/> method.
    /// </summary>
    /// <param name="args">The arguments for activation.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe</param>
    /// <returns>A task that resolves when the operation is complete.</returns>
    protected abstract Task TypedHandleAsync(TParameter? args, CancellationToken cancellationToken = default);
}
