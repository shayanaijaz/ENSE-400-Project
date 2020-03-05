using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Xamarin.Forms;
using CareOnDemand.Views.CustomerViews;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace CareOnDemand.ViewModels
{
    public class OrderDetailsViewModel : BaseServiceAndOrderViewModel
    {
        public OrderDetailsViewModel()
        {
            LocationList = GetLocation().ToList();
            MinimumDate = GetMinimumDate();
            TimeList = GetTime().ToList();
            RecipientList = GetRecipient().ToList();
            CarePartnerList = GetCarePartner().ToList();
            ContinueOrderCommand = new Command(async () => await ContinueOrderClicked());

            OrderServicesList = new ObservableCollection<string>();
            PopulateOrderServicesList();

        }

        public List<Location> LocationList { get; set; }
        public DateTime MinimumDate { get; set; }
        public static DateTime Now { get; }
        public List<Time> TimeList { get; set; }
        public List<Recipient> RecipientList { get; set; }
        public List<CarePartner> CarePartnerList { get; set; }

        public ObservableCollection<String> OrderServicesList { get; set; }

        public Command ContinueOrderCommand { get; set; }

        async Task ContinueOrderClicked()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ServiceReview());
        }

        public void PopulateOrderServicesList()
        {
            foreach(var service in user_order.Order_Services)
            {
                OrderServicesList.Add(service.ServiceName.Trim() + " - " + service.ServiceLength + " hours");
            }
        }
        public List<Location> GetLocation()
        {
            var location = new List<Location>()
            {
                new Location(){Key =  1, Value= "My Home"},
                new Location(){Key =  2, Value= "Grandmas's Home"},
                new Location(){Key =  3, Value= "*Add New*"}
            };

            return location;
        }
        public DateTime GetMinimumDate()
        {
            var minimumDate = DateTime.Now;
            return minimumDate;
        }
        public List<Time> GetTime()
        {
            var time = new List<Time>()
            {
                new Time(){Key =  1, Value= "9:00 AM"},
                new Time(){Key =  2, Value= "10:00 AM"},
                new Time(){Key =  3, Value= "11:30 AM"},
                new Time(){Key =  3, Value= "11:30 AM"},
                new Time(){Key =  3, Value= "12:30 PM"},
                new Time(){Key =  3, Value= "1:30 PM"}
            };

            return time;
        }
        public List<Recipient> GetRecipient()
        {
            var recipient = new List<Recipient>()
            {
                new Recipient(){Key =  1, Value= "Myself"},
                new Recipient(){Key =  2, Value= "Grandma"}
            };

            return recipient;
        }
        public List<CarePartner> GetCarePartner()
        {
            var carePartner = new List<CarePartner>()
            {
                new CarePartner(){Key =  1, Value= "No Preference"},
                new CarePartner(){Key =  2, Value= "Joe Fresh"},
                new CarePartner(){Key =  3, Value= "Bill Nye"},
                new CarePartner(){Key =  3, Value= "Gary Snail"}
            };
            return carePartner;
        }

        public class Location
        {
            public int Key { get; set; }
            public string Value { get; set; }
        }
        public class Time
        {
            public int Key { get; set; }
            public string Value { get; set; }
        }
        public class Recipient
        {
            public int Key { get; set; }
            public string Value { get; set; }
        }
        public class CarePartner
        {
            public int Key { get; set; }
            public string Value { get; set; }
        }
    }
}
