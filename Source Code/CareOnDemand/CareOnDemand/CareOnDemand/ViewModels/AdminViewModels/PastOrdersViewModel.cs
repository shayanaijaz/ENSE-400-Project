using System;
using System.Collections.Generic;
using System.Text;

namespace CareOnDemand.ViewModels.AdminViewModels
{
    public class PastOrdersViewModel : BaseAdminOrdersViewModel
    {
        public PastOrdersViewModel()
        {
            PastOrders = new List<OrdersList>();
            ActivityIndicatorVisible = true;
            ActivityIndicatorRunning = true;
            GetPastOrders();
        }

        public bool ActivityIndicatorVisible { get; set; }
        public bool ActivityIndicatorRunning { get; set; }

        public List<OrdersList> PastOrders { get; set; }
        async void GetPastOrders()
        {
            string[] pastOrderStatusArray = { "Cancelled", "Completed" };

            PastOrders = await GetOrdersFromDb(pastOrderStatusArray);

            ActivityIndicatorRunning = false;
            ActivityIndicatorVisible = false;
            OnPropertyChanged(nameof(ActivityIndicatorRunning));
            OnPropertyChanged(nameof(ActivityIndicatorVisible));
            OnPropertyChanged(nameof(PastOrders));
        }
    }
}
