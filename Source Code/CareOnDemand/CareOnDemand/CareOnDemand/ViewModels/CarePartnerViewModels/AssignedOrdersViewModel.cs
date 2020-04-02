using CareOnDemand.Views.CarePartnerViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace CareOnDemand.ViewModels.CarePartnerViewModels
{
    public class AssignedOrdersViewModel : BaseCarePartnerOrdersViewModel
    {
        public AssignedOrdersViewModel()
        {
            AssignedOrders = new ObservableCollection<OrdersList>();
            ActivityIndicatorVisible = true;
            ActivityIndicatorRunning = true;
            GetAssignedOrder();
        }

        public ObservableCollection<OrdersList> AssignedOrders { get; set; }

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
        async void GetAssignedOrder()
        {
            string[] newOrderStatusArray = { "In Progress", "On The Way", "Waiting" };

            AssignedOrders = await GetOrdersFromDb(newOrderStatusArray);

            ActivityIndicatorRunning = false;
            ActivityIndicatorVisible = false;
            OnPropertyChanged(nameof(ActivityIndicatorRunning));
            OnPropertyChanged(nameof(ActivityIndicatorVisible));
            OnPropertyChanged(nameof(AssignedOrders));
        }

    }
}
