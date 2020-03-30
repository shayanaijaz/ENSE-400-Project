using CareOnDemand.Data;
using CareOnDemand.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace CareOnDemand.ViewModels.AdminViewModels
{
    public class BaseAdminOrdersViewModel : BaseViewModel
    {
        protected static Order admin_selected_order;
        static BaseAdminOrdersViewModel()
        {
            admin_selected_order = new Order();
        }

        public string Location { get; set; }
        public string DateString { get; set; }
        public string TimeString { get; set; }
        public string Recipient { get; set; }
        public string FinalPrice { get; set; }
        public string AdditionalInstructions { get; set; }
        public string CarePartnerNotes { get; set; }


        public ObservableCollection<Order_Service> OrderServicesList { get; set; }

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

            return order_list_to_display;
        }

        public async void GetOrderDetailsFromDb(Order order)
        {
            Customer customer = await new CustomerRestService().GetCustomerByIDAsync(order.CustomerID);
            Account account = await new AccountRestService().GetAccountByIDAsync(customer.AccountID);

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

            OrderServicesList = orderServiceCollection;
            OnPropertyChanged(nameof(OrderServicesList));

            Location = user_address.AddrLine1.Trim() + ", " + user_address.City.Trim() + ", " + user_address.Province.Trim() + ", " + user_address.PostalCode.Trim();
            OnPropertyChanged(nameof(Location));

            DateString = order.RequestedTime.Date.ToString("yyyy-MM-dd");
            OnPropertyChanged(nameof(DateString));


            TimeString = new DateTime(order.RequestedTime.Ticks).ToString("h:mm tt");
            OnPropertyChanged(nameof(TimeString));

            Recipient = account.FirstName.Trim() + " " + account.LastName.Trim();
            OnPropertyChanged(nameof(Recipient));

            FinalPrice = "$" + finalPrice.ToString();
            OnPropertyChanged(nameof(FinalPrice));

            if (order.OrderInstructions != null)
            {
                AdditionalInstructions = order.OrderInstructions.Trim();
                OnPropertyChanged(nameof(AdditionalInstructions));
            }
        }

    }

    public class OrdersList
    {
        public string CustomerName { get; set; }
        public string ServicesOrderedString { get; set; }
        public Order CustomerOrder { get; set; }

    }
}
