using CareOnDemand.Data;
using CareOnDemand.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace CareOnDemand.ViewModels.AdminViewModels
{
    public class ViewActivePastOrderViewModel : BaseAdminOrdersViewModel
    {
        public ViewActivePastOrderViewModel()
        {
            Task.Run(async () => await GetOrderDetailsFromDb(admin_selected_order));
            Task.Run(async () => await GetServiceRequest());
            OrderServicesList = new ObservableCollection<Order_Service>();
            ElementVisible = false;
            ActivityIndicatorVisible = true;
            ActivityIndicatorRunning = true;
        }

        async Task GetServiceRequest()
        {
            ServiceRequest serviceRequest = await new ServiceRequestRestService().GetServiceRequestByIDAsync(admin_selected_order.OrderID);

            CarePartnerNotes = serviceRequest.OrderNotes.Trim();
            OnPropertyChanged(nameof(CarePartnerNotes));
        }

    }
}
