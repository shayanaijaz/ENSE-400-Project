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

    }

    public class OrdersList
    {
        public string CustomerName { get; set; }
        public string ServicesOrderedString { get; set; }
        public Order CustomerOrder { get; set; }

    }
}
