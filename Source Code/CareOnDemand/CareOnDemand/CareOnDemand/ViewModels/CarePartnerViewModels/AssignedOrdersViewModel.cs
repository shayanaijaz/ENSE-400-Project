using CareOnDemand.Views.CarePartnerViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CareOnDemand.ViewModels.CarePartnerViewModels
{
    public class AssignedOrdersViewModel : BaseCarePartnerOrdersViewModel
    {
        public AssignedOrdersViewModel()
        {
            Orders = new List<OrdersList>();
            ActivityIndicatorVisible = true;
            ActivityIndicatorRunning = true;

            string[] assignedOrderStatusArray = { "In Progress", "On The Way", "Waiting" };

            Task.Run(async () => await GetOrders(assignedOrderStatusArray));

            RefreshCommand = new Command(async () => await ManualRefreshOrderList(assignedOrderStatusArray));
            Device.StartTimer(TimeSpan.FromMinutes(5), () => AutoRefreshOrderList(assignedOrderStatusArray));

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

                care_partner_selected_order = selectedOrder.CustomerOrder;

                OrderSelected();
            }
        }
        async void OrderSelected()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ViewAssignedOrder());
        }

    }
}
