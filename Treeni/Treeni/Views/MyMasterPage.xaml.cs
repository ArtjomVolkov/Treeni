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
            int totalMinuts = 0;
            foreach (Tren exercise in exercises)
            {
                totalTrennid += exercise.Trennid;
                totalMinuts += exercise.Minutes;
            }
            Console.WriteLine("Time: " + totalTrennid + " minutes");
            saavutus1.BindingContext = 5;
            saavutus2.BindingContext = 10;
            saavutus3.BindingContext = 15;
            saavutus4.BindingContext = 100;
            saavutus5.BindingContext = 200;
            saavutus6.BindingContext = 300;
            if (totalTrennid >= 5)
            {
                saavutus1.Source = "medal3.png";
            }
            else if (totalTrennid < 5)
            {
                saavutus1.Source = "";
                saavutus1.IsEnabled = false;
                saavutus2.IsEnabled = false;
                saavutus3.IsEnabled = false;
            }
            if (totalTrennid >= 10)
            {
                saavutus2.Source = "medal2.png";
            }
            else if (totalTrennid < 10)
            {
                saavutus2.Source = "";
                saavutus2.IsEnabled = false;
                saavutus3.IsEnabled = false;
            }
            if (totalTrennid >= 15)
            {
                saavutus3.Source = "medal4.png";
            }
            else if (totalTrennid < 15)
            {
                saavutus3.Source = "";
                saavutus3.IsEnabled = false;
            }
            if (totalMinuts >= 100)
            {
                saavutus4.Source = "medal100.png";
            }
            else if (totalMinuts < 100)
            {
                saavutus4.Source = "";
                saavutus4.IsEnabled = false;
                saavutus5.IsEnabled = false;
                saavutus6.IsEnabled = false;
            }
            if (totalMinuts >= 200)
            {
                saavutus5.Source = "medal200.png";
            }
            else if (totalMinuts < 200)
            {
                saavutus5.Source = "";
                saavutus5.IsEnabled = false;
                saavutus6.IsEnabled = false;
            }
            if (totalMinuts >= 300)
            {
                saavutus6.Source = "medal300.png";
            }
            else if (totalMinuts < 300)
            {
                saavutus6.Source = "";
                saavutus6.IsEnabled = false;
            }
            var Time = DateTime.Now.TimeOfDay;
            Console.WriteLine(Time.ToString());
            if (userSettings != null)
            {
                if (Time < new TimeSpan(11, 0, 0))
                {
                    tere.Text = "Tere hommikust, " + userSettings.Name;
                    if (userSettings.Name == "")
                    {
                        tere.Text = "Tere tulemast";
                    }
                }
                else if (Time < new TimeSpan(17, 0, 0))
                {
                    tere.Text = "Tere päevast, " + userSettings.Name;
                    if (userSettings.Name == "")
                    {
                        tere.Text = "Tere tulemast";
                    }
                }
                else if (Time < new TimeSpan(6, 0, 0))
                {
                    tere.Text = "Tere õhtust, " + userSettings.Name;
                    if (userSettings.Name == "")
                    {
                        tere.Text = "Tere tulemast";
                    }
                }
                else
                {
                    tere.Text = "Tere ööst, " + userSettings.Name;
                    if (userSettings.Name == "")
                    {
                        tere.Text = "Tere tulemast";
                    }
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
        private void OnSaavutusTapped2(object sender, EventArgs e)
        {
            Image tappedImage = (Image)sender;
            int treeningsCount;
            if (tappedImage.BindingContext is int count)
            {
                treeningsCount = count;
                string message = $"Teenitud saavutus harjutustele kulutatud {treeningsCount} minuti eest.";
                DisplayAlert("Saavutus", message, "ОК");
            }
        }

        private async void music_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new muusika());
        }
    }
}