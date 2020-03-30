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
    public class ForgotPassViewModel    : BaseCustomerDetailsViewModel
    {
        public ForgotPassViewModel()
        {
            ForgotPasswordCommand = new Command(ForgotPassword);
        }

        public Command ForgotPasswordCommand { private set; get; }

        async void ForgotPassword()
        {
            try
            {
                ForgotPasswordService forgotPasswordService = new ForgotPasswordService(Email);

                await forgotPasswordService.ForgotPassowrd();

                await Application.Current.MainPage.DisplayAlert("Success", "Password reset request successful. Please check your email for verification code.", "OK");

                await Application.Current.MainPage.Navigation.PushAsync(new ConfirmForgotPasswordPage());
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
        }
    }

}
