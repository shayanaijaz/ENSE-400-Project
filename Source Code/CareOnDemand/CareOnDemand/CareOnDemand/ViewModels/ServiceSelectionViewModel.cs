using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CareOnDemand.ViewModels
{
    public class ServiceSelectionViewModel : BaseViewModel
    {
        public ServiceSelectionViewModel()
        {
            ServicesList = GetServices().ToList();
        }

        public List<Service> ServicesList { get; set; }


        public List<Service> GetServices()
        {
            var services = new List<Service>()
            {
                new Service(){Key =  1, Value= "Housekeeping"},
                new Service(){Key =  2, Value= "Laundry"},
                new Service(){Key =  3, Value= "Meal Prep"},
                new Service(){Key =  4, Value= "Medication Assistance"},
                new Service(){Key =  5, Value= "Personal Care"},
                new Service(){Key =  6, Value= "Bathing Services"},
                new Service(){Key =  7, Value= "Companionship in Home"}
            };

            return services;
        }

        public class Service
        {
            public int Key { get; set; }
            public string Value { get; set; }
        }
    }
}
