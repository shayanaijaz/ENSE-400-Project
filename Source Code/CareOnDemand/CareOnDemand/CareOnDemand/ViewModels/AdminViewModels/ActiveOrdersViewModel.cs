using System;
using System.Collections.Generic;
using System.Text;

namespace CareOnDemand.ViewModels.AdminViewModels
{
    public class ActiveOrdersViewModel : BaseAdminOrdersViewModel
    {
        public ActiveOrdersViewModel()
        {
            ActiveOrders = new List<OrdersList>();
            ActivityIndicatorVisible = true;
            ActivityIndicatorRunning = true;
            getNewOrder();
        }

        public bool ActivityIndicatorVisible { get; set; }
        public bool ActivityIndicatorRunning { get; set; }

        public List<OrdersList> ActiveOrders { get; set; }
        async void getNewOrder()
        {
            string[] activeOrderStatusArray = { "In Progress", "On The Way", "Waiting" };
            ActiveOrders = await GetOrdersFromDb(activeOrderStatusArray);

            ActivityIndicatorRunning = false;
            ActivityIndicatorVisible = false;
            OnPropertyChanged(nameof(ActivityIndicatorRunning));
            OnPropertyChanged(nameof(ActivityIndicatorVisible));
            OnPropertyChanged(nameof(ActiveOrders));
        }
    }
}
