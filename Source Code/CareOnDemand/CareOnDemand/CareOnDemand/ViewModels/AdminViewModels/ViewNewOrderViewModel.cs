using CareOnDemand.Data;
using CareOnDemand.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareOnDemand.ViewModels.AdminViewModels
{
    public class ViewNewOrderViewModel : BaseAdminOrdersViewModel
    {
        public ViewNewOrderViewModel()
        {

        }



        async void GetOrderData(Order order)
        {
            Customer customer = await new CustomerRestService().GetCustomerByIDAsync(order.CustomerID);
            Account account = await new AccountRestService().GetAccountByIDAsync(customer.AccountID);

            List<Order_Service> order_services = await new Order_ServiceRestService().GetOrderServiceByID(order.OrderID);

            List<Service> order_service_list = new List<Service>();

            string servicesString = "";

            foreach (var service in order_services)
            {
                var service_details = await new ServiceRestService().GetServiceByIDAsync(service.ServiceID);
                order_service_list.Add(service_details);
                servicesString += string.Format("\u2022 " + service_details.ServiceName.Trim() + " - " + service.RequestedLength + " hours " + "{0}", Environment.NewLine);
            }

            Address address = await new AddressRestService().GetAddressByIDAsync(order.AddressID);
            

        }
    }
}
