using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using CareOnDemand.Models;

namespace CareOnDemand.ViewModels
{
    public class BaseServiceAndOrderViewModel : BaseViewModel
    {
        protected static Service user_selected_service;
        protected static Order user_order;
        public static List<Order_Service> user_order_service;
        protected static Address user_address;
        protected static Account recipient;
        protected static Account care_partner;
        protected static DateTime selected_date;
        protected static TimeSpan selected_time;
        static BaseServiceAndOrderViewModel()
        {

        }

        public ObservableCollection<String> OrderServicesList { get; set; }

        public void PopulateOrderServicesList()
        {
            //foreach (var service in user_order.Order_Services)
            //{
            //    OrderServicesList.Add(service.ServiceName.Trim() + " - " + service.RequestedLength + " hours");
            //}

            // placeholder
            foreach (var service in user_order_service)
            {
                OrderServicesList.Add(service.ServiceName.Trim() + " - " + service.RequestedLength + " hours");
            }
        }

    }
}
