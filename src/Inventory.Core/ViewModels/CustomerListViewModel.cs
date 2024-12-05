using CommunityToolkit.Mvvm.Input;
using Inventory.Core.Models.Data;
using Inventory.Core.Models.UI;
using Inventory.Core.Services;
using Inventory.Core.ViewModels.Common;
using Microsoft.Extensions.Logging;

namespace Inventory.Core.ViewModels;

public partial class CustomerListViewModel(IDataService dataService, IMappingService mapper, ILogger<CustomerListViewModel> logger) 
    : GenericListViewModel<CustomerModel>
{
    [RelayCommand]
    private async Task LoadDataAsync()
    {
        logger.LogTrace("LoadDataAsync");
        await foreach (Customer item in dataService.Customers.GetAsyncItems())
        {
            Items.Add(mapper.ToUIModel(item));
            ItemsCount = Items.Count;
        }
    }
}
