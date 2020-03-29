using CareOnDemand.Data;
using CareOnDemand.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CareOnDemand.ViewModels.AdminViewModels
{
    public class AdminHomeViewModel : BaseViewModel
    {
        public AdminHomeViewModel()
        {
            GetAdminName();
        }

        public string AdminName { get; set; }

        async void GetAdminName()
        {
            if (Application.Current.Properties["accountID"] != null)
            {
                Account account = await new AccountRestService().GetAccountByIDAsync((int)Application.Current.Properties["accountID"]);

                AdminName = "Welcome " + account.FirstName.Trim();
                OnPropertyChanged(nameof(AdminName));
            }
        }
    }
}
