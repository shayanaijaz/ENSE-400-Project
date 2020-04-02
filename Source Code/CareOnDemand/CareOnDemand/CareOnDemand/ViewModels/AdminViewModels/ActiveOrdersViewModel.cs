using CareOnDemand.Models;
using CareOnDemand.Views.AdminViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace CareOnDemand.ViewModels.AdminViewModels
{
    public class ActiveOrdersViewModel : BaseAdminOrdersViewModel
    {
        public ActiveOrdersViewModel()
        {
            ActiveOrders = new List<OrdersList>();
            ActivityIndicatorVisible = true;
            ActivityIndicatorRunning = true;
            GetActiveOrders();
        }

        public List<OrdersList> ActiveOrders { get; set; }
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
        async void GetActiveOrders()
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
