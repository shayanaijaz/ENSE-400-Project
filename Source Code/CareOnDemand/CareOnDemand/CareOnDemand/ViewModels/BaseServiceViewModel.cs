using System;
using System.Collections.Generic;
using System.Text;
using CareOnDemand.Models;
using CareOnDemand.Views.CustomerViews;
using Xamarin.Forms;

namespace CareOnDemand.ViewModels
{
    class BaseServiceViewModel : BaseViewModel
    {

        static BaseServiceViewModel()
        {
            service = new Service();
        }

        protected static Service service;

        public Service SelectedService
        {
            get => service;
            set
            {
                service = value;

                if (service == null)
                    return;

                HandleServiceSelected();

                SelectedService = null;
            }
        }

        async void HandleServiceSelected()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ServiceSelectedDetails());
        }
    }
}
