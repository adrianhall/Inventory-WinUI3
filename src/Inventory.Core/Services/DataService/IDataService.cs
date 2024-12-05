using Inventory.Core.Models.Data;

namespace Inventory.Core.Services;

public interface IDataService
{
    /// <summary>
    /// The virtual collection for the customers.
    /// </summary>
    IVirtualCollection<Customer> Customers { get; }
}
