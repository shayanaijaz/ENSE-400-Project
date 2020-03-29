using CareOnDemand.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CareOnDemand.ViewModels.AdminViewModels
{
    public class ViewActivePastOrderViewModel : BaseAdminOrdersViewModel
    {
        public ViewActivePastOrderViewModel()
        {
            GetOrderDetailsFromDb(admin_selected_order);
            OrderServicesList = new ObservableCollection<Order_Service>();
        }


    }
}
