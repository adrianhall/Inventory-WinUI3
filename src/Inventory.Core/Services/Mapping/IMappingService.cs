using Inventory.Core.Models.Data;
using Inventory.Core.Models.UI;

namespace Inventory.Core.Services;

/// <summary>
/// The mapping service is responsible for converting between the data and UI models.
/// </summary>
public interface IMappingService
{
    /// <summary>
    /// Converts the UI customer model to the DTO model.
    /// </summary>
    /// <param name="customerModel">The model to convert.</param>
    /// <returns>The converted model.</returns>
    public Customer ToDTO(CustomerModel customerModel);

    /// <summary>
    /// Converts the DTO customer model to the UI model.
    /// </summary>
    /// <param name="customer">The model to convert.</param>
    /// <returns>The converted model.</returns>
    public CustomerModel ToUIModel(Customer customer);
}
