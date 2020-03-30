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


namespace CareOnDemand.ViewModels
{
    class AccountManagementViewModel : BaseViewModel
    {
        public AccountManagementViewModel()
        {
            GoToRegisterAddressCommand = new Command(async () => await RegisterAddressButtonClicked());
            LogOutCommand = new Command(async () => await LogOut());
            GoToChangePassCommand = new Command(async () => await ChangePassButtonClicked());
        }

        public Command GoToRegisterAddressCommand { private set; get; }
        public Command LogOutCommand { private set; get; }
        public Command GoToChangePassCommand { private set; get; }

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
            Application.Current.Properties["isLoggedIn"] = Boolean.FalseString;
            Application.Current.Properties["accountLevelID"] = null;
            Application.Current.Properties["accountID"] = null;
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }
    }
}
