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
    class PaymentMethodRestService
    {
        HttpClient _client;

        public List<PaymentMethod> PaymentMethods { get; private set; }

        public PaymentMethodRestService()
        {
            _client = new HttpClient();
        }

        public async Task<List<PaymentMethod>> RefreshDataAsync()
        {
            PaymentMethods = new List<PaymentMethod>();

            var uri = new Uri(string.Format(Constants.PaymentMethodsUrl, string.Empty));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    PaymentMethods = JsonConvert.DeserializeObject<List<PaymentMethod>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return PaymentMethods;
        }

        public async Task SavePaymentMethodAsync(PaymentMethod item, bool isNewItem = false)
        {
            var uri = new Uri(string.Format(Constants.PaymentMethodsUrl, string.Empty));

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
                    Debug.WriteLine(@"\tPayment Method successfully saved.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task DeletePaymentMethodAsync(string id)
        {
            var uri = new Uri(string.Format(Constants.PaymentMethodsUrl, id));

            try
            {
                var response = await _client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Payment Method successfully deleted.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
    }
}