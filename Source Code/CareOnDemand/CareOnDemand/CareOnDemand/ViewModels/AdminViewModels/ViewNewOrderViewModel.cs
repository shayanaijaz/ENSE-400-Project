/*
    Care on Demand Application
    Capstone 2020 - ENSE 400/477
    The Ni(c)(k)S

    Author: Shayan Khan
    Last Modified: Apr. 10, 2020
*/
using CareOnDemand.Data;
using CareOnDemand.Models;
using CareOnDemand.Views.AdminViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CareOnDemand.ViewModels.AdminViewModels
{
    /* This class contains bindings and functions relating to elements on the ViewNewOrder page. It inherits variables, objects and 
     * functions from the BaseAdminOrdersViewModel class.
     */
    public class ViewNewOrderViewModel : BaseAdminOrdersViewModel
    {
        // Constructor that initializes the bindings and commands and runs functions to populate the bindings
        public ViewNewOrderViewModel()
        {
            Task.Run(async () => await GetOrderDetailsFromDb(admin_selected_order));
            Task.Run(async () => await GetCarePartnerData());
            AssignOrderCommand = new Command(async () => await CreateServiceRequest());
            OrderServicesList = new ObservableCollection<Order_Service>();
            ElementVisible = false;
            ActivityIndicatorVisible = true;
            ActivityIndicatorRunning = true;
        }

        // Bindings and commands on this page
        public ObservableCollection<CarePartnerDetails> CarePartnerAccounts { get; set; }
        public Command AssignOrderCommand { get; set; }

        public CarePartnerDetails SelectedCarePartner { get; set; }

        /* This Task gets a list of the care partners to display in the Picker
         */ 
        async Task GetCarePartnerData()
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

        /* This Task creates a new service request for a care partner. It uses the REST services to assign the service request to a care partner 
         * and also updates the status of the order in the database
         */ 
        async Task CreateServiceRequest()
        {
            ServiceRequestRestService serviceRequestRestService = new ServiceRequestRestService();

            ServiceRequest serviceRequest = new ServiceRequest { CarePartnerID = SelectedCarePartner.CarePartner.CarePartnerID, 
            OrderID = admin_selected_order.OrderID, OrderNotes = CarePartnerNotes};

            List<OrderStatus> order_status_list = await new OrderStatusRestService().RefreshDataAsync();

            // get the order status ID for In Progress order
            foreach(var status in order_status_list)
            {
                if (status.Status.Trim() == "In Progress")
                    admin_selected_order.OrderStatusID = status.OrderStatusID;
            }

            try
            {
                await serviceRequestRestService.SaveServiceRequestAsync(serviceRequest, true);
                await new OrderRestService().SaveOrderAsync(admin_selected_order, false);   // Update the order in db to reflect new status
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
