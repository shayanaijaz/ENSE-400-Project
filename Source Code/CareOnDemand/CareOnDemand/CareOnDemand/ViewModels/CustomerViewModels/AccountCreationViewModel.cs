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
using CareOnDemand.Data;

namespace CareOnDemand.ViewModels
{
    public class AccountCreationViewModel : BaseViewModel
    {
        public AccountCreationViewModel()
        {
            AccountList = GetAccount().ToList();
        }
        public List<Account> AccountList { get; set; }

        public class Account
        {
            public int Key { get; set; }
            public string Value { get; set; }
        }

        public List<Account> GetAccount()
        {
            var Accounts = new List<Account>()
            {
                new Account(){Key = 1, Value= "Admin"},
                new Account(){Key = 2, Value= "Care Partner" }  
            };

            return Accounts;
        }
        
    }
}