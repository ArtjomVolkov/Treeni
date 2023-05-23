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
            List<UserSettings> userSettingsList = App.Databases.GetUserSettingsAsync();
            UserSettings userSettings = userSettingsList.FirstOrDefault();
            base.OnAppearing();
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
    }
}