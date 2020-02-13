using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CareOnDemand.ViewModels
{
    class ServiceSelectionListViewModel : BaseViewModel
    {

        public ServiceSelectionListViewModel()
        {
            ServiceList = new ObservableCollection<Service>();
            ServiceList.Add(new Service { Name = "Laundry", Description = "We do yo laundry bih" });
            ServiceList.Add(new Service { Name = "Housekeeping", Description = "We keep yo house duh" });
            ServiceList.Add(new Service { Name = "Meal Prep", Description = "Make sure to get yo protein" });
            ServiceList.Add(new Service { Name = "Medical Assistance", Description = "We got yo boo boo" });
        }

        public ObservableCollection<Service> ServiceList { get; set; }

        public class Service
        {
            public String Name { get; set; }
            public String Description { get; set; }
        }


    }
}
