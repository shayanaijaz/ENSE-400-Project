using CareOnDemand.Data;
using CareOnDemand.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace CareOnDemand.ViewModels.CarePartnerViewModels
{
    public class BaseCarePartnerOrdersViewModel : BaseViewModel
    {
        protected static Order care_partner_selected_order;
        static BaseCarePartnerOrdersViewModel()
        {
            care_partner_selected_order = new Order();
        }

        public string Location { get; set; }
        public string DateString { get; set; }
        public string TimeString { get; set; }
        public string Recipient { get; set; }
        public string Status { get; set; }
        public string FinalPrice { get; set; }
        public string AdditionalInstructions { get; set; }
        public string CarePartnerNotes { get; set; }
        public ObservableCollection<Order_Service> OrderServicesList { get; set; }


        public async void GetOrdersFromDb()
        {
        }

        public class OrdersList
        {
            public string CustomerName { get; set; }
            public string ServicesOrderedString { get; set; }
            public Order CustomerOrder { get; set; }

        }

    }
}
