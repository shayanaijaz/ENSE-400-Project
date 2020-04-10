/*
    Care on Demand Application
    Capstone 2020 - ENSE 400/477
    The Ni(c)(k)S

    Author: Shayan Khan
    Last Modified: Apr. 10, 2020
*/
using System;
using Xamarin.Forms;
using CareOnDemand.Data;
using CareOnDemand.Views.SharedViews;

namespace CareOnDemand.ViewModels
{
    /* This class contains bindings and functions relating to elements on the ForgotPassword page. It inherits variables and objects from the 
     * BaseCustomerDetailsViewModel class.
     */ 
    public class ForgotPassViewModel : BaseCustomerDetailsViewModel
    {
        // Constructor that initializes the commands on this page
        public ForgotPassViewModel()
        {
            ForgotPasswordCommand = new Command(ForgotPassword);
        }

        // Commands on this page
        public Command ForgotPasswordCommand { private set; get; }

        // This function is run when the user clicks the Submit button. It uses the ForgotPasswordService class which communicates with AWS to handle 
        // the forgot password flow. On success, it redirects the user to ConfirmForgotPassword page. 
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
