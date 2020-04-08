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
using CareOnDemand.Data;
using CareOnDemand.Models;
using CareOnDemand.Views.CustomerViews;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CareOnDemand.ViewModels
{
    /*
     * Class that defines binding and functions relating to elements on the ServiceSelectionList page. This class
     * inherits from the BaseServiceAndOrderViewModel.
     */ 
    class ServiceSelectionListViewModel : BaseServiceAndOrderViewModel
    {
        // Constructor that initializes the bindings
        public ServiceSelectionListViewModel()
        {
            user_selected_service = new Service();
            ServiceList = new ObservableCollection<Service>();
            ActivityIndicatorVisible = true;
            ActivityIndicatorRunning = true;
            PopulateServiceList();
            PopulateCheckoutButton();
            CheckoutCommand = new Command(async () => await CheckoutButtonClicked());
        }

        // Bindings used on this page
        public ObservableCollection<Service> ServiceList { get; set; }
        public Command CheckoutCommand { private set; get; }
        public bool CheckoutIsVisible { get; set; }
        public string CheckoutText { get; set; }
        public bool ActivityIndicatorVisible { get; set; }
        public bool ActivityIndicatorRunning { get; set; }
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

        /* This function uses the ServiceRestService class and retrieves the list of services from the database and assigns 
         * it to the binding for ServiceList
        */
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

            ActivityIndicatorRunning = false;
            ActivityIndicatorVisible = false;
            OnPropertyChanged(nameof(ActivityIndicatorRunning));
            OnPropertyChanged(nameof(ActivityIndicatorVisible));
        }

        // Function that redirects user to the ServiceSelectedDetails page
        async void ServiceSelectedClicked()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ServiceSelectedDetails());
        }

        /* This function shows or hides the checkout button depending on whether the user has any services in the cart or not
         */ 
        public void PopulateCheckoutButton()
        {
            if (user_order != null)
            {
                if (user_order_service.Count > 0)
                {
                    CheckoutIsVisible = true;
                    CheckoutText = "Checkout " + "(" + user_order_service.Count + ")";
                }
                else
                {
                    CheckoutIsVisible = false;
                }

                OnPropertyChanged(nameof(CheckoutIsVisible));
            }
        }

        // Function that redirects user to the OrderDetails page when the Checkout button is clicked
        async Task CheckoutButtonClicked()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new OrderDetails());
        }
    }
}
