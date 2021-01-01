using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using RVTMobileAPP.Interfaces;
using RVTMobileAPP.Models.Results;
using RVTMobileAPP.Models.Vote;
using RVTMobileAPP.Services;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RVTMobileAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatisticsPage : ContentPage
    {
        //private static StatisticsResponse modelResult;
       

        public StatisticsPage()
        {
            InitializeComponent();

                Charts("0");
        }

        public void Charts(string id)
        {
            try
            {
                var stats = new StatsServices();
                var modelResult = stats.Statistics(id).Result;
                PopulationLabel.Text = modelResult.Population.ToString();
                VotantLabel.Text = modelResult.Voters.ToString();
                ParticipationLabel.Text= ((modelResult.Voters * 100) / modelResult.Population).ToString()+"%";
                RegionName.Text = modelResult.Name;
                List<ChartEntry> entries1 = new List<ChartEntry>();
                string[] colors = { "#007bff", "#007bff", "#dc3545", "#ffc107", "#28a745", "#1cc88a" };
                int n = 0;
                foreach (var item in modelResult.AgeVoters)
                {

                    var stats1 = new ChartEntry(Int32.Parse(item.Voters))
                    {
                        Color = SKColor.Parse(colors[n++]),
                        Label = item.Ages,
                        ValueLabel = item.Voters
                    };
                    entries1.Add(stats1);
                }

                n = 0;


                List<ChartEntry> entries2 = new List<ChartEntry>();
                var female = new ChartEntry(modelResult.GenderStatistics.Female)
                    {
                        Color = SKColor.Parse(colors[1]),
                        Label = "Feminin",
                        ValueLabel = modelResult.GenderStatistics.Female.ToString()
                    };
                    entries2.Add(female);

                    var male = new ChartEntry(modelResult.GenderStatistics.Male)
                    {
                        Color = SKColor.Parse(colors[2]),
                        Label = "Masculin",
                        ValueLabel = modelResult.GenderStatistics.Male.ToString()
                    };
                    entries2.Add(female);

                Chart2.Chart = new PieChart { Entries = entries1 };
                Chart1.Chart = new DonutChart { Entries = entries2 };
            }
            catch
            {
            }
        }

        public StatisticsPage(string id)
        {
            InitializeComponent();
            Charts(id);
        }

        private async void RegionSelection_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (RegionSelection.SelectedIndex != -1)
                await Application.Current.MainPage.Navigation.PushAsync(new StatisticsPage((RegionSelection.SelectedIndex+1).ToString()));
        }
    }
}