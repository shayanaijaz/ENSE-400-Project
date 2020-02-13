using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using CareOnDemand.Views.CustomerViews;
using System.Windows.Input;
using System.Threading.Tasks;
using CareOnDemand.Models;
using CareOnDemand.Data;

namespace CareOnDemand.ViewModels
{
    public class ForgotPassViewModel    : BaseViewModel
    {
        private String email;
        public ForgotPassViewModel()
        {
            ForgotPasswordCommand = new Command(ForgotPassword);
        }

        public String Email
        {
            get => email;
            set
            {
                email = value;
            }
        }

        public Command ForgotPasswordCommand { private set; get; }

        async void ForgotPassword()
        {
            ForgotPasswordService forgotPasswordService = new ForgotPasswordService(email);

            await forgotPasswordService.ForgotPassowrd();

            await Application.Current.MainPage.DisplayAlert("Success", "Password reset request successful. Please check your email for verification code.", "OK");
        }
    }

}
