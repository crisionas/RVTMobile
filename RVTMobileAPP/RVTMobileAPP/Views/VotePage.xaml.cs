using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RVTMobileAPP.Interfaces;
using RVTMobileAPP.Models.Vote;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RVTMobileAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VotePage : ContentPage
    {
        protected string IDVN;
        public VotePage(string idvn)
        {
            InitializeComponent();
            IDVN = idvn;
        }

        private async void  VoteButtonOnClick_OnClicked(object sender, EventArgs e)
        {
            VoteButtonOnClick.IsEnabled = false;
            var model = new VoteMessage();
            model.IDVN = IDVN;
            if (GreenButton.IsChecked)
                model.Party = Int32.Parse(GreenButton.Text);
            else if (RedButton.IsChecked)
                model.Party = Int32.Parse(RedButton.Text);
            else if (BlueButton.IsChecked)
                model.Party = Int32.Parse(BlueButton.Text);
            else if (YellowButton.IsChecked)
                model.Party = Int32.Parse(YellowButton.Text);
            else if (RoseButton.IsChecked)
                model.Party = Int32.Parse(RoseButton.Text);
            else model.Party = 0;

            var service = DependencyService.Get<IUser>().Vote(model).Result;
            DisplayAlert("Votare", service.Message, "Ok");
            await Navigation.PopToRootAsync();

        }


    }
}