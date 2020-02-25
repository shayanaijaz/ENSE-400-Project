//using System;
//using System.Collections.Generic;
//using System.Text;
//using CareOnDemand.Models;

//namespace CareOnDemand.ViewModels
//{
//    public class BaseRegisterViewModel : BaseViewModel
//    {
//        //protected static Account user_account_details = new Account();
//        //protected static Address customer_address = new Address();
//        //protected static Customer customer_details = new Customer();

//        public BaseRegisterViewModel()
//        {
//            customer_details = new Customer();
//            customer_details.Account = new Account();
//        }

//        protected static Customer customer_details;
//        protected static Address customer_address;


//        public String Email
//        {
//            get => customer_details.Account.Email;
//            set
//            {
//                customer_details.Account.Email = value.Trim().ToLower();
//            }
//        }
//        public String Password
//        {
//            get => customer_details.Account.Password;
//            set
//            {
//                customer_details.Account.Password = value;
//            }
//        }

//        public String PasswordConfirmation
//        {
//            get => customer_details.Account.PasswordConfirmation;
//            set
//            {
//                customer_details.Account.PasswordConfirmation = value;
//            }
//        }
//        public String Number
//        {
//            get => customer_details.Account.PhoneNumber;
//            set
//            {
//                customer_details.Account.PhoneNumber = "+1" + value;
//            }
//        }
//        public String FirstName
//        {
//            get => customer_details.Account.FirstName;
//            set
//            {
//                customer_details.Account.FirstName = value;
//            }
//        }
//        public String LastName
//        {
//            get => customer_details.Account.LastName;
//            set
//            {
//                customer_details.Account.LastName = value;
//            }
//        }
//    }
//}
