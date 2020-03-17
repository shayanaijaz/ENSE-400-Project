using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Xamarin.Forms;
using CareOnDemand.Views.CustomerViews;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CareOnDemand.Data;
using CareOnDemand.Models;

namespace CareOnDemand.ViewModels
{
    public class OrderDetailsViewModel : BaseServiceAndOrderViewModel
    {
        public OrderDetailsViewModel()
        {
            OrderServicesList = new ObservableCollection<string>();
            PopulateOrderServicesList();
            PopulateLists();
            ContinueOrderCommand = new Command(async () => await ContinueOrderClicked());
        }

        
        public List<Address> AddressList { get; set; }

        public Address SelectedAddress
        {
            get => user_address;
            set
            {
                user_address = value;
            }
        }

        public DateTime SelectedDate
        {
            get => selected_date;
            set
            {
                selected_date = value.Date;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }
        public TimeSpan SelectedTime
        {
            get => selected_time;
            set
            {
                selected_time = value;
                OnPropertyChanged(nameof(SelectedTime));
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        public List<Account> RecipientList { get; set; }
        
        public Account SelectedRecipient
        {
            get => recipient;
            set
            {
                recipient = value;
            }
        }
        public List<Account> CarePartnerList { get; set; }
        public Account SelectedCarePartner
        {
            get => care_partner;
            set
            {
                care_partner = value;
            }
        }
        

        public Command ContinueOrderCommand { get; set; }

        async Task ContinueOrderClicked()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ServiceReview());
        }



        async void PopulateLists()
        {
            var address_result = await GetAddressList();
            AddressList = address_result.ToList();
            OnPropertyChanged(nameof(AddressList));

            var account_result = await GetRecipientList();
            RecipientList = account_result.ToList();
            OnPropertyChanged(nameof(RecipientList));

            var care_partner_result = await GetCarePartnerList();
            CarePartnerList = care_partner_result.ToList();
            OnPropertyChanged(nameof(CarePartnerList));
        }
        public async Task<List<Address>> GetAddressList()
        {
            int customerID = Application.Current.Properties.ContainsKey("customerID") ? Convert.ToInt32(Application.Current.Properties["customerID"]) : 0;

            AddressRestService addressRestService = new AddressRestService();
            Customer_AddressRestService customer_AddressRestService = new Customer_AddressRestService();

            List<Customer_Address> customer_addresses = await customer_AddressRestService.GetCustomerAddressesByCustomerIDAsync(customerID);


            List<Address> address_list = new List<Address>();

            foreach (var customer_address in customer_addresses)
            {
                var address = await addressRestService.GetAddressByIDAsync(customer_address.AddressID);
                address_list.Add(address);
            }

            return address_list;
        }

        public async Task<List<Account>> GetRecipientList()
        {
            int accountID = Application.Current.Properties.ContainsKey("accountID") ? Convert.ToInt32(Application.Current.Properties["accountID"]) : 0;

            AccountRestService accountRestService = new AccountRestService();
            var retrieved_account = await accountRestService.GetAccountByIDAsync(accountID);

            retrieved_account.FullName = retrieved_account.FirstName.Trim() + " " + retrieved_account.LastName.Trim();

            List<Account> account_list = new List<Account>();
            account_list.Add(retrieved_account);

            return account_list;
        }

        public async Task<List<Account>> GetCarePartnerList()
        {
            CarePartnerRestService carePartnerRestService = new CarePartnerRestService();
            AccountRestService accountRestService = new AccountRestService();
            var retrieved_care_partners = await carePartnerRestService.RefreshDataAsync();

            List<Account> care_partner_list = new List<Account>();

            foreach (var care_partner in retrieved_care_partners)
            {
                int care_partner_account_id = care_partner.AccountID;
                var account = await accountRestService.GetAccountByIDAsync(care_partner_account_id);
                account.FullName = account.FirstName.Trim() + " " + account.LastName.Trim();
                care_partner_list.Add(account);
            }

            return care_partner_list;
        }
    }
}
