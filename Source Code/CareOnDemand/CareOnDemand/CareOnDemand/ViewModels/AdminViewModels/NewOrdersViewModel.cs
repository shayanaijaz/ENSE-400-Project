﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using CareOnDemand.Data;
using CareOnDemand.Models;

namespace CareOnDemand.ViewModels.AdminViewModels
{
    public class NewOrdersViewModel : BaseAdminOrdersViewModel
    {
        public NewOrdersViewModel()
        {
            NewOrders = new List<OrdersList>();
            ActivityIndicatorVisible = true;
            ActivityIndicatorRunning = true;
            getNewOrder();
        }

        public bool ActivityIndicatorVisible { get; set; }
        public bool ActivityIndicatorRunning { get; set; }

        public List<OrdersList> NewOrders { get; set; }
        async void getNewOrder()
        {
            string[] newOrderStatusArray = {"New"};

            NewOrders = await getOrderData(newOrderStatusArray);

            ActivityIndicatorRunning = false;
            ActivityIndicatorVisible = false;
            OnPropertyChanged(nameof(ActivityIndicatorRunning));
            OnPropertyChanged(nameof(ActivityIndicatorVisible));
            OnPropertyChanged(nameof(NewOrders));
        }

    }
}
