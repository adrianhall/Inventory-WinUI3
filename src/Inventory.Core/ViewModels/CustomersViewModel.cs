using CommunityToolkit.Mvvm.ComponentModel;
using Inventory.Core.Services;
using Microsoft.Extensions.Logging;

namespace Inventory.Core.ViewModels;

public partial class CustomersViewModel(CustomerListViewModel customerListViewModel, ILogger<CustomersViewModel> logger) 
    : ObservableObject, INavigationLifecycle
{
    /// <summary>
    /// The view model for the customer list.
    /// </summary>
    public CustomerListViewModel CustomerList { get; set; } = customerListViewModel;

    /// <summary>
    /// The display title for the top of the page.
    /// </summary>
    public string Title
    {
        get => $"Customers {CustomerList.Title}";
    }

    [ObservableProperty]
    private int _rowSpan = 1;

    #region INavigationLifecycle Implementation
    public void OnNavigatedFrom()
    {
        logger.LogTrace("OnNavigatedFrom");
    }

    public void OnNavigatedTo()
    {
        logger.LogTrace("OnNavigatedTo");

        // Load the customer list data.
        CustomerList.LoadDataCommand.Execute(null);
    }
    #endregion
}
