using System;
using System.Collections.Generic;
using System.Text;
using CareOnDemand.Models;

namespace CareOnDemand.ViewModels
{
    public class BaseServiceAndOrderViewModel : BaseViewModel
    {
        protected static Service user_selected_service;
        protected static Order user_order;
        static BaseServiceAndOrderViewModel()
        {

        }


    }
}
