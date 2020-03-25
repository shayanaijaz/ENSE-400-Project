using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CareOnDemand.ViewModels.AdminViewModels;

namespace CareOnDemand.Views.AdminViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewOrdersPage : ContentPage
    {
        ViewCell lastCell;
        public NewOrdersPage()
        {
            InitializeComponent();

            BindingContext = new NewOrdersViewModel();
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (lastCell != null)
                lastCell.View.BackgroundColor = Color.Transparent;
            OrderListView.SelectedItem = null;
        }
    }
}