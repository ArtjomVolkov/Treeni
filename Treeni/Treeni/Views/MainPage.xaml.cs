using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Treeni.Models;
using Treeni.Views;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
using System.Security.Cryptography;

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
            await Navigation.PushAsync(new Grud());
        }

        private async void Ruki_Dc(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Ruki());
        }

        private async void Nogi_Dc(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Nogi());
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

            TrennidLabel.Text = totalTrennid.ToString();
            MinutesLabel.Text = totalMinutes.ToString();
            KaalLabel.Text = totalKaal.ToString();
        }
        private async void OnGoalButtonClicked(object sender, EventArgs args)
        {
            //модальное окно
            var popup = new PopupPage();
            popup.WidthRequest = 300;
            popup.BackgroundColor = Color.FromHex("#80000000");
            popup.HasSystemPadding = true;
            //stacklayout
            var layout = new StackLayout();
            layout.BackgroundColor = Color.White;
            layout.VerticalOptions = LayoutOptions.CenterAndExpand;
            layout.HorizontalOptions = LayoutOptions.CenterAndExpand;
            layout.Padding = new Thickness(20);
            //lbl
            var titleLabel = new Label();
            titleLabel.Text = "Seadke oma nädala eesmärk";
            titleLabel.TextColor = Color.Black;
            titleLabel.FontSize = 20;
            titleLabel.HorizontalOptions = LayoutOptions.Center;

            var goalEntry = new Entry();
            goalEntry.TextColor = Color.Black;

            // Получение цели и вывод ее в Label
            if (App.Current.Properties.ContainsKey("Goal"))
            {
                var savedGoal = (string)App.Current.Properties["Goal"];
                goalEntry.Text = savedGoal;
            }

            var saveButton = new Button();
            saveButton.Text = "Salvesta eesmärk";
            saveButton.BackgroundColor = Color.White;
            saveButton.TextColor = Color.Black;
            saveButton.Margin = new Thickness(0, 20, 0, 0);
            saveButton.Clicked += async (s, e) =>
            {
                // Сохранение цели
                App.Current.Properties["Goal"] = goalEntry.Text;
                await App.Current.SavePropertiesAsync();
                await PopupNavigation.Instance.PopAsync();
            };

            var deleteButton = new Button();
            deleteButton.Text = "Kustuta eesmärk";
            deleteButton.BackgroundColor = Color.White;
            deleteButton.TextColor = Color.Black;
            deleteButton.Margin = new Thickness(0, 10, 0, 0);
            deleteButton.Clicked += async (s, e) =>
            {
                // Удаление цели
                if (App.Current.Properties.ContainsKey("Goal"))
                {
                    App.Current.Properties.Remove("Goal");
                    await App.Current.SavePropertiesAsync();
                }
                await PopupNavigation.Instance.PopAsync();
            };

            layout.Children.Add(titleLabel);
            layout.Children.Add(goalEntry);
            layout.Children.Add(saveButton);
            layout.Children.Add(deleteButton);

            popup.Content = layout;
            await Navigation.PushPopupAsync(popup);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateExerciseValues();
        }
        private void ClearDB_Clicked(object sender, EventArgs e)
        {
            App.Database.ClearDatabase();
            DisplayAlert("Info", "Database kustatud", "OK");
            TrennidLabel.Text = "0";
            MinutesLabel.Text = "0";
            KaalLabel.Text = "0";
        }
    }

}
