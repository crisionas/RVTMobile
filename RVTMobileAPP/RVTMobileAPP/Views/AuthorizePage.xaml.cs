using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RVTMobileAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthorizePage : ContentPage
    {
        public AuthorizePage()
        {
            InitializeComponent();
        }

        private async void Login_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        private async void Registration_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistPage());
        }
    }
}