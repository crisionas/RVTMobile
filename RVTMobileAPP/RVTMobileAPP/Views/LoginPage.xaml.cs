using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RVTMobileAPP.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RVTMobileAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }


        private void Button_OnClicked(object sender, EventArgs e)
        {
            DisplayAlert("Alert", "please enter password", "ok");
        }
    }
}