using System.ComponentModel.DataAnnotations;

namespace Inventory.Core.Models.Data;

/// <summary>
/// A data transfer object for a customer record.
/// </summary>
public class Customer : DTOModel
{
    [MaxLength(8)]
    public string? Title { get; set; }

    [Required, MaxLength(50)]
    public string? FirstName { get; set; }

    [MaxLength(50)]
    public string? MiddleName { get; set; }

    [Required, MaxLength(50)]
    public string? LastName { get; set; }

    [MaxLength(10)]
    public string? Suffix { get; set; }

    [MaxLength(1)]
    public string? Gender { get; set; }

    [Required, MaxLength(50)]
    public string? EmailAddress { get; set; }

    [Required, MaxLength(120)]
    public string? AddressLine1 { get; set; }

    [MaxLength(120)]
    public string? AddressLine2 { get; set; }

    [Required, MaxLength(30)]
    public string? City { get; set; }

    [Required, MaxLength(50)]
    public string? Region { get; set; }

    [Required, MaxLength(2)]
    public string? CountryCode { get; set; }

    [Required, MaxLength(15)]
    public string? PostalCode { get; set; }

    [MaxLength(20)]
    public string? Phone { get; set; }

    public DateOnly? BirthDate { get; set; }

    [MaxLength(100)]
    public string? Occupation { get; set; }

    public decimal? YearlyIncome { get; set; }

    public byte[]? Picture { get; set; }

    public byte[]? Thumbnail { get; set; }
}
