using CommunityToolkit.Mvvm.ComponentModel;

namespace Inventory.Core.Models.UI;

public abstract partial class UIModel : ObservableObject
{
    [ObservableProperty]
    private string _id = string.Empty;

    [ObservableProperty]
    private DateTimeOffset _createdAt = DateTimeOffset.UtcNow;

    [ObservableProperty]
    private DateTimeOffset _updatedAt = DateTimeOffset.UtcNow;
}


