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

namespace CareOnDemand.ViewModels
{
    public class RegisterViewModel  : BaseViewModel
    {
        public RegisterViewModel()
        {
            NextPageCommand = new Command(async () => await NextButtonClicked());
        }

        private String email;

        public String Email
        {
            get { return email; }
            set
            {
                email = value.Trim().ToLower();
            }
        }
        public String Password { get; set; }
        public String Number { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Address { get; set; }


        public Command NextPageCommand { private set; get; }

        async Task NextButtonClicked()
        {
            //RegisterModel registerModel = new RegisterModel(email, Password, Number, FirstName, Address);
            //registerModel.CreateCognitoUser();

            await Application.Current.MainPage.Navigation.PushAsync(new RegisterAddressPage());


        }

    }
}
