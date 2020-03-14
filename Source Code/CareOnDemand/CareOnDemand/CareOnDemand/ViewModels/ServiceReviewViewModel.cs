using CareOnDemand.Data;
using CareOnDemand.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CareOnDemand.ViewModels
{
    public class ServiceReviewViewModel : BaseServiceAndOrderViewModel
    {
        public ServiceReviewViewModel()
        {
            OrderServicesList = new ObservableCollection<string>();
            PopulateOrderServicesList();
            GetFullUserAddress();
            GetDatetime();
            GetFinalPrice();
            SubmitOrderCommand = new Command(async () => await SubmitOrderClicked());
        }

        public Command SubmitOrderCommand { private set; get; }
        public String Address { get; set; }
        public String Recipient
        {
            get => recipient.FullName;
            set
            {
                recipient.FullName = value;
            }
        }

        public String DateString { get; set; }
        public String TimeString { get; set; }
        public String FinalPrice { get; set; }

        public void GetFullUserAddress()
        {
            Address = user_address.AddrLine1.Trim() + ", " + user_address.City.Trim() + ", " + user_address.Province.Trim() + ", " + user_address.PostalCode.Trim();
        }

        public void GetDatetime()
        {
            DateString = selected_date.Date.ToString("yyyy-MM-dd");
            TimeString = new DateTime(selected_time.Ticks).ToString("h:mm tt");
        }

        async void GetFinalPrice()
        {
            ServiceRestService serviceRestService = new ServiceRestService();

            float total = 0;

            //foreach(var service in user_order.Order_Services)
            //{
            //    var retrieved_service = await serviceRestService.GetServiceByIDAsync(service.ServiceID);
            //    int price = retrieved_service.ServicePrice;
            //    total += price * service.RequestedLength;
            //}

            // placeholder
            foreach (var service in user_order_service)
            {
                var retrieved_service = await serviceRestService.GetServiceByIDAsync(service.ServiceID);
                int price = retrieved_service.ServicePrice;
                total += price * service.RequestedLength;
            }

            FinalPrice = "$" + total.ToString();
            OnPropertyChanged(nameof(FinalPrice));
        }


        async Task SubmitOrderClicked()
        {
            CustomerRestService customerRestService = new CustomerRestService();
            OrderStatusRestService orderStatusRestService = new OrderStatusRestService();
            OrderRestService orderRestService = new OrderRestService();
            Order_ServiceRestService order_ServiceRestService = new Order_ServiceRestService();

            var customer = await customerRestService.GetCustomerByAccountIDAsync(recipient.AccountID);

            user_order.AddressID = user_address.AddressID;
            user_order.CustomerID = customer[0].CustomerID;
            user_order.OrderForID = 0;
            user_order.PaymentMethodID = 0;

            var order_statuses = await orderStatusRestService.RefreshDataAsync();

            foreach(var order_status in order_statuses)
            {
                if (order_status.Status.Trim() == "New")
                {
                    user_order.OrderStatusID = order_status.OrderStatusID;
                    break;
                }
            }

            DateTime date = DateTime.Parse(DateString + " " + TimeString);

            user_order.RequestedTime = date;
            user_order.CreationTime = date;

            var created_order = await orderRestService.SaveOrderAsync(user_order, true);

            foreach (var service in user_order_service)
            {
                service.OrderID = created_order.OrderID;
                await order_ServiceRestService.SaveOrder_SericeAsync(service, true);
            }

            await Application.Current.MainPage.DisplayAlert("Success", "Order placed successfully!", "OK");

        }
    }
}
