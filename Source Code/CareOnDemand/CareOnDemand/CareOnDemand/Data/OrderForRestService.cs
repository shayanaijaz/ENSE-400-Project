//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;
//using CareOnDemand.Helpers;
//using CareOnDemand.Models;
//using Newtonsoft.Json;

//namespace CareOnDemand.Data
//{
//    class OrderForRestService
//    {
//        HttpClient _client;

//        public List<OrderFor> OrderFors { get; private set; }

//        public OrderForRestService()
//        {
//            _client = new HttpClient();
//        }

//        public async Task<List<OrderFor>> RefreshDataAsync()
//        {
//            OrderFors = new List<OrderFor>();

//            var uri = new Uri(string.Format(Constants.OrderForsUrl, string.Empty));
//            try
//            {
//                var response = await _client.GetAsync(uri);
//                if (response.IsSuccessStatusCode)
//                {
//                    var content = await response.Content.ReadAsStringAsync();
//                    OrderFors = JsonConvert.DeserializeObject<List<OrderFor>>(content);
//                }
//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine(@"\tERROR {0}", ex.Message);
//            }

//            return OrderFors;
//        }

//        public async Task SaveOrderForAsync(OrderFor item, bool isNewItem = false)
//        {
//            var uri = new Uri(string.Format(Constants.OrderForsUrl, string.Empty));

//            try
//            {
//                var json = JsonConvert.SerializeObject(item);
//                var content = new StringContent(json, Encoding.UTF8, "application/json");

//                HttpResponseMessage response = null;
//                if (isNewItem)
//                {
//                    response = await _client.PostAsync(uri, content);
//                }
//                else
//                {
//                    response = await _client.PutAsync(uri, content);
//                }

//                if (response.IsSuccessStatusCode)
//                {
//                    Debug.WriteLine(@"\tOrderFor successfully saved.");
//                }

//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine(@"\tERROR {0}", ex.Message);
//            }
//        }

//        public async Task DeleteOrderForAsync(string id)
//        {
//            var uri = new Uri(string.Format(Constants.OrderForsUrl, id));

//            try
//            {
//                var response = await _client.DeleteAsync(uri);

//                if (response.IsSuccessStatusCode)
//                {
//                    Debug.WriteLine(@"\tOrderFor successfully deleted.");
//                }

//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine(@"\tERROR {0}", ex.Message);
//            }
//        }
//    }
//}