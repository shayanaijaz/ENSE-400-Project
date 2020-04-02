using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using CareOnDemand.Data;
using CareOnDemand.Models;
using CareOnDemand.Views.AdminViews;
using Xamarin.Forms;

namespace CareOnDemand.ViewModels.AdminViewModels
{
    public class NewOrdersViewModel : BaseAdminOrdersViewModel
    {
        public NewOrdersViewModel()
        {
            NewOrders = new List<OrdersList>();
            ActivityIndicatorVisible = true;
            ActivityIndicatorRunning = true;
            GetNewOrder();
        }

        public List<OrdersList> NewOrders { get; set; }

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
            await Application.Current.MainPage.Navigation.PushAsync(new ViewNewOrder());
        }
        async void GetNewOrder()
        {
            string[] newOrderStatusArray = {"New"};

            NewOrders = await GetOrdersFromDb(newOrderStatusArray);

            ActivityIndicatorRunning = false;
            ActivityIndicatorVisible = false;
            OnPropertyChanged(nameof(ActivityIndicatorRunning));
            OnPropertyChanged(nameof(ActivityIndicatorVisible));
            OnPropertyChanged(nameof(NewOrders));
        }

    }
}
