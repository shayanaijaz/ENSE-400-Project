/*
    Care on Demand Application
    Capstone 2020 - ENSE 400/477
    The Ni(c)(k)S

    Author: Shayan Khan
    Last Modified: Apr. 10, 2020
*/
using CareOnDemand.Views.CarePartnerViews;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CareOnDemand.ViewModels.CarePartnerViewModels
{
    /* This class contains bindings and functions relating to elements on the AssignedOrders page. It inherits variables, objects and 
     * functions from the BaseCarePartnerOrdersViewModel class.
     */
    public class AssignedOrdersViewModel : BaseCarePartnerOrdersViewModel
    {
        // Constructor that initializes the bindings and runs commands to populate the bindings. 
        public AssignedOrdersViewModel()
        {
            Orders = new List<OrdersList>();
            ActivityIndicatorVisible = true;
            ActivityIndicatorRunning = true;

            // Array of order statuses that need to be retrieved
            string[] assignedOrderStatusArray = { "In Progress", "On The Way", "Waiting" };

            Task.Run(async () => await GetOrders(assignedOrderStatusArray));

            RefreshCommand = new Command(async () => await ManualRefreshOrderList(assignedOrderStatusArray));

            // This command starts a timer of 5 minutes and runs the refresh function every 5 minutes. 
            Device.StartTimer(TimeSpan.FromMinutes(5), () => AutoRefreshOrderList(assignedOrderStatusArray));

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

                care_partner_selected_order = selectedOrder.CustomerOrder;

                OrderSelected();
            }
        }

        // Redirect to ViewAssignedOrder page
        async void OrderSelected()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ViewAssignedOrder());
        }

    }
}
