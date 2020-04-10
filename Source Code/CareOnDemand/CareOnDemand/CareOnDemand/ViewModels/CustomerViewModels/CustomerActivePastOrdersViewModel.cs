/*
    Care on Demand Application
    Capstone 2020 - ENSE 400/477
    The Ni(c)(k)S

    Author: Shayan Khan
    Last Modified: Apr. 10, 2020
*/
using CareOnDemand.Models;
using CareOnDemand.Views.CustomerViews;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CareOnDemand.ViewModels.CustomerViewModels
{
    /* This class contains bindings and functions relating to elements on the CustomerActivePastOrders page. It inherits variables, objects and 
     * functions from the BaseCustomerOrderHistoryViewModel class.
     */
    public class CustomerActivePastOrdersViewModel : BaseCustomerOrderHistoryViewModel
    {
        // Constructor that initializes the bindings and runs commands to populate the bindings. 
        public CustomerActivePastOrdersViewModel()
        {
            ActiveOrders = new List<OrdersList>();
            PastOrders = new List<OrdersList>();

            ActivityIndicatorVisible = true;
            ActivityIndicatorRunning = true;
            ElementVisible = false;

            // Array of order statuses that need to be retrieved
            string[] activeOrderStatusArray = { "New", "In Progress", "On The Way", "Waiting" };
            string[] pastOrderStatusArray = { "Cancelled", "Completed" };

            Task.Run(async () =>
            {
                ActiveOrders = await GetOrdersFromDb(activeOrderStatusArray);
                PastOrders = await GetOrdersFromDb(pastOrderStatusArray);


                ActivityIndicatorVisible = false;
                ActivityIndicatorRunning = false;
                ElementVisible = true;

                // Update the elements
                OnPropertyChanged(nameof(ActiveOrders));
                OnPropertyChanged(nameof(PastOrders));
                OnPropertyChanged(nameof(ActivityIndicatorRunning));
                OnPropertyChanged(nameof(ActivityIndicatorVisible));
                OnPropertyChanged(nameof(ElementVisible));


            });
        }

        // Bindings on this page
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

        // Redirect ot ViewActivePastOrder page 
        async void OrderSelected()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ViewActivePastOrder());
        }

        
    }

    /* This class is a model for what information an item displayed on the page should contain. 
     */ 
    public class OrdersList
    {
        public string CustomerName { get; set; }
        public string ServicesOrderedString { get; set; }
        public Order CustomerOrder { get; set; }

    }
}
