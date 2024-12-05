using Inventory.Core.Models.Data;

namespace Inventory.Core.Services;

public interface IVirtualCollection<TModel> where TModel : DTOModel
{
    /// <summary>
    /// Retrieves a single item by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the DTO.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe.</param>
    /// <returns>The model, or null if it doesn't exist.</returns>
    Task<TModel?> GetItemByIdAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all the items in the collection.
    /// </summary>
    /// <returns>The list of items, returned as an <see cref="IAsyncEnumerable{T}"/> feed.</returns>
    IAsyncEnumerable<TModel> GetAsyncItems();
}
