/*
    Care on Demand Application
    Capstone 2020 - ENSE 400/477
    The Ni(c)(k)S

    Author: Shayan Khan
    Last Modified: Apr. 10, 2020
*/
using CareOnDemand.Data;
using CareOnDemand.Views.SharedViews;
using System;
using Xamarin.Forms;

namespace CareOnDemand.ViewModels
{
    /* This class contains bindings and functions relating to elements on the ConfirmForgotPassword page. It inherits variables and objects from the 
    * BaseCustomerDetailsViewModel class.
    */
    public class ConfirmForgotPassViewModel : BaseCustomerDetailsViewModel
    {
        // Constructor that initializes the commands on this page
        public ConfirmForgotPassViewModel()
        {
            ConfirmForgotPassCommand = new Command(ConfirmForgotPassword);
        }

        // Bindings and Commands on this page
        public Command ConfirmForgotPassCommand { private set; get; }

        public string VerificationCode { get; set; }

        /* This function uses the ForgotPasswordService class which communicates with AWS to handle the confim forgot password flow. Redirects user 
         * to login page upon successfull password reset
         */ 
        async void ConfirmForgotPassword()
        {
            try
            {
                ForgotPasswordService forgotPasswordService = new ForgotPasswordService(Email);

                // Function called with variables initialized in the base class
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
