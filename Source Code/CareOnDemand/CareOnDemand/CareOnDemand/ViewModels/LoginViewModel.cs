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
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }
        async Task ForgotButtonClicked()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ForgotPassPage());
        }
        async void Login()
        {
            LoginService loginModel = new LoginService(Email, Password);

            try
            {
                await loginModel.Login();

                Application.Current.Properties["isLoggedIn"] = Boolean.TrueString;

                await Application.Current.MainPage.Navigation.PushAsync(new CustomerNavBar());

            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }

        }


    }
}
