using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CareOnDemand.Data;
using CareOnDemand.Models;
using CareOnDemand.Views.CustomerViews;
using System.Text;
using Xamarin.Forms;

namespace CareOnDemand.ViewModels
{
    class ServiceSelectionListViewModel : BaseServiceViewModel
    {
        public ServiceSelectionListViewModel()
        {
            ServiceList = new ObservableCollection<Service>();
            PopulateServiceList();
        }

        public ObservableCollection<Service> ServiceList { get; set; }


        async void PopulateServiceList()
        {
            ServiceRestService serviceRestService = new ServiceRestService();

            var result = await serviceRestService.RefreshDataAsync();

            if(result.Count != 0)
            {
                foreach(var service_result in result)
                {
                    ServiceList.Add(new Service { ServiceName = service_result.ServiceName, ServiceDescription = service_result.ServiceDescription, ServicePrice = service_result.ServicePrice,
                    Length = service_result.Length});
                }
            }
        }
    }
}
