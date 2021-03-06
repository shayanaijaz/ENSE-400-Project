﻿/*
    Care on Demand Application
    Capstone 2020 - ENSE 400/477
    The Ni(c)(k)S

    Author: Shayan Khan
    Contributor(s): Nicolas Achter
    Last Modified: Apr. 07, 2020
*/

using CareOnDemand.Models;
using System;

namespace CareOnDemand.ViewModels
{
    /*
     * This base class defines bindings that will be shared between other customer related classes.
     * Bindings relating to customer information are defined in this class. 
     */
    public class BaseCustomerDetailsViewModel : BaseViewModel
    {
        static BaseCustomerDetailsViewModel()
        {
            account = new Account();
            account.Customer = new Customer();

            address = new Address();
            customer_address = new Customer_Address();
        }

        // static variable declartions
        protected static Account account;
        protected static Address address;
        protected static Customer_Address customer_address;

        // Various customer information bindings that use the Account and Address model
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
            get => address.AddrLine1;
            set
            {
                address.AddrLine1 = value;
            }
        }
        public String AddressCity
        {
            get => address.City;
            set
            {
                address.City = value;
            }
        }
        public String AddressProvince
        {
            get => address.Province;
            set
            {
                address.Province = value;
            }
        }
        public String AddressPostalCode
        {
            get => address.PostalCode;
            set
            {
                address.PostalCode = value.Trim().Replace(" ", String.Empty);
            }
        }
        public String AddressLabel
        {
            get => customer_address.AddressLabel; 
            set
            {
                customer_address.AddressLabel = value.Trim().Replace(" ", String.Empty);
            }
        }
    }
}
