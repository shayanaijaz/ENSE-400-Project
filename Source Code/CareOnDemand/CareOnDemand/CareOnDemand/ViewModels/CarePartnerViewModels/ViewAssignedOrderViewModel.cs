/*
    Care on Demand Application
    Capstone 2020 - ENSE 400/477
    The Ni(c)(k)S

    Author: Shayan Khan
    Last Modified: Apr. 10, 2020
*/
using CareOnDemand.Data;
using CareOnDemand.Models;
using CareOnDemand.Views.CarePartnerViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CareOnDemand.ViewModels.CarePartnerViewModels
{
    /* This class contains bindings and functions relating to elements on the ViewAssignedOrder page. It inherits variables, objects and 
     * functions from the BaseCarePartnerOrdersViewModel class.
     */
    public class ViewAssignedOrderViewModel : BaseCarePartnerOrdersViewModel
    {
        // Constructor that initializes the bindings and commands
        public ViewAssignedOrderViewModel()
        {
            Task.Run(async () => await GetOrderDetailsFromDb(care_partner_selected_order));
            OrderServicesList = new ObservableCollection<Order_Service>();
            ElementVisible = false;
            ActivityIndicatorVisible = true;
            ActivityIndicatorRunning = true;
            CompleteOrderVisible = false;
            StartOrderVisible = false;
            OpenMapCommand = new Command(async () => await OpenMap());
            StartOrderCommand = new Command(async () => await StartOrder());
            CompleteOrderCommand = new Command(async () => await CompleteOrder());
        }

        // Commands on this page
        public Command OpenMapCommand { private set; get; }
        public Command StartOrderCommand { private set; get; }
        public Command CompleteOrderCommand { private set; get; }
        
        // This task opens the device's native map application and populates the map with the user address
        async Task OpenMap()
        {
            Address user_address = await new AddressRestService().GetAddressByIDAsync(care_partner_selected_order.AddressID);

            var placemark = new Placemark
            {
                CountryName = "Canada",
                AdminArea  = user_address.Province.Trim(),
                Locality = user_address.City.Trim(),
                Thoroughfare = user_address.AddrLine1.Trim()
            };

            var options = new MapLaunchOptions {  };

            await Map.OpenAsync(placemark, options);
        }

        // This task starts the order. It uses the REST service to change the status of the order and displays a success message
        async Task StartOrder()
        {
            List<OrderStatus> order_status_list = await new OrderStatusRestService().RefreshDataAsync();

            foreach (var status in order_status_list)
            {
                if (status.Status.Trim() == "On The Way")
                    care_partner_selected_order.OrderStatusID = status.OrderStatusID;
            }

            try
            {
                await new OrderRestService().SaveOrderAsync(care_partner_selected_order, false);   // Update the order in db to reflect new status
                await Application.Current.MainPage.DisplayAlert("Success", "Order Started", "OK");
                await Application.Current.MainPage.Navigation.PushAsync(new ViewAssignedOrder());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // This task completes the order. It uses the REST service to change the status of the order and displays a success message
        async Task CompleteOrder()
        {
            bool answer = await Application.Current.MainPage.DisplayAlert("Confirm", "Are you sure you want to mark order as completed?", "Yes", "No");

            if (answer == true)
            {
                List<OrderStatus> order_status_list = await new OrderStatusRestService().RefreshDataAsync();

                foreach (var status in order_status_list)
                {
                    if (status.Status.Trim() == "Completed")
                        care_partner_selected_order.OrderStatusID = status.OrderStatusID;
                }

                try
                {
                    await new OrderRestService().SaveOrderAsync(care_partner_selected_order, false);   // Update the order in db to reflect new status
                    await Application.Current.MainPage.DisplayAlert("Success", "Order marked as completed", "OK");
                    await Application.Current.MainPage.Navigation.PushAsync(new CarePartnerNavBar());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
    }
}
