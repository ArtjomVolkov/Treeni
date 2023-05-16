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
        private void ClearDB_Clicked(object sender, EventArgs e)
        {
            App.Database.ClearDatabase();
            DisplayAlert("Info", "Database kustatud", "OK");
        }
        protected override void OnAppearing()
        {
            string nimid = userSettings.Name;
            base.OnAppearing();
            var Time = DateTime.Now.TimeOfDay;
            if (Time < new TimeSpan(12, 0, 0))
            {
                tere.Text = "Tere hommikust";
            }
            else if(Time < new TimeSpan(18, 0, 0))
            {
                tere.Text = "Tere päevast";
            }
            else
            {
                tere.Text = "Tere õhtust";
            }
        }

        private async void home_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
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
    }
}