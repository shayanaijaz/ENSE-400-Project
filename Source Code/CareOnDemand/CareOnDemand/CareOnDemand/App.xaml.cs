using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CareOnDemand.Views.SharedViews;
using CareOnDemand.Views.CustomerViews;
using CareOnDemand.Views.AdminViews;
using CareOnDemand.Views.CarePartnerViews;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace CareOnDemand
{
    public partial class App : Xamarin.Forms.Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new LoginPage());
            //Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);

            bool isLoggedIn = Current.Properties.ContainsKey("isLoggedIn") ? Convert.ToBoolean(Current.Properties["isLoggedIn"]) : false;

            isLoggedIn = false;

            if (!isLoggedIn)
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else if (isLoggedIn)
            {
                MainPage = new NavigationPage(new CustomerNavBar());

            }
            //MainPage = new NavigationPage(new TestTabbed());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
