using Bogus;
using Inventory.Core.Models.Data;
using System.Collections.Concurrent;

namespace Inventory.Core.Services;

/// <summary>
/// A fake implementation of the data service.
/// </summary>
public class FakeDataService : IDataService
{
    /// <summary>
    /// The customer list.
    /// </summary>
    public IVirtualCollection<Customer> Customers => new FakeCustomerCollection();
}

/// <summary>
/// A fake virtual collection of customers.
/// </summary>
public class FakeCustomerCollection : IVirtualCollection<Customer>
{
    private readonly ConcurrentDictionary<string, Customer> _customers = [];

    public FakeCustomerCollection()
    {
        var fakerForCustomers = new Faker<Customer>()
            .RuleFor(c => c.Id, f => f.Random.Guid().ToString())
            .RuleFor(c => c.Suffix, f => f.Name.Suffix())
            .RuleFor(c => c.FirstName, f => f.Name.FirstName())
            .RuleFor(c => c.MiddleName, f => f.Name.FirstName())
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.AddressLine1, f => f.Address.StreetAddress())
            .RuleFor(c => c.City, f => f.Address.City())
            .RuleFor(c => c.Region, f => f.Address.State())
            .RuleFor(c => c.CountryCode, f => f.Address.CountryCode())
            .RuleFor(c => c.PostalCode, f => f.Address.ZipCode())
            .RuleFor(c => c.EmailAddress, f => f.Internet.Email())
            .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber());

        for (int i = 0; i < 500; i++)
        {
            var customer = fakerForCustomers.Generate();
            _customers.TryAdd(customer.Id, customer);
        }
    }

    #region IVirtualCollection<Customer> Implementation
    public IAsyncEnumerable<Customer> GetAsyncItems()
        => _customers.Values.ToAsyncEnumerable();

    public Task<Customer?> GetItemByIdAsync(string id, CancellationToken cancellationToken = default)
        => _customers.TryGetValue(id, out Customer? customer) ? Task.FromResult<Customer?>(customer) : Task.FromResult<Customer?>(null);
    #endregion
}
