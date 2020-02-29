using CareOnDemand.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareOnDemand.ViewModels
{
    public class BaseCustomerDetailsViewModel : BaseViewModel
    {

        static BaseCustomerDetailsViewModel()
        {
            customer_details = new Customer();
            customer_details.Account = new Account();
            customer_address = new Address();
        }

        protected static Customer customer_details;
        protected static Address customer_address;


        public String Email
        {
            get => customer_details.Account.Email;
            set
            {
                customer_details.Account.Email = value.Trim().ToLower();
            }
        }
        public String Password
        {
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
        public String Number
        {
            get => customer_details.Account.PhoneNumber;
            set
            {
                customer_details.Account.PhoneNumber = "+1" + value;
            }
        }
        public String FirstName
        {
            get => customer_details.Account.FirstName;
            set
            {
                customer_details.Account.FirstName = value;
            }
        }
        public String LastName
        {
            get => customer_details.Account.LastName;
            set
            {
                customer_details.Account.LastName = value;
            }
        }

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
    }
}
