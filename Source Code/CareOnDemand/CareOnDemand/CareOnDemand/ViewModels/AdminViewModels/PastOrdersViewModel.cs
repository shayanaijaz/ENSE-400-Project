/*
    Care on Demand Application
    Capstone 2020 - ENSE 400/477
    The Ni(c)(k)S

    Author: Shayan Khan
    Last Modified: Apr. 10, 2020
*/
using CareOnDemand.Views.AdminViews;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CareOnDemand.ViewModels.AdminViewModels
{
    /* This class contains bindings and functions relating to elements on the PastOrders page. It inherits variables, objects and 
     * functions from the BaseAdminOrdersViewModel class.
     */
    public class PastOrdersViewModel : BaseAdminOrdersViewModel
    {
        // Constructor that initializes the bindings and runs commands to populate the bindings. 
        public PastOrdersViewModel()
        {
            Orders = new List<OrdersList>();
            ActivityIndicatorVisible = true;
            ActivityIndicatorRunning = true;

            // Array of order statuses that need to be retrieved
            string[] pastOrderStatusArray = { "Cancelled", "Completed" };

            Task.Run(async () => await GetOrders(pastOrderStatusArray));

            RefreshCommand = new Command(async () => await ManualRefreshOrderList(pastOrderStatusArray));

            // This command starts a timer of 5 minutes and runs the refresh function every 5 minutes. 
            Device.StartTimer(TimeSpan.FromMinutes(5), () => AutoRefreshOrderList(pastOrderStatusArray));

        }

        // Bindings on this page
        private OrdersList selectedOrder;
        public OrdersList SelectedOrder
        {
            get => selectedOrder;
            set
            {
                selectedOrder = value;

                if (selectedOrder == null)
                    return;

                admin_selected_order = selectedOrder.CustomerOrder;

                OrderSelected();
            }
        }

        async void OrderSelected()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ViewActivePastOrders());
        }
    }
}
