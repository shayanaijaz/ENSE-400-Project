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
    public partial class ForgotPassPage : ContentPage
    {
        public ForgotPassPage()
        {
            InitializeComponent();

            BindingContext = new ForgotPassViewModel();
        }


    }
}