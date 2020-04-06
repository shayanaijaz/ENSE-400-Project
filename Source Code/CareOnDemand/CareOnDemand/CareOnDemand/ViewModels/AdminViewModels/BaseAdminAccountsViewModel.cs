/*
    Care on Demand Application
    Capstone 2020 - ENSE 400/477
    The Ni(c)(k)S

    Author: Nicolas Achter
    Contributor(s): 
    Last Modified: Apr. 06, 2020
*/

using CareOnDemand.Models;
using System;

namespace CareOnDemand.ViewModels
{
    public class BaseAdminAccountsViewModel : BaseViewModel
    {

        static BaseAdminAccountsViewModel()
        {
            account = new Account();
        }

        protected static Account account;
        protected int account_level;

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

        public String AccountLevel
        {
            get 
            {
                switch (account_level)
                {
                    case 1:
                        return ("Admin");
                    case 2:
                        return ("Care Partner");
                    case 3:
                        return ("Customer");
                    default:
                        return null;
                }
            }

            set
            {
                switch (value)
                {
                    case ("Admin"):
                        account_level = 1;
                        break;
                    case ("Care Partner"):
                        account_level = 2;
                        break;
                    case ("Customer"):
                        account_level = 3;
                        break;
                }
            }
        }
    }
}
