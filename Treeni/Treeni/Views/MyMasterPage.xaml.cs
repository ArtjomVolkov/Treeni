using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Treeni.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Treeni.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyMasterPage : MasterDetailPage
    {
        private UserSettings userSettings;
        public MyMasterPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            userSettings = new UserSettings();
            OnAppearing();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            List<UserSettings> userSettingsList = App.Databases.GetUserSettingsAsync();
            UserSettings userSettings = userSettingsList.FirstOrDefault();
            List<Tren> exercises = App.Database.GetAllExercises();
            int totalTrennid = 0;
            foreach (Tren exercise in exercises)
            {
                totalTrennid += exercise.Trennid;
            }
            Console.WriteLine("Time: " + totalTrennid + " minutes");
            saavutus1.BindingContext = 5;
            saavutus2.BindingContext = 10;
            saavutus3.BindingContext = 15;
            if (totalTrennid >= 5)
            {
                saavutus1.Source = "medal3.png";
            }
            else if (totalTrennid < 5)
            {
                saavutus1.Source = "";
            }
            if (totalTrennid >= 10)
            {
                saavutus2.Source = "medal2.png";
            }
            else if (totalTrennid < 10)
            {
                saavutus2.Source = "";
            }
            if (totalTrennid >= 15)
            {
                saavutus3.Source = "medal4.png";
            }
            else if (totalTrennid < 15)
            {
                saavutus3.Source = "";
            }
            var Time = DateTime.Now.TimeOfDay;
            Console.WriteLine(Time.ToString());
            if (userSettings != null)
            {
                if (Time < new TimeSpan(11, 0, 0))
                {
                    tere.Text = "Tere hommikust, " + userSettings.Name;
                }
                else if (Time < new TimeSpan(17, 0, 0))
                {
                    tere.Text = "Tere päevast, " + userSettings.Name;
                }
                else if (Time < new TimeSpan(6, 0, 0))
                {
                    tere.Text = "Tere õhtust, " + userSettings.Name;
                }
                else
                {
                    tere.Text = "Tere ööst, " + userSettings.Name;
                }
            }
        }

        private async void aruanne_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Arruane());
        }

        private async void youtube_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Youtube());
        }

        private async void seaded_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Seaded());
        }
        private void OnSaavutusTapped(object sender, EventArgs e)
        {
            Image tappedImage = (Image)sender;
            int treeningsCount;
            if (tappedImage.BindingContext is int count)
            {
                treeningsCount = count;
                string message = $"Teenitud saavutus {treeningsCount} lõpetatud treeningu eest.";
                DisplayAlert("Saavutus", message, "ОК");
            }
        }
    }
}