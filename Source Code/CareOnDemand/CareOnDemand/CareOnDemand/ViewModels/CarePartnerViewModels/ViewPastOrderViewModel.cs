using CareOnDemand.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CareOnDemand.ViewModels.CarePartnerViewModels
{
    public class ViewPastOrderViewModel : BaseCarePartnerOrdersViewModel
    {
        public ViewPastOrderViewModel()
        {
            GetOrderDetailsFromDb(care_partner_selected_order);
            OrderServicesList = new ObservableCollection<Order_Service>();
            ElementVisible = false;
            ActivityIndicatorVisible = true;
            ActivityIndicatorRunning = true;
        }
    }
}
