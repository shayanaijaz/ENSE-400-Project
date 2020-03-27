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
    class ServiceSelectedDetailsViewModel : BaseServiceAndOrderViewModel
    {
        public ServiceSelectedDetailsViewModel()
        {
            DurationList = new ObservableCollection<Duration>();
            selected_duration = new Duration();
            PopulateDurationList();
            AddToCartCommand = new Command(async () => await AddToCartClicked());

        }

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

        async Task AddToCartClicked()
        {

            if (user_order == null)
            {
                user_order = new Order();
                //user_order.Order_Services = new List<Order_Service>();
                //placeholder
                user_order_service = new ObservableCollection<Order_Service>();
            }

            Order_Service orderService = new Order_Service{ ServiceID = user_selected_service.ServiceID, RequestedLength = selected_duration.Time, ServiceName = user_selected_service.ServiceName.Trim()};

            //user_order.Order_Services.Add(orderService);
            // placeholder
            user_order_service.Add(orderService);

            await Application.Current.MainPage.DisplayAlert("Success", "Item succesfully added to cart", "OK");
            await Application.Current.MainPage.Navigation.PushAsync(new CustomerNavBar());
        }
        public class Duration
        {
            public int Time { get; set; }
            public string TimeSentence { get; set; }
            public int Price { get; set; }
        }

    }
}
