using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using CareOnDemand.Helpers;
using Amazon.CognitoIdentityProvider;
//using Amazon.CognitoIdentity;
using Amazon.CognitoIdentityProvider.Model;
//using Amazon.Extensions.CognitoAuthentication;
using Amazon.Runtime;
using CareOnDemand.Models;
using System.Threading.Tasks;
using CareOnDemand.Views.CustomerViews;
using CareOnDemand.Validators;
using FluentValidation;
using System.Linq;
using FluentValidation.Results;
//using System.ComponentModel.DataAnnotations;

namespace CareOnDemand.ViewModels
{
    public class RegisterViewModel : BaseRegisterViewModel
    {        
        public RegisterViewModel()
        {
            NextPageCommand = new Command(async () => await NextButtonClicked());
            customer_details = new Customer();
            customer_details.Account = new Account();
            
        }

        public String Email
        {
            get => customer_details.Account.Email;
            set
            {
                customer_details.Account.Email = value.Trim().ToLower();
            }
        }
        public String Password { 
            get => customer_details.Account.Password; 
            set
            {
                customer_details.Account.Password = value;
            }
        }

        public String PasswordConfirmation
        {
            get => customer_details.Account.PasswordConfirmation;
            set
            {
                customer_details.Account.PasswordConfirmation = value;
            }
        }
        public String Number { 
            get => customer_details.Account.PhoneNumber; 
            set
            {
                customer_details.Account.PhoneNumber = "+1" + value;
            }
        }
        public String FirstName { 
            get => customer_details.Account.FirstName; 
            set
            {
                customer_details.Account.FirstName = value;
            }
        }
        public String LastName { 
            get => customer_details.Account.LastName; 
            set
            {
                customer_details.Account.LastName = value;
            }
        }
     
        public Command NextPageCommand { private set; get; }
        
        async Task NextButtonClicked()
        {
            CustomerDetailsValidator customer_details_validator = new CustomerDetailsValidator();
            ValidationResult results = customer_details_validator.Validate(customer_details.Account);

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
