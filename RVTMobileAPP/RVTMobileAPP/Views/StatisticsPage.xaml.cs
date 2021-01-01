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

        public StatisticsPage()
        {
            InitializeComponent();
            Charts("0");
            Device.StartTimer(TimeSpan.FromSeconds(30), () => { 
                Charts("0");
                return true;
            });
        }

        public void Charts(string id)
        {
            try
            {
                
                var modelResult = StatsServices.Statistics(id).Result;
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
                        ValueLabel = item.Voters,
                        ValueLabelColor = SKColor.Parse(colors[n])
                    };
                    entries1.Add(stats1);
                }

                n = 0;


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

                Chart2.Chart = new PieChart { Entries = entries1, LabelTextSize = 30 };
                Chart1.Chart = new DonutChart { Entries = entries2, LabelTextSize = 30 };
            }
            catch
            {
            }
        }

        public StatisticsPage(string id)
        {
            InitializeComponent();
            Charts(id);
            Device.StartTimer(TimeSpan.FromSeconds(30), () => {
                Charts(id);
                return true;
            });
        }

        private async void RegionSelection_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (RegionSelection.SelectedIndex != -1)
                await Application.Current.MainPage.Navigation.PushAsync(new StatisticsPage((RegionSelection.SelectedIndex+1).ToString()));
        }
    }
}