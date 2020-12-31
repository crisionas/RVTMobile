using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RVTMobileAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatisticsPage : ContentPage
    {
        private List<ChartEntry> entries = new List<ChartEntry>
        {
            new ChartEntry(100)
            {
                Color=SKColor.Parse("#FF0000"),
                Label = "january",
                ValueLabel = "100"
            },
            new ChartEntry(200)
            {
            Color=SKColor.Parse("#D8FF00"),
            Label = "january",
            ValueLabel = "200"
        },            new ChartEntry(300)
            {
                Color=SKColor.Parse("#0CFF00"),
                Label = "january",
                ValueLabel = "300"
            }
        };
        
        public StatisticsPage()
        {
            InitializeComponent();
            Chart1.Chart = new DonutChart{Entries = entries};
            Chart2.Chart = new PieChart { Entries = entries };
        }
    }
}