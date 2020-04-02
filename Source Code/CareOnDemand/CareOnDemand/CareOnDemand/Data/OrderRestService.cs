using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CareOnDemand.Helpers;
using CareOnDemand.Models;
using Newtonsoft.Json;

namespace CareOnDemand.Data
{
    class OrderRestService
    {
        HttpClient _client;

        public List<Order> Orders { get; private set; }

        public OrderRestService()
        {
            _client = new HttpClient();
        }

        public async Task<List<Order>> RefreshDataAsync()
        {
            Orders = new List<Order>(); 

             var uri = new Uri(string.Format(Constants.OrdersUrl, string.Empty));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Orders = JsonConvert.DeserializeObject<List<Order>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Orders;
        }

        public async Task<List<Order>> GetOrderByOrderStatusIDAsync(int orderStatusID)
        {
            Orders = new List<Order>();

            try
            {
                var uri = new Uri(string.Format(Constants.OrdersByOrderStatusIDUrl, orderStatusID));

                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Orders = JsonConvert.DeserializeObject<List<Order>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Orders;
        }

        public async Task<Order> GetOrdersByOrderIDAsync(int orderID)
        {
            Order order = new Order();

            try
            {
                var uri = new Uri(string.Format(Constants.OrdersUrl, orderID));

                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    order = JsonConvert.DeserializeObject<Order>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return order;
        }

        public async Task<Order> SaveOrderAsync(Order item, bool isNewItem = false)
        {
            var uri = new Uri(string.Format(Constants.OrdersUrl, string.Empty));

            Order Orders = new Order();
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await _client.PostAsync(uri, content);
                    var response_content = await response.Content.ReadAsStringAsync();
                    Orders = JsonConvert.DeserializeObject<Order>(response_content);
                }
                else
                {
                    uri = new Uri(string.Format(Constants.OrdersUrl, item.OrderID));
                    response = await _client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tOrder successfully saved.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Orders;
        }

        public async Task DeleteOrderAsync(string id)
        {
            var uri = new Uri(string.Format(Constants.OrdersUrl, id));

            try
            {
                var response = await _client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tOrder successfully deleted.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
    }

    class Order_ServiceRestService
    {
        HttpClient _client;

        public List<Order_Service> Order_Services { get; private set; }

        public Order_ServiceRestService()
        {
            _client = new HttpClient();
        }

        public async Task<List<Order_Service>> RefreshDataAsync()
        {
            Order_Services = new List<Order_Service>();

            var uri = new Uri(string.Format(Constants.Order_ServicesUrl, string.Empty));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Order_Services = JsonConvert.DeserializeObject<List<Order_Service>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Order_Services;
        }


        public async Task<List<Order_Service>> GetOrderServiceByID(int orderID)
        {
            Order_Services = new List<Order_Service>();

            var uri = new Uri(string.Format(Constants.Order_ServicesUrl, orderID));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Order_Services = JsonConvert.DeserializeObject<List<Order_Service>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Order_Services;
        }
        public async Task SaveOrder_SericeAsync(Order_Service item, bool isNewItem = false)
        {
            var uri = new Uri(string.Format(Constants.Order_ServicesUrl, string.Empty));

            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    response = await _client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tOrder_Service successfully saved.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task DeleteOrder_ServiceAsync(string id)
        {
            var uri = new Uri(string.Format(Constants.Order_ServicesUrl, id));

            try
            {
                var response = await _client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tOrder_Service successfully deleted.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
    }

    class OrderStatusRestService
    {
        HttpClient _client;

        public List<OrderStatus> orderStatuses { get; private set; }

        public OrderStatusRestService()
        {
            _client = new HttpClient();
        }

        public async Task<List<OrderStatus>> RefreshDataAsync()
        {
            orderStatuses = new List<OrderStatus>();

            var uri = new Uri(string.Format(Constants.OrderStatusesUrl, string.Empty));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    orderStatuses = JsonConvert.DeserializeObject<List<OrderStatus>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return orderStatuses;
        }

        public async Task<OrderStatus> GetOrderStatusByIDAsync(int orderStatusID)
        {
            OrderStatus orderStatus = new OrderStatus();

            try
            {
                var uri = new Uri(string.Format(Constants.OrderStatusesUrl, orderStatusID));

                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    orderStatus = JsonConvert.DeserializeObject<OrderStatus>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return orderStatus;
        }


    }

    class ServiceRequestRestService
    {
        HttpClient _client;

        public List<ServiceRequest> ServiceRequests { get; private set; }

        public ServiceRequestRestService()
        {
            _client = new HttpClient();
        }

        public async Task<List<ServiceRequest>> RefreshDataAsync()
        {
            ServiceRequests = new List<ServiceRequest>();

            var uri = new Uri(string.Format(Constants.ServiceRequestsUrl, string.Empty));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ServiceRequests = JsonConvert.DeserializeObject<List<ServiceRequest>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return ServiceRequests;
        }

        public async Task<List<ServiceRequest>> GetServiceRequestsByCarePartnerIDAsync(int carePartnerID)
        {
            ServiceRequests = new List<ServiceRequest>();

            try
            {
                var uri = new Uri(string.Format(Constants.ServiceRequestsByCarePartnerIdUrl, carePartnerID));

                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ServiceRequests = JsonConvert.DeserializeObject<List<ServiceRequest>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return ServiceRequests;
        }

        public async Task SaveServiceRequestAsync(ServiceRequest item, bool isNewItem = false)
        {
            var uri = new Uri(string.Format(Constants.ServiceRequestsUrl, string.Empty));

            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    response = await _client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tService Request successfully saved.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task DeleteServiceRequestAsync(string id)
        {
            var uri = new Uri(string.Format(Constants.ServiceRequestsUrl, id));

            try
            {
                var response = await _client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tService Request successfully deleted.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
    }
}