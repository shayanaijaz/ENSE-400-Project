using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CareOnDemand.Views.SharedViews;
using CareOnDemand.Views.CustomerViews;

namespace CareOnDemand
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new LoginPage());

            bool isLoggedIn = Current.Properties.ContainsKey("isLoggedIn") ? Convert.ToBoolean(Current.Properties["isLoggedIn"]) : false;

            if (!isLoggedIn)
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else if (isLoggedIn)
            {
                MainPage = new NavigationPage(new NavBar());

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
