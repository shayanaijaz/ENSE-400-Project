/*
    Care on Demand Application
    Capstone 2020 - ENSE 400/477
    The Ni(c)(k)S

    Author: Shayan Khan
    Last Modified: Apr. 07, 2020
*/
using CareOnDemand.Models;
using CareOnDemand.Views.CustomerViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CareOnDemand.ViewModels
{
    /*
     * This class defines bindings and functions for elements located on the ServiceSelectedDetails page. It inherits from 
     * the BaseServiceAndOrderViewModel class. 
     */
    class ServiceSelectedDetailsViewModel : BaseServiceAndOrderViewModel
    {
        // Constructor that initializes the bindings
        public ServiceSelectedDetailsViewModel()
        {
            DurationList = new ObservableCollection<Duration>();
            selected_duration = new Duration();
            PopulateDurationList();
            AddToCartCommand = new Command(async () => await AddToCartClicked());

        }

        // Bindings used on this page
        public ObservableCollection<Duration> DurationList { get; set; }

        protected static Duration selected_duration;
        public string PriceText { get; set; }
        public bool AddToCartIsVisible { get; set; }

        public Command AddToCartCommand { private set; get; }

        public String ServiceDescription
        {
            get => user_selected_service.ServiceDescription;
            set
            {
                user_selected_service.ServiceDescription = value;
            }
        }
        public Duration SelectedDuration
        {
            get => selected_duration;
            set
            {
                selected_duration = value;

                if (value != null)
                {
                    PriceText = "Price: $";
                    AddToCartIsVisible = true;
                    OnPropertyChanged(nameof(SelectedDuration));
                    OnPropertyChanged(nameof(PriceText));
                    OnPropertyChanged(nameof(AddToCartIsVisible));
                }
                
            }
        }

        // Function that populates the duration for all the services (1, 2, and 3 hours)
        public void PopulateDurationList()
        {
            if (user_selected_service.Length == 1)
            {
                for (int i = 1; i <= 3; i++)
                {
                    DurationList.Add(new Duration { Time = i, TimeSentence = i + " hours", Price = user_selected_service.ServicePrice * i });
                }
            }
        }

        /*
         * This function runs when user clicks the Add to Cart button. It starts a new Order if one does not already exists and 
         * add the service to the order. 
         */
        async Task AddToCartClicked()
        {

            if (user_order == null)
            {
                user_order = new Order();
                user_order_service = new ObservableCollection<Order_Service>();
            }

            Order_Service orderService = new Order_Service{ ServiceID = user_selected_service.ServiceID, RequestedLength = selected_duration.Time, ServiceName = user_selected_service.ServiceName.Trim()};

            user_order_service.Add(orderService);

            await Application.Current.MainPage.DisplayAlert("Success", "Item succesfully added to cart", "OK");
            await Application.Current.MainPage.Navigation.PopAsync();
            await Application.Current.MainPage.Navigation.PushAsync(new CustomerNavBar());
        }

        // Class that defines the Duration object that is used to show the durations of a service
        public class Duration
        {
            public int Time { get; set; }
            public string TimeSentence { get; set; }
            public int Price { get; set; }
        }

    }
}
