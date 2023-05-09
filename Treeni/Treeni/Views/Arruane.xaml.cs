using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Globalization;
using Microcharts;
using Newtonsoft.Json;
using Microcharts.Forms;
using SkiaSharp;

namespace Treeni.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Arruane : ContentPage
    {
        private List<WeightEntry> weightEntries = new List<WeightEntry>();
        public Arruane()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Application.Current.Properties.ContainsKey("weightEntries"))
            {
                var weightEntriesJson = (string)Application.Current.Properties["weightEntries"];
                weightEntries = JsonConvert.DeserializeObject<List<WeightEntry>>(weightEntriesJson);
            }

            UpdateWeightChart();
        }

        private void UpdateWeightChart()
        {
            var chartEntries = weightEntries
                .Where(x => x.Weight > 0)
                .OrderBy(x => x.Date)
                .Select(x => new Microcharts.ChartEntry((float)x.Weight)
                {
                    Label = x.Date.ToString("dd/MM"),
                    ValueLabel = x.Weight.ToString(),
                    Color = SKColor.Parse("#266489")
                });

            weightChart.Chart = new LineChart { Entries = chartEntries.ToList() };
        }
        private void SaveWeight()
        {
            if (!string.IsNullOrEmpty(weightEntry.Text))
            {
                var weight = double.Parse(weightEntry.Text);
                var date = calendar.SelectedDates.FirstOrDefault();

                var existingEntry = weightEntries.FirstOrDefault(x => x.Date == date);

                if (existingEntry != null)
                {
                    existingEntry.Weight = weight;
                }
                else
                {
                    weightEntries.Add(new WeightEntry { Date = date, Weight = weight });
                }

                var weightEntriesJson = JsonConvert.SerializeObject(weightEntries);
                Application.Current.Properties["weightEntries"] = weightEntriesJson;
                Application.Current.SavePropertiesAsync();

                weightEntry.Text = "";
                UpdateWeightChart();
            }
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            SaveWeight();
        }
        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            weightEntries.Clear();
            var weightEntriesJson = JsonConvert.SerializeObject(weightEntries);
            Application.Current.Properties["weightEntries"] = weightEntriesJson;
            Application.Current.SavePropertiesAsync();
            UpdateWeightChart();
        }

    }
    public class WeightEntry
    {
        public DateTime Date { get; set; }
        public double Weight { get; set; }
    }
}