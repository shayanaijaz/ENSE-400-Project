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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            try
            {
                BindingContext = new LoginViewModel();
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("error", ex.Message, "OK");
            }
            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            Application.Current.MainPage.Navigation.PushAsync(new ForgotPassPage());
        }
    }
}