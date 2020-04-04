using CareOnDemand.Models;
using CareOnDemand.Views.AdminViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CareOnDemand.ViewModels.AdminViewModels
{
    public class ActiveOrdersViewModel : BaseAdminOrdersViewModel
    {
        public ActiveOrdersViewModel()
        {
            Orders = new List<OrdersList>();
            ActivityIndicatorVisible = true;
            ActivityIndicatorRunning = true;

            string[] activeOrderStatusArray = { "In Progress", "On The Way", "Waiting" };

            Task.Run(async () => await GetOrders(activeOrderStatusArray));

            RefreshCommand = new Command(async () => await ManualRefreshOrderList(activeOrderStatusArray));
            Device.StartTimer(TimeSpan.FromMinutes(5), () => AutoRefreshOrderList(activeOrderStatusArray));

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
            await Application.Current.MainPage.Navigation.PushAsync(new ViewActivePastOrders());
        }
    }
}
