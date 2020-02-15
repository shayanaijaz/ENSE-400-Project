/*
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
    class ServiceRestService
    {
        HttpClient _client;

        public List<Service> Services { get; private set; }

        public ServiceRestService()
        {
            _client = new HttpClient();
        }

        public async Task<List<Service>> RefreshDataAsync()
        {
            Services = new List<Service>();

          //  var uri = new Uri(string.Format(Constants.ServicesUrl, string.Empty));
            try
            {
             //   var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Services = JsonConvert.DeserializeObject<List<Service>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Services;
        }

        public async Task SaveServiceAsync(Service item, bool isNewItem = false)
        {
            var uri = new Uri(string.Format(Constants.ServicesUrl, string.Empty));

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
                    Debug.WriteLine(@"\tService successfully saved.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task DeleteServiceAsync(string id)
        {
          //  var uri = new Uri(string.Format(Constants.ServicesUrl, id));

            try
            {
                var response = await _client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tService successfully deleted.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
    }

    class ServiceTypeRestService
    {
        HttpClient _client;

        public List<ServiceType> ServiceTypes { get; private set; }

        public ServiceTypeRestService()
        {
            _client = new HttpClient();
        }

        public async Task<List<ServiceType>> RefreshDataAsync()
        {
            ServiceTypes = new List<ServiceType>();

          //  var uri = new Uri(string.Format(Constants.ServiceTypesUrl, string.Empty));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ServiceTypes = JsonConvert.DeserializeObject<List<ServiceType>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return ServiceTypes;
        }
    }
}*/