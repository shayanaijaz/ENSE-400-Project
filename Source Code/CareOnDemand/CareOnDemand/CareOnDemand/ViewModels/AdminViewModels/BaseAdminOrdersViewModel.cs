﻿/*
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
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CareOnDemand.ViewModels.AdminViewModels
{
    /* This base class defines bindings and objects that will be shared between classes relating to an admins orders.
     */
    public class BaseAdminOrdersViewModel : BaseViewModel
    {
        // Static variable declaration
        protected static Order admin_selected_order;

        // Constructor that initializes the static variable
        static BaseAdminOrdersViewModel()
        {
            admin_selected_order = new Order();
        }

        // Bindings shared between pages
        public string Location { get; set; }
        public string DateString { get; set; }
        public string TimeString { get; set; }
        public string Recipient { get; set; }
        public string Status { get; set; }
        public string FinalPrice { get; set; }
        public string AdditionalInstructions { get; set; }
        public string CarePartnerNotes { get; set; }
        public bool ElementVisible { get; set; }
        public bool ActivityIndicatorVisible { get; set; }
        public bool ActivityIndicatorRunning { get; set; }
        public Command RefreshCommand { get; set; }
        public bool IsRefreshing { get; set; }

        public List<OrdersList> Orders { get; set; }

        public ObservableCollection<Order_Service> OrderServicesList { get; set; }

        // Task that retrieves orders from the database and sets bindings
        public async Task GetOrders(string[] orderStatusArray)
        {
            Orders = await GetOrdersFromDb(orderStatusArray);
            ActivityIndicatorRunning = false;
            ActivityIndicatorVisible = false;
            OnPropertyChanged(nameof(ActivityIndicatorRunning));
            OnPropertyChanged(nameof(ActivityIndicatorVisible));
            OnPropertyChanged(nameof(Orders));
        }

        // Function that is run at intervals and refreshes the data
        public bool AutoRefreshOrderList(string[] orderStatusArray)
        {
            Device.BeginInvokeOnMainThread(async () => await GetOrders(orderStatusArray));
            return true;
        }

        // Task that is run when the user manually refreshes the page
        public async Task ManualRefreshOrderList(string[] orderStatusArray)
        {
            IsRefreshing = true;
            OnPropertyChanged(nameof(IsRefreshing));

            await GetOrders(orderStatusArray);

            IsRefreshing = false;
            OnPropertyChanged(nameof(IsRefreshing));
        }

        /* This Task is used to get all the orders from the database. It takes in an OrderStatusList arguement which is an array of strings that 
         * contains the order statuses that need to be retrieved (New, Completed etc.) It uses the REST services to retrieve the orders from the database/
         * It also retrieves the customers detailed information from the database to display on the page. It returns the list of orders 
         * that will be displayed on the page.
         */
        public async Task<List<OrdersList>> GetOrdersFromDb(string[] OrderStatus)
        {
            List<OrderStatus> order_status_list_from_db = await new OrderStatusRestService().RefreshDataAsync();

            List<int> order_statuses_to_query = new List<int>();

            foreach (var status in order_status_list_from_db)
            {
                if (OrderStatus.Contains(status.Status.Trim()))
                {
                    order_statuses_to_query.Add(status.OrderStatusID);
                }
            }

            List<Order> all_status_orders_list = new List<Order>();
            
            // Get all orders with the status and add them to a list
            foreach (var status in order_statuses_to_query)
            {
                List<Order> orders_list = await new OrderRestService().GetOrderByOrderStatusIDAsync(status);

                foreach (var order in orders_list)
                {
                    all_status_orders_list.Add(order);
                }
            }

            List<OrdersList> order_list_to_display = new List<OrdersList>();

            foreach (var order in all_status_orders_list)
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
                    ServicesOrderedString = servicesString
                });
            }

            // Sort in descending order
            order_list_to_display.Sort((a, b) => b.CustomerOrder.CreationTime.CompareTo(a.CustomerOrder.CreationTime));

            return order_list_to_display;
        }

        /* This Task is run when the admin clicks into one of the orders displayed on the page and retrieves detailed information about that order. 
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
                if (status.OrderStatusID == admin_selected_order.OrderStatusID)
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

    /* This class is a model for what information an item displayed on the page should contain. 
    */
    public class OrdersList
    {
        public string CustomerName { get; set; }
        public string ServicesOrderedString { get; set; }
        public Order CustomerOrder { get; set; }

    }
}
