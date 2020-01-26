using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using CareOnDemand.Views.CustomerViews;
using System.Windows.Input;
using System.Threading.Tasks;
using CareOnDemand.Models;

namespace CareOnDemand.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        public LoginViewModel()
        {
            GoToRegisterPageCommand = new Command(async () => await RegisterButtonClicked());
            LoginCommand = new Command(Login);
        }

        private String email;

        public String Email
        {
            get { return email; }
            set
            {
                email = value.Trim().ToLower();
            }
        }
        public String Password { get; set; }

        public Command GoToRegisterPageCommand { private set; get; }
        public Command LoginCommand { private set; get; }

        async Task RegisterButtonClicked()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }

        public void Login()
        {
            LoginModel loginModel = new LoginModel(email, Password);
            loginModel.Login();
        }
    }
}
