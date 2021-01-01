using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using RVTMobileAPP.Interfaces;
using RVTMobileAPP.Services;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RVTMobileAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultsPage : ContentPage
    {
        public ResultsPage()
        {
            InitializeComponent();
            Charts("0");
            Device.StartTimer(TimeSpan.FromSeconds(30), () => {
                Charts("0");
                return true;
            });
        }

        public ResultsPage(string id)
        {
            InitializeComponent();
            Charts(id);
            Device.StartTimer(TimeSpan.FromSeconds(30), () => {
                Charts(id);
                return true;
            });
        }

        public void Charts(string id)
        {
            try
            {
                var modelResult = ResultsServices.Results(id).Result;
                PopulationLabel.Text = modelResult.Population.ToString();
                VotantLabel.Text = modelResult.Votants.ToString();
                ParticipationLabel.Text = ((modelResult.Votants * 100) / modelResult.Population).ToString() + "%";
                RegionName.Text = modelResult.Name;
                List<ChartEntry> entries1 = new List<ChartEntry>();
                
                foreach (var item in modelResult.TotalVotes)
                {

                    var stats1 = new ChartEntry(item.Votes)
                    {
                        Color = SKColor.Parse(item.Color),
                        Label = item.IDParty+" "+item.Name,
                        ValueLabel = item.Votes.ToString(),
                        ValueLabelColor = SKColor.Parse(item.Color)

                    };
                    entries1.Add(stats1);
                }


                List<ChartEntry> entries2 = new List<ChartEntry>();
                var female = new ChartEntry(modelResult.GenderStatistics.Female)
                {
                    Color = SKColor.Parse("#1cc88a"),
                    Label = "Feminin",
                    ValueLabel = modelResult.GenderStatistics.Female.ToString(),
                    ValueLabelColor = SKColor.Parse("#1cc88a")
                };
                entries2.Add(female);

                var male = new ChartEntry(modelResult.GenderStatistics.Male)
                {
                    Color = SKColor.Parse("#4e73df"),
                    Label = "Masculin",
                    ValueLabel = modelResult.GenderStatistics.Male.ToString(),
                    ValueLabelColor = SKColor.Parse("#4e73df")
                };
                entries2.Add(male);

                Chart2.Chart = new BarChart() {Entries = entries1, LabelTextSize = 40};
                Chart1.Chart = new DonutChart() {Entries = entries2, LabelTextSize = 40};
            }
            catch
            {
            }
        }


        private async void RegionSelection_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (RegionSelection.SelectedIndex != -1)
                await Application.Current.MainPage.Navigation.PushAsync(new ResultsPage((RegionSelection.SelectedIndex + 1).ToString()));
        }
    }
}