using Inventory.Core.Models.Data;
using Inventory.Core.Models.UI;

namespace Inventory.Core.Services;

public class MappingService : IMappingService
{
    /// <summary>
    /// Converts the UI customer model to the DTO model.
    /// </summary>
    /// <param name="customerModel">The model to convert.</param>
    /// <returns>The converted model.</returns>
    public Customer ToDTO(CustomerModel customerModel) => new Customer()
    {
        Id = customerModel.Id,
        CreatedAt = customerModel.CreatedAt,
        UpdatedAt = customerModel.UpdatedAt,

        Title = customerModel.Title,
        FirstName = customerModel.FirstName,
        MiddleName = customerModel.MiddleName,
        LastName = customerModel.LastName,
        Suffix = customerModel.Suffix,
        Gender = customerModel.Gender,
        EmailAddress = customerModel.EmailAddress,
        AddressLine1 = customerModel.AddressLine1,
        AddressLine2 = customerModel.AddressLine2,
        City = customerModel.City,
        Region = customerModel.Region,
        CountryCode = customerModel.CountryCode,
        PostalCode = customerModel.PostalCode,
        Phone = customerModel.Phone,
        BirthDate = customerModel.BirthDate,
        Occupation = customerModel.Occupation,
        YearlyIncome = customerModel.YearlyIncome,
        Picture = customerModel.Picture,
        Thumbnail = customerModel.Thumbnail
    };


    /// <summary>
    /// Converts the DTO customer model to the UI model.
    /// </summary>
    /// <param name="customer">The model to convert.</param>
    /// <returns>The converted model.</returns>
    public CustomerModel ToUIModel(Customer customer) => new()
    {
        Id = customer.Id,
        CreatedAt = customer.CreatedAt,
        UpdatedAt = customer.UpdatedAt,

        Title = customer.Title,
        FirstName = customer.FirstName,
        MiddleName = customer.MiddleName,
        LastName = customer.LastName,
        Suffix = customer.Suffix,
        Gender = customer.Gender,
        EmailAddress = customer.EmailAddress,
        AddressLine1 = customer.AddressLine1,
        AddressLine2 = customer.AddressLine2,
        City = customer.City,
        Region = customer.Region,
        CountryCode = customer.CountryCode,
        PostalCode = customer.PostalCode,
        Phone = customer.Phone,
        BirthDate = customer.BirthDate,
        Occupation = customer.Occupation,
        YearlyIncome = customer.YearlyIncome,
        Picture = customer.Picture,
        Thumbnail = customer.Thumbnail
    };
}
