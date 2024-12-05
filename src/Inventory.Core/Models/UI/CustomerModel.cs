using CommunityToolkit.Mvvm.ComponentModel;

namespace Inventory.Core.Models.UI;

public partial class CustomerModel : UIModel
{
    [ObservableProperty]
    private string? _title;

    [ObservableProperty]
    private string? _firstName;

    [ObservableProperty]
    private string? _middleName;

    [ObservableProperty]
    private string? _lastName;

    [ObservableProperty]
    private string? _suffix;

    [ObservableProperty]
    private string? _gender;

    [ObservableProperty]
    private string? _emailAddress;

    [ObservableProperty]
    private string? _addressLine1;

    [ObservableProperty]
    private string? _addressLine2;

    [ObservableProperty]
    private string? _city;

    [ObservableProperty]
    private string? _region;

    [ObservableProperty]
    private string? _countryCode;

    [ObservableProperty]
    private string? _postalCode;

    [ObservableProperty]
    private string? _phone;

    [ObservableProperty]
    private DateOnly? _birthDate;

    [ObservableProperty]
    private string? _occupation;

    [ObservableProperty]
    private decimal? _yearlyIncome;

    [ObservableProperty]
    private byte[]? _picture;

    [ObservableProperty]
    private byte[]? _thumbnail;
}
