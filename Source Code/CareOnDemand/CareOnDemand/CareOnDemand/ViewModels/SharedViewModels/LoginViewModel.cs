/*
    Care on Demand Application
    Capstone 2020 - ENSE 400/477
    The Ni(c)(k)S

    Author: Shayan Khan
    Contributor(s): Nicolas Achter
    Last Modified: Apr. 07, 2020
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using CareOnDemand.Views.CustomerViews;
using CareOnDemand.Views.SharedViews;
using System.Windows.Input;
using System.Threading.Tasks;
using CareOnDemand.Models;
using CareOnDemand.Views.AdminViews;
using CareOnDemand.Views.CarePartnerViews;
using CareOnDemand.Data;

namespace CareOnDemand.ViewModels
{
    /*
     * This class provides services related to elements located on the login page. It inherits from the 
     * BaseCustomerDetailsViewModel class
     */
    public class LoginViewModel : BaseCustomerDetailsViewModel
    {
        // Constructor that initializes the bindings
        public LoginViewModel()
        {
            GoToRegisterPageCommand = new Command(async () => await RegisterButtonClicked());
            LoginCommand = new Command(Login);
            GoToForgotCommand = new Command(async () => await ForgotButtonClicked());
        }

        // Bindings for elements on the login page
        public Command GoToRegisterPageCommand { private set; get; }
        public Command LoginCommand { private set; get; }
        public Command GoToForgotCommand { private set; get; }

        // Function that runs on Register button click. Redirects user to the Register page
        async Task RegisterButtonClicked()
        {
            Application.Current.Properties["accountID"] = null; //init accountID to null as user is not yet logged in

            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }

        // Function that redirects user to Forgot Password page
        async Task ForgotButtonClicked()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ForgotPassPage());
        }

        /*
         * Function that runs when user clicks login. Uses the LoginService class to login a user and get their 
         * information from the database. Depending on the type of user that logs in, it redirects them to the 
         * appropriate page and also saves some information in persistent storage for future use.
         */
        async void Login()
        {
            LoginService loginService = new LoginService(Email, Password);
            CustomerRestService customerRestService = new CustomerRestService();

            try
            {
                await loginService.Login();
                Account retrieved_user = await loginService.GetUserFromDatabase();

                int account_level_id = retrieved_user.AccountLevelID;
                int account_id = retrieved_user.AccountID;

                // Save information in persistent storage
                Application.Current.Properties["isLoggedIn"] = Boolean.TrueString;
                Application.Current.Properties["accountLevelID"] = account_level_id;
                Application.Current.Properties["accountID"] = account_id;


                // If statement depending on the type of user that logged in (Admin, Care Partner, or Customer)
                if (account_level_id == 1)
                {
                    Application.Current.MainPage.Navigation.InsertPageBefore(new AdminNavBar(), Application.Current.MainPage.Navigation.NavigationStack[0]);
                    await Application.Current.MainPage.Navigation.PopToRootAsync();
                }
                else if (account_level_id == 2)
                {
                    CarePartner carePartner = await new CarePartnerRestService().GetCarePartnerByAccountIDAsync(account_id);
                    Application.Current.Properties["carePartnerID"] = carePartner.CarePartnerID;
                    Application.Current.MainPage.Navigation.InsertPageBefore(new CarePartnerNavBar(), Application.Current.MainPage.Navigation.NavigationStack[0]);
                    await Application.Current.MainPage.Navigation.PopToRootAsync();
                }
                else if (account_level_id == 3) // if the user is a customer
                {
                    var retrieved_customer = await customerRestService.GetCustomerByAccountIDAsync(account_id);
                    int customer_id = retrieved_customer[0].CustomerID;
                    Application.Current.Properties["customerID"] = customer_id; // Store CustomerID in persistent storage
                    Application.Current.MainPage.Navigation.InsertPageBefore(new CustomerNavBar(), Application.Current.MainPage.Navigation.NavigationStack[0]);
                    await Application.Current.MainPage.Navigation.PopToRootAsync();
                }

            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }

        }


    }
}
