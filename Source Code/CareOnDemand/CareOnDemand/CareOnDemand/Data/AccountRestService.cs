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
    class AccountRestService
    {
        HttpClient _client;

        public List<Account> Accounts { get; private set; }

        public AccountRestService()
        {
            _client = new HttpClient();
        }

        public async Task<List<Account>> RefreshDataAsync()
        {
            Accounts = new List<Account>();

            var uri = new Uri(string.Format(Constants.AccountsUrl, string.Empty));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Accounts = JsonConvert.DeserializeObject<List<Account>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Accounts;
        }

        public async Task<List<Account>> GetAccountByEmailAsync(string email)
        {
            Accounts = new List<Account>();

            try
            {
                var uri = new Uri(string.Format(Constants.AccountsEmailUrl, email));

                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Accounts = JsonConvert.DeserializeObject<List<Account>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Accounts;
        }


        public async Task<Account> SaveAccountAsync(Account item, bool isNewItem = false)
        {
            var uri = new Uri(string.Format(Constants.AccountsUrl, string.Empty));

            Account Accounts = new Account();

            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await _client.PostAsync(uri, content);
                    var response_content = await response.Content.ReadAsStringAsync();
                    Accounts = JsonConvert.DeserializeObject<Account>(response_content);
                }
                else
                {
                    response = await _client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tAccount successfully saved.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Accounts;
        }

        public async Task DeleteAccountAsync(string id)
        {
            var uri = new Uri(string.Format(Constants.AccountsUrl, id));

            try
            {
                var response = await _client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tAccount successfully deleted.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
    }

    class AccountLevelRestService
    {
        HttpClient _client;

        public List<AccountLevel> AccountLevels { get; private set; }

        public AccountLevelRestService()
        {
            _client = new HttpClient();
        }

        public async Task<List<AccountLevel>> RefreshDataAsync()
        {
            AccountLevels = new List<AccountLevel>();

            var uri = new Uri(string.Format(Constants.AccountLevelsUrl, string.Empty));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    AccountLevels = JsonConvert.DeserializeObject<List<AccountLevel>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return AccountLevels;
        }
    }

    class AdminRestService
    {
        HttpClient _client;

        public List<Admin> Admins { get; private set; }

        public AdminRestService()
        {
            _client = new HttpClient();
        }

        public async Task<List<Admin>> RefreshDataAsync()
        {
            Admins = new List<Admin>();

            var uri = new Uri(string.Format(Constants.AdminsUrl, string.Empty));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Admins = JsonConvert.DeserializeObject<List<Admin>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Admins;
        }

        public async Task SaveAdminAsync(Admin item, bool isNewItem = false)
        {
            var uri = new Uri(string.Format(Constants.AdminsUrl, string.Empty));

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
                    Debug.WriteLine(@"\tAdmin successfully saved.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task DeleteAdminAsync(string id)
        {
            var uri = new Uri(string.Format(Constants.AdminsUrl, id));

            try
            {
                var response = await _client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tAdmin successfully deleted.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
    }

    class CarePartnerRestService
    {
        HttpClient _client;

        public List<CarePartner> CarePartners { get; private set; }

        public CarePartnerRestService()
        {
            _client = new HttpClient();
        }

        public async Task<List<CarePartner>> RefreshDataAsync()
        {
            CarePartners = new List<CarePartner>();

            var uri = new Uri(string.Format(Constants.CarePartnersUrl, string.Empty));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    CarePartners = JsonConvert.DeserializeObject<List<CarePartner>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return CarePartners;
        }

        public async Task SaveCarePartnerAsync(CarePartner item, bool isNewItem = false)
        {
            var uri = new Uri(string.Format(Constants.CarePartnersUrl, string.Empty));

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
                    Debug.WriteLine(@"\tCare Partner successfully saved.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task DeleteCarePartnerAsync(string id)
        {
            var uri = new Uri(string.Format(Constants.CarePartnersUrl, id));

            try
            {
                var response = await _client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tCare Partner successfully deleted.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
    }

    class CustomerRestService
    {
        HttpClient _client;

        public List<Customer> Customers { get; private set; }

        public CustomerRestService()
        {
            _client = new HttpClient();
        }

        public async Task<List<Customer>> RefreshDataAsync()
        {
            Customers = new List<Customer>();

            var uri = new Uri(string.Format(Constants.CustomersUrl, string.Empty));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Customers = JsonConvert.DeserializeObject<List<Customer>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Customers;
        }

        public async Task SaveCustomerAsync(Customer item, bool isNewItem = false)
        {
            var uri = new Uri(string.Format(Constants.CustomersUrl, string.Empty));

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
                    Debug.WriteLine(@"\tCustomer successfully saved.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task DeleteCustomerAsync(string id)
        {
            var uri = new Uri(string.Format(Constants.CustomersUrl, id));

            try
            {
                var response = await _client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tCustomer successfully deleted.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
    }
}
