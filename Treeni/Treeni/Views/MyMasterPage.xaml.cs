using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Treeni.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyMasterPage : MasterDetailPage
    {
        public MyMasterPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            OnAppearing();
        }
        private void ClearDB_Clicked(object sender, EventArgs e)
        {
            App.Database.ClearDatabase();
            DisplayAlert("Info", "Database kustatud", "OK");
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var Time = DateTime.Now.TimeOfDay;
            if (Time < new TimeSpan(12, 0, 0))
            {
                tere.Text = "Tere hommikust";
            }
            else
            {
                tere.Text = "Tere päevast";
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

        private async void meeldetuletus_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Meeldetuletus());
        }

        private async void youtube_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Youtube());
        }
    }
}