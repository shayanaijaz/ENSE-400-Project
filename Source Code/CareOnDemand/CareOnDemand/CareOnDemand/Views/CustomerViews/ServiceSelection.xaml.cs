using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CareOnDemand.Views.CustomerViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ServiceSelection : ContentPage
    {
        public ServiceSelection()
        {
            InitializeComponent();
            ServiceSelector.Items.Add("T1"); //Populate picker
            ServiceSelector.Items.Add("T2"); //Populate picker
            ServiceSelector.Items.Add("T3"); //Populate picker
            ServiceSelector.Items.Add("T4"); //Populate picker
            ServiceSelector.Items.Add("T5"); //Populate picker
            ServiceSelector.Items.Add("T6"); //Populate picker
        }

        //Used to get the value of a selected service in serviceSelector Picker
        private void ServiceSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
           var service = ServiceSelector.Items[ServiceSelector.SelectedIndex];
           DisplayAlert(service, "Was selected", "Ok"); //Display test to confirm picker is working
        }
    }
}