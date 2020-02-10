using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CareOnDemand.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using CareOnDemand.Helpers;

namespace CareOnDemand.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

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

        void ContactButtonClicked()
        {
            try
            {
                PhoneDialer.Open(Settings.EDEN_CARE_PHONE_NUMBER);
            }
            catch (ArgumentNullException anEx)
            {
                // Number was null or white space
            }
            catch (FeatureNotSupportedException ex)
            {
                // Phone Dialer is not supported on this device.
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }
    }
}
