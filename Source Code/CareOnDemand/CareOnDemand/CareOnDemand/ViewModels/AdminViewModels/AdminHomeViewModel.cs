using CareOnDemand.Data;
using CareOnDemand.Models;
using CareOnDemand.Views.AdminViews;
using CareOnDemand.Views.SharedViews;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CareOnDemand.ViewModels.AdminViewModels
{
    public class AdminHomeViewModel : BaseViewModel
    {
        public AdminHomeViewModel()
        {
            GetAdminName();

            GoToCreateAccountCommand = new Command(async () => await CreateAccountButtonClicked()); //create account button
            GoToMyAccountCommand = new Command(async () => await MyAccountButtonClicked());         //my account button
        }

        public string AdminName { get; set; }

        public Command GoToCreateAccountCommand { private set; get; }
        public Command GoToMyAccountCommand { private set; get; }

        async void GetAdminName()
        {
            if (Application.Current.Properties["accountID"] != null)
            {
                Account account = await new AccountRestService().GetAccountByIDAsync((int)Application.Current.Properties["accountID"]);

                AdminName = "Welcome " + account.FirstName.Trim();
                OnPropertyChanged(nameof(AdminName));
            }
        }

        //go to account management page
        async Task CreateAccountButtonClicked()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AccountCreationPage());
        }

        //go to account management page
        async Task MyAccountButtonClicked()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AccountManagementPage());
        }
    }
}
