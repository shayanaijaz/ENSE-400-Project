using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
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
            Orders = new List<OrdersList>();

            string[] newOrderStatusArray = { "New" };

            ActivityIndicatorRunning = true;
            ActivityIndicatorVisible = true;

            Task.Run(async () => await GetOrders(newOrderStatusArray));

            RefreshCommand = new Command(async () => await ManualRefreshOrderList(newOrderStatusArray));
            Device.StartTimer(TimeSpan.FromMinutes(5), () => AutoRefreshOrderList(newOrderStatusArray));
        }

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



    }
}
