using CareOnDemand.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace CareOnDemand.ViewModels.CustomerViewModels
{
    public class ViewActivePastOrderViewModel : BaseCustomerOrderHistoryViewModel
    {
        public ViewActivePastOrderViewModel()
        {
            Task.Run(async () => await GetOrderDetailsFromDb(customer_selected_order));
            OrderServicesList = new ObservableCollection<Order_Service>();
            ElementVisible = false;
            ActivityIndicatorVisible = true;
            ActivityIndicatorRunning = true;
        }
    }
}
