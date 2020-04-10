/*
    Care on Demand Application
    Capstone 2020 - ENSE 400/477
    The Ni(c)(k)S

    Author: Shayan Khan
    Last Modified: Apr. 10, 2020
*/
using CareOnDemand.Data;
using CareOnDemand.Models;
using CareOnDemand.Views.AdminViews;
using CareOnDemand.Views.SharedViews;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CareOnDemand.ViewModels.AdminViewModels
{
    /* This class contains bindings and functions relating to elements on the AdminHome page. It inherits from the BaseViewModel class
     */
    public class AdminHomeViewModel : BaseViewModel
    {
        // Constructor that initializes commands and populates bindings
        public AdminHomeViewModel()
        {
            Task.Run(async () => await GetAdminName());

            GoToCreateAccountCommand = new Command(async () => await CreateAccountButtonClicked()); //create account button
            GoToMyAccountCommand = new Command(async () => await MyAccountButtonClicked());         //my account button
        }

        // Bindings on this page
        public string AdminName { get; set; }

        public Command GoToCreateAccountCommand { private set; get; }
        public Command GoToMyAccountCommand { private set; get; }

        // This Task uses the REST services to retrieve an admins name
        async Task GetAdminName()
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
