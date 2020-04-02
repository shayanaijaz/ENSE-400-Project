using CareOnDemand.Models;
using CareOnDemand.Views.AdminViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

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

                admin_selected_order = selectedOrder.CustomerOrder;

                OrderSelected();
            }
        }

        async void OrderSelected()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ViewActivePastOrders());
        }
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
