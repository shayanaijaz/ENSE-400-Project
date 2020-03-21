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
            account = new Account();
            account.Customer = new Customer();

            customer_address = new Address();
            //customer_address.Customer_Addresses = new List<Customer_Address>();
            //account.Customer.Customer_Addresses = new List<Customer_Address>();
        }

        protected static Account account;
        protected static Address customer_address;

        //public Account Account
        //{
        //    get { return account; }
        //    set
        //    {
        //        account = value;
        //    }
        //}

        public String Email
        {
            get => account.Email;
            set
            {
                account.Email = value.Trim().ToLower();
            }
        }
        public String Password
        {
            get => account.Password;
            set
            {
                account.Password = value;
            }
        }

        public String PasswordConfirmation
        {
            get => account.PasswordConfirmation;
            set
            {
                account.PasswordConfirmation = value;
            }
        }
        public String Number
        {
            get => account.PhoneNumber;
            set
            {
                account.PhoneNumber = "+1" + value;
            }
        }
        public String FirstName
        {
            get => account.FirstName;
            set
            {
                account.FirstName = value;
            }
        }
        public String LastName
        {
            get => account.LastName;
            set
            {
                account.LastName = value;
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
                customer_address.PostalCode = value.Trim().Replace(" ", String.Empty);
            }
        }
    }
}
