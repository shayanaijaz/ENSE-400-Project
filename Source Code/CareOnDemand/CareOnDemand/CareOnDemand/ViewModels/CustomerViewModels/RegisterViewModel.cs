/*
    Care on Demand Application
    Capstone 2020 - ENSE 400/477
    The Ni(c)(k)S

    Author: Shayan Khan
    Last Modified: Apr. 09, 2020
*/
using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using CareOnDemand.Views.CustomerViews;
using CareOnDemand.Validators;
using FluentValidation.Results;

namespace CareOnDemand.ViewModels
{
    /*
     * This class defines bindings and commands related to the first Register page the user goes to when clicking the New User button from the login page.
     * It inheherits variables and objects from the BaseCustomerDetailsViewModel
     */
    public class RegisterViewModel : BaseCustomerDetailsViewModel
    {
        // Constructor that initializes the bindings and commands
        public RegisterViewModel()
        {
            NextPageCommand = new Command(async () => await NextButtonClicked());
        }

        // Command used on this page
        public Command NextPageCommand { private set; get; }
        
        /* Function for when the user clicks the next button. It runs validation on the data entered by the user on the page. If a field is 
         * left blank or not filled out properly it displays an alert with the errors, otherwise it sends the user to the RegisterAddress page
         */
        async Task NextButtonClicked()
        {
            CustomerDetailsValidator customer_details_validator = new CustomerDetailsValidator();
            ValidationResult results = customer_details_validator.Validate(account);

            if (!results.IsValid)
            {
                String result_messages = results.ToString("\n");
                await Application.Current.MainPage.DisplayAlert("Error", result_messages, "OK");
            }
            else
            {
                await Application.Current.MainPage.Navigation.PushAsync(new RegisterAddressPage());
            }
        }
    }
}
