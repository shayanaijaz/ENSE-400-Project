using CareOnDemand.ViewModels.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CareOnDemand.Views.AdminViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewActivePastOrders : ContentPage
    {
        public ViewActivePastOrders()
        {
            InitializeComponent();

            BindingContext = new ViewActivePastOrderViewModel();
        }
    }
}