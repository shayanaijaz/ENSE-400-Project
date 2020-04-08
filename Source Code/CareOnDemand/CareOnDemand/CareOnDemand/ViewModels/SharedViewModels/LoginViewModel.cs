/*
    Care on Demand Application
    Capstone 2020 - ENSE 400/477
    The Ni(c)(k)S

    Author: Shayan Khan
    Contributor(s): Nicolas Achter
    Last Modified: Apr. 06, 2020
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
    public class LoginViewModel : BaseCustomerDetailsViewModel
    {

        public LoginViewModel()
        {
            GoToRegisterPageCommand = new Command(async () => await RegisterButtonClicked());
            LoginCommand = new Command(Login);
            GoToForgotCommand = new Command(async () => await ForgotButtonClicked());
        }

        public Command GoToRegisterPageCommand { private set; get; }
        public Command LoginCommand { private set; get; }

        public Command GoToForgotCommand { private set; get; }

        async Task RegisterButtonClicked()
        {
            Application.Current.Properties["accountID"] = null; //init accountID to null as user is not yet logged in

            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }
        async Task ForgotButtonClicked()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ForgotPassPage());
        }
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

                Application.Current.Properties["isLoggedIn"] = Boolean.TrueString;
                Application.Current.Properties["accountLevelID"] = account_level_id;
                Application.Current.Properties["accountID"] = account_id;


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
