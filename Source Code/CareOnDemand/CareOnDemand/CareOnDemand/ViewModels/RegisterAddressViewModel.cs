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

namespace CareOnDemand.ViewModels
{
    public class RegisterAddressViewModel   : BaseRegisterViewModel
    {
        public RegisterAddressViewModel()
        {
            ProvinceList = GetProvinces().ToList();
            CityList = GetCities().ToList();
            CreateAccountCommand = new Command(CreateAccountClicked);
            customer_address = new Address();
        }

        public Command CreateAccountCommand { private set; get; }


        public String AddressLine1
        {
            get => customer_address.AddrLine1;
            set
            {
                customer_address.AddrLine1 = value;
            }
        }
        public String AddressCity
        {
            get => customer_address.City;
            set
            {
                customer_address.City = value;
            }
        }
        public String AddressProvince
        {
            get => customer_address.Province;
            set
            {
                customer_address.Province = value;
            }
        }
        public String AddressPostalCode
        {
            get => customer_address.PostalCode;
            set
            {
                customer_address.PostalCode = value;
            }
        }

        public List<Province> ProvinceList { get; set; }
        public List<City> CityList { get; set; }
    

        public List<Province> GetProvinces()
        {
            var provinces = new List<Province>()
            {
                new Province(){Key =  1, Value= "Saskatchewan"},
                //new Province(){Key =  2, Value= "BC"},
                //new Province(){Key =  3, Value= "MB"},
                //new Province(){Key =  4, Value= "NB"},
                //new Province(){Key =  5, Value= "NL"},
                //new Province(){Key =  6, Value= "NS"},
                //new Province(){Key =  7, Value= "ON"},
                //new Province(){Key =  8, Value= "PE"},
                //new Province(){Key =  9, Value= "QC"},
                //new Province(){Key =  10,Value= "SK"},
                //new Province(){Key =  11,Value= "NT"},
                //new Province(){Key =  12,Value= "NU"},
                //new Province(){Key =  13,Value= "YT"}
            };

            return provinces;
        }

        public List<City> GetCities()
        {
            var cities = new List<City>()
            {
                new City(){Key = 1, Value= "Moose Jaw"},
                new City(){Key = 2, Value= "Regina"},
                new City(){Key = 3, Value= "Saskatoon"}
            };

            return cities;
        }


        public class Province
        {
            public int Key { get; set; }
            public string Value { get; set; }
        }

        public class City
        {
            public int Key { get; set; }
            public string Value { get; set; }
        }

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
                RegisterService registerModel = new RegisterService(customer_details, customer_address);

                try
                {
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
