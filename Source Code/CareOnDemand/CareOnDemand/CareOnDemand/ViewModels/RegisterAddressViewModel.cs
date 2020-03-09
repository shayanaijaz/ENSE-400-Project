using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using CareOnDemand.Models;
using CareOnDemand.Views.SharedViews;
using CareOnDemand.Validators;
using FluentValidation;
using FluentValidation.Results;
using CareOnDemand.Data;

namespace CareOnDemand.ViewModels
{
    public class RegisterAddressViewModel   : BaseCustomerDetailsViewModel
    {
        public RegisterAddressViewModel()
        {
            CreateAccountCommand = new Command(CreateAccountClicked);
        }

        public Command CreateAccountCommand { private set; get; }

        async void CreateAccountClicked()
        {
            CustomerAddressValidator customer_address_validator = new CustomerAddressValidator();
            ValidationResult results = customer_address_validator.Validate(customer_address);

            if (!results.IsValid)
            {
                String result_messages = results.ToString("\n");
                await Application.Current.MainPage.DisplayAlert("Error", result_messages, "OK");
            }
            else
            {
                RegisterService registerModel = new RegisterService(account, customer_address);

                try
                {
                    await registerModel.AddAddress(account);
                    await registerModel.CreateDatabaseUser(account);
                    await registerModel.CreateCognitoUser();
                    await Application.Current.MainPage.DisplayAlert("Success", "Account created succesfully. Please check your email for verification link", "OK");
                    await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
                }
                catch(Exception e)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                }
            }

        }

    }
}
