using System;
using Xamarin.Forms;
using CareOnDemand.Models;
using System.Threading.Tasks;
using CareOnDemand.Views.AdminViews;
using CareOnDemand.Validators;
using FluentValidation.Results;

namespace CareOnDemand.ViewModels
{
    public class AccountCreationViewModel : BaseAdminAccountsViewModel
    {
        public AccountCreationViewModel()
        {
            CreateAccountCommand = new Command(async () => await CreateAccountButtonClicked());
        }
        public Command CreateAccountCommand { private set; get; }
        
        async Task CreateAccountButtonClicked()
        {
            CustomerDetailsValidator account_details_validator = new CustomerDetailsValidator();
            ValidationResult results = account_details_validator.Validate(account); //validate account data

            if (!results.IsValid)
            {
                String result_messages = results.ToString("\n");
                await Application.Current.MainPage.DisplayAlert("Error", result_messages, "OK");
            }
            else
            {
                RegisterService registerModel = new RegisterService(account);
                try //to save account entry to db
                {
                    await registerModel.CreateCognitoUser();
                    await registerModel.CreateDatabaseUser(account, account_level);
                    await Application.Current.MainPage.DisplayAlert("Account Created", " ", "OK");
                    await Application.Current.MainPage.Navigation.PushAsync(new AdminHome());
                }
                catch (Exception e)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                }

            }
        }
    }
}
