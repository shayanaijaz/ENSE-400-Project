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
        public static ObservableCollection<Order_Service> user_order_service;
        protected static Address user_address;
        protected static Account recipient;
        protected static Account care_partner;
        protected static DateTime selected_date;
        protected static TimeSpan selected_time;
        static BaseServiceAndOrderViewModel()
        {

        }
        public ObservableCollection<Order_Service> Order_Service_List
        {
            get => user_order_service;
            set
            {
                user_order_service = value;
            }
        }

    }
}
