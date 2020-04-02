/*
    Care on Demand Application
    Capstone 2020 - ENSE 400/477
    The Ni(c)(k)S

    Author: Nicolas AChter
    Contributor(s):
    Last Modified: Mar. 30, 2020
*/

using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using CareOnDemand.Views.CustomerViews;
using System.Windows.Input;
using System.Threading.Tasks;
using CareOnDemand.Models;
using CareOnDemand.Data;
using CareOnDemand.Views.SharedViews;
using System.Linq;

namespace CareOnDemand.ViewModels
{
    class AccountManagementViewModel : BaseViewModel
    {
        public AccountManagementViewModel()
        {
            GoToRegisterAddressCommand = new Command(async () => await RegisterAddressButtonClicked()); //add an address button
            LogOutCommand = new Command(async () => await LogOut());                                    //log out button
            GoToChangePassCommand = new Command(async () => await ChangePassButtonClicked());           //change password button
        }

        //commands
        public Command GoToRegisterAddressCommand { private set; get; }
        public Command LogOutCommand { private set; get; }
        public Command GoToChangePassCommand { private set; get; }

        //tasks
        async Task RegisterAddressButtonClicked()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterAddressPage());
        }
        async Task ChangePassButtonClicked()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ChangePassPage());
        }

        async Task LogOut()
        {
            //remove accountID and set login bool to false
            Application.Current.Properties["isLoggedIn"] = Boolean.FalseString;
            Application.Current.Properties["accountID"] = null;

            //redirect to login page
            Application.Current.MainPage.Navigation.InsertPageBefore(new LoginPage(), Application.Current.MainPage.Navigation.NavigationStack[0]);
            await Application.Current.MainPage.Navigation.PopToRootAsync();
        }
    }
}
