/*
    Care on Demand Application
    Capstone 2020 - ENSE 400/477
    The Ni(c)(k)S

    Author: Shayan Khan
    Last Modified: Apr. 10, 2020
*/
using CareOnDemand.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CareOnDemand.ViewModels.CarePartnerViewModels
{
    /* This class inherits variables and objects from the BaseCarePartnerOrdersViewModel class and initializes those variables with the required information
     */
    public class ViewPastOrderViewModel : BaseCarePartnerOrdersViewModel
    {
        public ViewPastOrderViewModel()
        {
            Task.Run(async () => await GetOrderDetailsFromDb(care_partner_selected_order));
            OrderServicesList = new ObservableCollection<Order_Service>();
            ElementVisible = false;
            ActivityIndicatorVisible = true;
            ActivityIndicatorRunning = true;
        }
    }
}
