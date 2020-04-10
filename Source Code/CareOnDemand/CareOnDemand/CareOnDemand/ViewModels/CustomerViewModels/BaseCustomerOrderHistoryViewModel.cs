/*
    Care on Demand Application
    Capstone 2020 - ENSE 400/477
    The Ni(c)(k)S

    Author: Shayan Khan
    Last Modified: Apr. 10, 2020
*/
using CareOnDemand.Data;
using CareOnDemand.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CareOnDemand.ViewModels.CustomerViewModels
{
    /* This base class defines bindings and objects that will be shared between classes relating to viewing a customers order history.
     */ 
    public class BaseCustomerOrderHistoryViewModel : BaseViewModel
    {
        // Static variable declaration
        protected static Order customer_selected_order;

        // Defining bindings that will be common among the pages
        public bool ActivityIndicatorVisible { get; set; }
        public bool ActivityIndicatorRunning { get; set; }
        public string Location { get; set; }
        public string DateString { get; set; }
        public string TimeString { get; set; }
        public string Recipient { get; set; }
        public string Status { get; set; }
        public string FinalPrice { get; set; }
        public string AdditionalInstructions { get; set; }
        public ObservableCollection<Order_Service> OrderServicesList { get; set; }
        public bool ElementVisible { get; set; }

        // Constructor that initializes the static variable
        static BaseCustomerOrderHistoryViewModel()
        {
            customer_selected_order = new Order();
        }

        /* This function is used to get all the orders from the database that are created by the user that is logged in. It takes in an OrderStatusList
         * arguement which is an array of strings that contains the order statuses that need to be retrieved (New, Completed etc.) It uses the REST services to 
         * retrieve the orders from the database using the logged in customers ID. It also retrieves the customers detailed information from the database to display
         * on the page. It returns the list of orders that will be displayed on the page.
         */ 
        public async Task<List<OrdersList>> GetOrdersFromDb(string[] OrderStatusList)
        {
            int customerID = (int)Application.Current.Properties["customerID"];
            List<Order> orderList = await new OrderRestService().GetOrderByCustomerIDAsync(customerID);

            List<OrdersList> order_list_to_display = new List<OrdersList>();

            foreach (var order in orderList)
            {
                OrderStatus orderStatus = await new OrderStatusRestService().GetOrderStatusByIDAsync(order.OrderStatusID);

                // Use only orders whose status matches one of the statuses in OrderStatusList
                if (OrderStatusList.Contains(orderStatus.Status.Trim()))
                {
                    List<Order_Service> order_services = await new Order_ServiceRestService().GetOrderServiceByID(order.OrderID);

                    List<Service> order_service_list = new List<Service>();

                    string servicesString = "";

                    foreach (var service in order_services)
                    {
                        var service_details = await new ServiceRestService().GetServiceByIDAsync(service.ServiceID);
                        order_service_list.Add(service_details);
                        servicesString += string.Format("\u2022 " + service_details.ServiceName.Trim() + " - " + service.RequestedLength + " hours " + "{0}", Environment.NewLine);
                    }

                    Customer customer = await new CustomerRestService().GetCustomerByIDAsync(order.CustomerID);

                    Account account = await new AccountRestService().GetAccountByIDAsync(customer.AccountID);

                    order_list_to_display.Add(new OrdersList
                    {
                        CustomerName = account.FirstName.Trim() + " " + account.LastName.Trim(),
                        CustomerOrder = order,
                        ServicesOrderedString = servicesString.Trim(),
                    });
                }
            }
            // Sort in descending order
            order_list_to_display.Sort((a, b) => b.CustomerOrder.CreationTime.CompareTo(a.CustomerOrder.CreationTime));


            return order_list_to_display;
        }

        /* This function is run when the user clicks into one of the orders displayed on the page and retrieves detailed information about that order. 
         * It takes in an Order arguement which is the order that was clicked by the user. It uses the REST services to retrieve customer information 
         * as well as details about the selected order. It also formats the data to display on the page and updates the elements with the new data. 
         */ 
        public async Task GetOrderDetailsFromDb(Order order)
        {
            Customer customer = await new CustomerRestService().GetCustomerByIDAsync(order.CustomerID);
            Account account = await new AccountRestService().GetAccountByIDAsync(customer.AccountID);
            List<OrderStatus> orderStatusList = await new OrderStatusRestService().RefreshDataAsync();

            List<Order_Service> order_services = await new Order_ServiceRestService().GetOrderServiceByID(order.OrderID);

            double finalPrice = 0;


            foreach (var service in order_services)
            {
                var service_details = await new ServiceRestService().GetServiceByIDAsync(service.ServiceID);
                service.ServiceName = service_details.ServiceName.Trim();
                finalPrice += service.RequestedLength * service_details.ServicePrice;
            }

            Address user_address = await new AddressRestService().GetAddressByIDAsync(order.AddressID);

            ObservableCollection<Order_Service> orderServiceCollection = new ObservableCollection<Order_Service>(order_services as List<Order_Service>);

            foreach (var status in orderStatusList)
            {
                if (status.OrderStatusID == order.OrderStatusID)
                {
                    Status = status.Status.Trim();
                }

            }

            OrderServicesList = orderServiceCollection;

            Location = user_address.AddrLine1.Trim() + ", " + user_address.City.Trim() + ", " + user_address.Province.Trim() + ", " + user_address.PostalCode.Trim();

            DateString = order.RequestedTime.Date.ToString("yyyy-MM-dd");


            TimeString = new DateTime(order.RequestedTime.Ticks).ToString("h:mm tt");

            Recipient = account.FirstName.Trim() + " " + account.LastName.Trim();

            FinalPrice = "$" + finalPrice.ToString();

            if (order.OrderInstructions != null)
            {
                AdditionalInstructions = order.OrderInstructions.Trim();
            }
            else
            {
                AdditionalInstructions = "None";
            }

            ElementVisible = true;
            ActivityIndicatorRunning = false;
            ActivityIndicatorVisible = false;

            // Update the elements
            OnPropertyChanged(nameof(ActivityIndicatorRunning));
            OnPropertyChanged(nameof(ActivityIndicatorVisible));
            OnPropertyChanged(nameof(ElementVisible));
            OnPropertyChanged(nameof(Status));
            OnPropertyChanged(nameof(OrderServicesList));
            OnPropertyChanged(nameof(Location));
            OnPropertyChanged(nameof(DateString));
            OnPropertyChanged(nameof(Recipient));
            OnPropertyChanged(nameof(TimeString));
            OnPropertyChanged(nameof(FinalPrice));
            OnPropertyChanged(nameof(AdditionalInstructions));

        }
    }
}
