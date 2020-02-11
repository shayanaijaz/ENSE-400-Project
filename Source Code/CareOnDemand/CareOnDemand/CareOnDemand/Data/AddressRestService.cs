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
//    class AddressRestService
//    {
//        HttpClient _client;

//        public List<Address> Addresses { get; private set; }

//        public AddressRestService()
//        {
//            _client = new HttpClient();
//        }

//        public async Task<List<Address>> RefreshDataAsync()
//        {
//            Addresses = new List<Address>();

//            var uri = new Uri(string.Format(Constants.AddressesUrl, string.Empty));
//            try
//            {
//                var response = await _client.GetAsync(uri);
//                if (response.IsSuccessStatusCode)
//                {
//                    var content = await response.Content.ReadAsStringAsync();
//                    Addresses = JsonConvert.DeserializeObject<List<Address>>(content);
//                }
//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine(@"\tERROR {0}", ex.Message);
//            }

//            return Addresses;
//        }

//        public async Task SaveAddressAsync(Address item, bool isNewItem = false)
//        {
//            var uri = new Uri(string.Format(Constants.AddressesUrl, string.Empty));

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
//                    Debug.WriteLine(@"\tAddress successfully saved.");
//                }

//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine(@"\tERROR {0}", ex.Message);
//            }
//        }

//        public async Task DeleteAddressAsync(string id)
//        {
//            var uri = new Uri(string.Format(Constants.AddressesUrl, id));

//            try
//            {
//                var response = await _client.DeleteAsync(uri);

//                if (response.IsSuccessStatusCode)
//                {
//                    Debug.WriteLine(@"\tAddress successfully deleted.");
//                }

//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine(@"\tERROR {0}", ex.Message);
//            }
//        }
//    }

//    class Customer_AddressRestService
//    {
//        HttpClient _client;

//        public List<Customer_Address> Customer_Addresses { get; private set; }

//        public Customer_AddressRestService()
//        {
//            _client = new HttpClient();
//        }

//        public async Task<List<Customer_Address>> RefreshDataAsync()
//        {
//            Customer_Addresses = new List<Customer_Address>();

//            var uri = new Uri(string.Format(Constants.Customer_AddressesUrl, string.Empty));
//            try
//            {
//                var response = await _client.GetAsync(uri);
//                if (response.IsSuccessStatusCode)
//                {
//                    var content = await response.Content.ReadAsStringAsync();
//                    Customer_Addresses = JsonConvert.DeserializeObject<List<Customer_Address>>(content);
//                }
//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine(@"\tERROR {0}", ex.Message);
//            }

//            return Customer_Addresses;
//        }

//        public async Task SaveCustomer_AddressAsync(Customer_Address item, bool isNewItem = false)
//        {
//            var uri = new Uri(string.Format(Constants.Customer_AddressesUrl, string.Empty));

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
//                    Debug.WriteLine(@"\tCustomer_Address successfully saved.");
//                }

//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine(@"\tERROR {0}", ex.Message);
//            }
//        }

//        public async Task DeleteCustomer_AddressAsync(string id)
//        {
//            var uri = new Uri(string.Format(Constants.Customer_AddressesUrl, id));

//            try
//            {
//                var response = await _client.DeleteAsync(uri);

//                if (response.IsSuccessStatusCode)
//                {
//                    Debug.WriteLine(@"\Customer_Address successfully deleted.");
//                }

//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine(@"\tERROR {0}", ex.Message);
//            }
//        }
//    }
//}
