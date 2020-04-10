/*
    Care on Demand Application
    Capstone 2020 - ENSE 400/477
    The Ni(c)(k)S

    Author: Shayan Khan
    Last Modified: Apr. 10, 2020
*/
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Essentials;
using Xamarin.Forms;
using CareOnDemand.Helpers;

namespace CareOnDemand.ViewModels
{
    /* This ViewModel implements and defines bindings and functions that are used by several classes in the application.
     */ 
    public abstract class BaseViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        // Function used to update an element
        protected virtual void OnPropertyChanged(
        [CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));
        }

        public BaseViewModel()
        {
            ContactCommand = new Command(ContactButtonClicked);
        }

        public Command ContactCommand { private set; get; }

        // Function that opens phone dialer on button click
        void ContactButtonClicked()
        {
            try
            {
                PhoneDialer.Open(Settings.EDEN_CARE_PHONE_NUMBER);
            }
            catch (FeatureNotSupportedException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
