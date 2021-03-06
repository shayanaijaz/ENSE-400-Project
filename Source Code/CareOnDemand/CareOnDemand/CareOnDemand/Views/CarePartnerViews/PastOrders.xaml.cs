﻿using CareOnDemand.ViewModels.CarePartnerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CareOnDemand.Views.CarePartnerViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PastOrders : ContentPage
    {
        ViewCell lastCell;
        public PastOrders()
        {
            InitializeComponent();

            BindingContext = new PastOrdersViewModel();
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
            OrderListView.SelectedItem = null;
            if (lastCell != null)
                lastCell.View.BackgroundColor = Color.Transparent;
        }
    }
}