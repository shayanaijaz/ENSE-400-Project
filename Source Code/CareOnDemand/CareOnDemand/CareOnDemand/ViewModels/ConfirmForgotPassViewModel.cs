using CareOnDemand.Data;
using CareOnDemand.Views.SharedViews;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CareOnDemand.ViewModels
{
    public class ConfirmForgotPassViewModel : BaseCustomerDetailsViewModel
    {
        private String verificationCode;

        public ConfirmForgotPassViewModel()
        {
            ConfirmForgotPassCommand = new Command(ConfirmForgotPassword);
        }

        public Command ConfirmForgotPassCommand { private set; get; }

        public String VerificationCode
        {
            get => verificationCode;
            set
            {
                verificationCode = value;
            }
        }

        async void ConfirmForgotPassword()
        {
            try
            {
                ForgotPasswordService forgotPasswordService = new ForgotPasswordService(Email);

                await forgotPasswordService.ForgotPasswordConfirmation(VerificationCode, Password);

                await Application.Current.MainPage.DisplayAlert("Success", "Password changed successfully", "OK");

                await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }


        }





    }
}
