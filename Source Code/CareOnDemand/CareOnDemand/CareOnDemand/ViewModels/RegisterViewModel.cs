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
using System.Linq;

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

            await Application.Current.MainPage.Navigation.PushAsync(new RegisterAddressPage());
        }
    }
}
