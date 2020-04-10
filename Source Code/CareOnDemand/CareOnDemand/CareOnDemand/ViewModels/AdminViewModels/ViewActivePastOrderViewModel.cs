/*
    Care on Demand Application
    Capstone 2020 - ENSE 400/477
    The Ni(c)(k)S

    Author: Shayan Khan
    Last Modified: Apr. 10, 2020
*/
using CareOnDemand.Data;
using CareOnDemand.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CareOnDemand.ViewModels.AdminViewModels
{
    /* This class inherits variables and objects from the BaseAdminOrderViewModel class and initializes those variables with the required information. This 
     * ViewModel class is shared by ViewActiveOrder and ViewPastOrder page.
     */
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
