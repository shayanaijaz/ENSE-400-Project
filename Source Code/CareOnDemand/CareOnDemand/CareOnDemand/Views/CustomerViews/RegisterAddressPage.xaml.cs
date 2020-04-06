using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CareOnDemand.ViewModels;
using CareOnDemand.Models;

namespace CareOnDemand.Views.CustomerViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterAddressPage : ContentPage
    {
        public RegisterAddressPage()
        {
            InitializeComponent();

            BindingContext = new RegisterAddressViewModel();
            //if there is an account, display the add address button instead of the create account button
            if ((int)Application.Current.Properties["accountLevelID"] == 3)
            {
                addr_label.IsVisible = true;
                create_acct.IsVisible = false;
                add_addr.IsVisible = true;
            }

        }
    }
}