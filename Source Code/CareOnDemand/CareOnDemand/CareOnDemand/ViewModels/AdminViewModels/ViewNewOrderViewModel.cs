using CareOnDemand.Data;
using CareOnDemand.Models;
using CareOnDemand.Views.AdminViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CareOnDemand.ViewModels.AdminViewModels
{
    public class ViewNewOrderViewModel : BaseAdminOrdersViewModel
    {
        public ViewNewOrderViewModel()
        {
            GetOrderData(admin_selected_order);
            GetCarePartnerData();
            AssignOrderCommand = new Command(async () => await CreateServiceRequest());
        }

        public string Location { get; set; }
        public string DateString { get; set; }
        public string TimeString { get; set; }
        public string Recipient { get; set; }
        public string FinalPrice { get; set; }
        public string AdditionalInstructions { get; set; }
        public string CarePartnerNotes { get; set; }
        public ObservableCollection<Order_Service> OrderServicesList {get; set;}
        public ObservableCollection<CarePartnerDetails> CarePartnerAccounts { get; set; }
        public Command AssignOrderCommand { get; set; }

        public CarePartnerDetails SelectedCarePartner { get; set; }

        async void GetOrderData(Order order)
        {
            Customer customer = await new CustomerRestService().GetCustomerByIDAsync(order.CustomerID);
            Account account = await new AccountRestService().GetAccountByIDAsync(customer.AccountID);

            List<Order_Service> order_services = await new Order_ServiceRestService().GetOrderServiceByID(order.OrderID);

            List<Service> order_service_list = new List<Service>();

            string servicesString = "";

            double finalPrice = 0;

            foreach (var service in order_services)
            {
                var service_details = await new ServiceRestService().GetServiceByIDAsync(service.ServiceID);
                service.ServiceName = service_details.ServiceName;
                order_service_list.Add(service_details);
                servicesString += string.Format("\u2022 " + service_details.ServiceName.Trim() + " - " + service.RequestedLength + " hours " + "{0}", Environment.NewLine);
                finalPrice += service.RequestedLength * service_details.ServicePrice;
            }

            Address user_address = await new AddressRestService().GetAddressByIDAsync(order.AddressID);

            ObservableCollection<Order_Service> orderServiceCollection = new ObservableCollection<Order_Service>(order_services as List<Order_Service>);
            OrderServicesList = orderServiceCollection;
            OnPropertyChanged(nameof(OrderServicesList));

            Location = user_address.AddrLine1.Trim() + ", " + user_address.City.Trim() + ", " + user_address.Province.Trim() + ", " + user_address.PostalCode.Trim();
            OnPropertyChanged(nameof(Location));

            DateString = order.RequestedTime.Date.ToString("yyyy-MM-dd");

            TimeString = new DateTime(order.RequestedTime.Ticks).ToString("h:mm tt");

            OnPropertyChanged(nameof(DateString));
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

        async void GetCarePartnerData()
        {
            ObservableCollection<CarePartnerDetails> carePartnerDetails = new ObservableCollection<CarePartnerDetails>();

            List<CarePartner> carePartnerList = await new CarePartnerRestService().RefreshDataAsync();

            ObservableCollection<Account> carePartnerAccount = new ObservableCollection<Account>();

            foreach (var care_partner in carePartnerList)
            {
                var account = await new AccountRestService().GetAccountByIDAsync(care_partner.AccountID);
                account.FullName = account.FirstName.Trim() + " " + account.LastName.Trim();
                carePartnerAccount.Add(account);

                carePartnerDetails.Add(new CarePartnerDetails { CarePartner = care_partner, CarePartnerAccount = account });

            }


            CarePartnerAccounts = carePartnerDetails;
            OnPropertyChanged(nameof(CarePartnerAccounts));

        }

        async Task CreateServiceRequest()
        {
            ServiceRequestRestService serviceRequestRestService = new ServiceRequestRestService();

            ServiceRequest serviceRequest = new ServiceRequest { CarePartnerID = SelectedCarePartner.CarePartner.CarePartnerID, 
            OrderID = admin_selected_order.OrderID, OrderNotes = CarePartnerNotes};

            try
            {
                await serviceRequestRestService.SaveServiceRequestAsync(serviceRequest, true);

                await Application.Current.MainPage.DisplayAlert("Success", "Request assigned successfully", "OK");
                await Application.Current.MainPage.Navigation.PushAsync(new AdminNavBar());

            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public class CarePartnerDetails
        {
            public Account CarePartnerAccount { get; set; }
            public CarePartner CarePartner { get; set; }
        }


    }
}
