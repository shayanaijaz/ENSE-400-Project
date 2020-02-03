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
    public class RegisterViewModel
    {
        public RegisterViewModel()
        {
            NextPageCommand = new Command(async () => await NextButtonClicked());
            ProvinceList = GetProvinces().ToList();
            CreateAccountCommand = new Command(CreateAccountClicked);
        }

        private String email;
        private String password;
        private String number;
        private String first_name;
        private String last_name;
        private String address_line_1;
        private String address_city;
        private String address_province;
        private String address_postal_code;


        public String Email
        {
            get { return email; }
            set
            {
                email = value.Trim().ToLower();
            }
        }
        public String Password { 
            get => password; 
            set
            {
                password = value;
            }
        }
        public String Number { 
            get => number; 
            set
            {
                number = "+1" + value;
            }
        }
        public String FirstName { 
            get => first_name; 
            set
            {
                first_name = value;
            }
        }
        public String LastName { 
            get => last_name; 
            set
            {
                last_name = value;
            }
        }
        public String AddressLine1 { 
            get => address_line_1; 
            set
            {
                address_line_1 = value;
            }
        }
        public String AddressCity {
            get => address_city; 
            set
            {
                address_city = value;
            }
        }
        public String AddressProvince {
            get => address_province; 
            set
            {
                address_province = value;
            }
        }
        public String AddressPostalCode { 
            get => address_postal_code; 
            set
            {
                address_postal_code = value;
            }
        }

        public class Province
        {
            public int Key { get; set; }
            public string Value { get; set; }
        }


        public Command NextPageCommand { private set; get; }
        public Command CreateAccountCommand { private set; get; }
        public List<Province> ProvinceList { get; set; }

        public List<Province> GetProvinces()
        {
            var provinces = new List<Province>()
            {
                new Province(){Key =  1, Value= "AB"},
                new Province(){Key =  2, Value= "BC"},
                new Province(){Key =  3, Value= "MB"},
                new Province(){Key =  4, Value= "NB"},
                new Province(){Key =  5, Value= "NL"},
                new Province(){Key =  6, Value= "NS"},
                new Province(){Key =  7, Value= "ON"},
                new Province(){Key =  8, Value= "PE"},
                new Province(){Key =  9, Value= "QC"},
                new Province(){Key =  10,Value= "SK"},
                new Province(){Key =  11,Value= "NT"},
                new Province(){Key =  12,Value= "NU"},
                new Province(){Key =  13,Value= "YT"}
            };

            return provinces;
        }



        async Task NextButtonClicked()
        {
            //RegisterModel registerModel = new RegisterModel(email, Password, Number, FirstName, Address);
            //registerModel.CreateCognitoUser();
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterAddressPage());
        }

        public void CreateAccountClicked()
        {
            Console.Out.WriteLine(first_name);
            Console.Out.WriteLine(last_name);
        } 

        

    }
}
