using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CareOnDemand.ViewModels;

namespace CareOnDemand.Views.SharedViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountManagementPage : ContentPage
    {
        public AccountManagementPage()
        {
            InitializeComponent();
            BindingContext = new AccountManagementViewModel();
            //if the account is a customer, display the add address button
            if((int)Application.Current.Properties["accountLevelID"] == 3)
            {
                add_addr.IsVisible = true;
            }
        }
    }
}

