/*
    Care on Demand Application
    Capstone 2020 - ENSE 400/477
    The Ni(c)(k)S

    Author: Shayan Khan
    Last Modified: Apr. 07, 2020
*/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using CareOnDemand.Models;

namespace CareOnDemand.ViewModels
{
    /*
    * This base class defines bindings that will be shared between other customer related classes.
    * Bindings relating to customer order are defined in this class. 
    */
    public class BaseServiceAndOrderViewModel : BaseViewModel
    {
        // Static variables that will be shared between classes 
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

        // Binding that will be shared between classes
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
