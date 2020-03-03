using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CareOnDemand.ViewModels;

namespace CareOnDemand.Views.CustomerViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ServiceSelectedDetails : ContentPage
    {
        ViewCell lastCell;
        public ServiceSelectedDetails()
        {
            InitializeComponent();

            BindingContext = new ServiceSelectedDetailsViewModel();

        }

        private void ViewCellTapped(object sender, System.EventArgs e)
        {
            if (lastCell != null)
                lastCell.View.BackgroundColor = Color.Transparent;

            var viewCell = (ViewCell)sender;
            if (viewCell.View != null)
            {
                viewCell.View.BackgroundColor = Color.FromHex("#a5d5ed");
                lastCell = viewCell;
            }
        }
    }
}