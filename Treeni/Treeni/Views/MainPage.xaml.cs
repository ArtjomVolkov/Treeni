using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Treeni.Models;
using Treeni.Views;
using Xamarin.Forms;

namespace Treeni
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            base.OnAppearing();
            UpdateExerciseValues();
        }

        private async void Ple4i_DC(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page());
        }

        private async void Plank_Dc(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Plank());
        }

        private async void Grud_Dc(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page());
        }

        private async void Ruki_Dc(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page());
        }

        private async void Nogi_Dc(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page());
        }

        private void UpdateExerciseValues()
        {
            // Выбираем все записи из таблицы Tren в базе данных
            List<Tren> exercises = App.Database.GetAllExercises();

            // Отображаем значения treenid, kaal и minutes на странице
            int totalTrennid = 0;
            int totalKaal = 0;
            int totalMinutes = 0;
            foreach (Tren exercise in exercises)
            {
                totalTrennid += exercise.Trennid;
                totalKaal += exercise.Kaal;
                totalMinutes += exercise.Minutes;
            }

            Trennid.Text = totalTrennid.ToString();
            Minutes.Text = totalMinutes.ToString();
            Kaal.Text = totalKaal.ToString();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateExerciseValues();
        }
    }

}
