using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CareOnDemand.Data;
using CareOnDemand.Models;
using CareOnDemand.Views.CustomerViews;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CareOnDemand.ViewModels
{
    class ServiceSelectionListViewModel : BaseServiceAndOrderViewModel
    {
        public ServiceSelectionListViewModel()
        {
            user_selected_service = new Service();
            ServiceList = new ObservableCollection<Service>();
            PopulateServiceList();
            PopulateCheckoutButton();
            CheckoutCommand = new Command(async () => await CheckoutButtonClicked());
            TextCellCommand = new Command(async () => await TextCellClicked());
        }

        public ObservableCollection<Service> ServiceList { get; set; }
        public Command CheckoutCommand { private set; get; }

        public Command TextCellCommand { private set; get; }
        public bool CheckoutIsVisible { get; set; }
        public string CheckoutText { get; set; }

        public Service SelectedService
        {
            get => user_selected_service;
            set
            {
                user_selected_service = value;

                if (user_selected_service == null)
                    return;

                ServiceSelectedClicked();
            }
        }

        async Task TextCellClicked()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ServiceSelectedDetails());
        }


        async void PopulateServiceList()
        {
            ServiceRestService serviceRestService = new ServiceRestService();

            var result = await serviceRestService.RefreshDataAsync();

            if(result.Count != 0)
            {
                foreach(var service_result in result)
                {
                    ServiceList.Add(new Service { ServiceName = service_result.ServiceName, ServiceDescription = service_result.ServiceDescription, ServicePrice = service_result.ServicePrice,
                    Length = service_result.Length, ServiceID = service_result.ServiceID});
                }
            }
        }

        async void ServiceSelectedClicked()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ServiceSelectedDetails());
        }

        public void PopulateCheckoutButton()
        {
            if (user_order != null)
            {
                CheckoutIsVisible = true;
                //CheckoutText = "Checkout " + "(" + user_order.Order_Services.Count + ")";
                // placeholder
                CheckoutText = "Checkout " + "(" + user_order_service.Count + ")";
            }
        }

        async Task CheckoutButtonClicked()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new OrderDetails());
        }
    }
}
