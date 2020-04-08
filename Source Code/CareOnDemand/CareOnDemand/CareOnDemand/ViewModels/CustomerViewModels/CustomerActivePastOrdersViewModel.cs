using CareOnDemand.Data;
using CareOnDemand.Models;
using CareOnDemand.Views.CustomerViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CareOnDemand.ViewModels.CustomerViewModels
{
    public class CustomerActivePastOrdersViewModel : BaseCustomerOrderHistoryViewModel
    {
        public CustomerActivePastOrdersViewModel()
        {
            ActiveOrders = new List<OrdersList>();
            PastOrders = new List<OrdersList>();

            ActivityIndicatorVisible = true;
            ActivityIndicatorRunning = true;
            ElementVisible = false;

            string[] activeOrderStatusArray = { "New", "In Progress", "On The Way", "Waiting" };
            string[] pastOrderStatusArray = { "Cancelled", "Completed" };

            Task.Run(async () =>
            {
                ActiveOrders = await GetOrdersFromDb(activeOrderStatusArray);
                PastOrders = await GetOrdersFromDb(pastOrderStatusArray);


                ActivityIndicatorVisible = false;
                ActivityIndicatorRunning = false;
                ElementVisible = true;

                OnPropertyChanged(nameof(ActiveOrders));
                OnPropertyChanged(nameof(PastOrders));
                OnPropertyChanged(nameof(ActivityIndicatorRunning));
                OnPropertyChanged(nameof(ActivityIndicatorVisible));
                OnPropertyChanged(nameof(ElementVisible));


            });
        }

        public List<OrdersList> ActiveOrders { get; set; }
        public List<OrdersList> PastOrders { get; set; }


        private OrdersList selectedOrder;
        public OrdersList SelectedOrder
        {
            get => selectedOrder;
            set
            {
                selectedOrder = value;

                if (selectedOrder == null)
                    return;
               
                customer_selected_order = selectedOrder.CustomerOrder;

                OrderSelected();
            }
        }

        async void OrderSelected()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ViewActivePastOrder());
        }

        
    }

    public class OrdersList
    {
        public string CustomerName { get; set; }
        public string ServicesOrderedString { get; set; }
        public Order CustomerOrder { get; set; }

    }
}
