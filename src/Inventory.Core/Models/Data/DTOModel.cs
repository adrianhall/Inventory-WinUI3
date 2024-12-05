using System.ComponentModel.DataAnnotations;

namespace Inventory.Core.Models.Data;

/// <summary>
/// The base class for all the DTO models.
/// </summary>
public abstract class DTOModel
{
    /// <summary>
    /// A globally unique identifier for the model.
    /// </summary>
    [Key]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The date and time that the model was created.
    /// </summary>
    public DateTimeOffset CreatedAt = DateTimeOffset.UtcNow;

    /// <summary>
    /// The date and time that the model was last updated.
    /// </summary>
    public DateTimeOffset UpdatedAt = DateTimeOffset.UtcNow;
}
