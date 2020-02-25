using CareOnDemand.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CareOnDemand.Views.SharedViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmForgotPasswordPage : ContentPage
    {
        public ConfirmForgotPasswordPage()
        {
            InitializeComponent();

            BindingContext = new ConfirmForgotPassViewModel();
        }
    }
}