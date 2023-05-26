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
            await Navigation.PushAsync(new Ple4i());
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
            // Выписываем все записи из таблицы Tren в базе данных
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

            var goalPicker = new Picker();
            goalPicker.Items.Add("Kõhulihaste treening");
            goalPicker.Items.Add("Rindkere treening");
            goalPicker.Items.Add("Käte treening");
            goalPicker.Items.Add("Jalgade treening");
            goalPicker.Items.Add("Õlgade treening");
            goalPicker.TextColor = Color.Black;

            // Получение цели и вывод ее в Picker
            if (App.Current.Properties.ContainsKey("Goal"))
            {
                var savedGoal = (string)App.Current.Properties["Goal"];
                goalPicker.SelectedItem = savedGoal;
            }

            var saveButton = new Button();
            saveButton.Text = "Salvesta eesmärk";
            saveButton.BackgroundColor = Color.White;
            saveButton.TextColor = Color.Black;
            saveButton.Margin = new Thickness(0, 20, 0, 0);
            saveButton.Clicked += (s, e) =>
            {
                // Сохранение выбранной цели
                var selectedGoal = goalPicker.SelectedItem.ToString();
                App.Current.Properties["Goal"] = selectedGoal;
                App.Current.SavePropertiesAsync();

                // Закрытие модального окна
                PopupNavigation.Instance.PopAsync();
            };

            layout.Children.Add(titleLabel);
            layout.Children.Add(goalPicker);
            layout.Children.Add(saveButton);

            popup.Content = layout;

            await PopupNavigation.Instance.PushAsync(popup);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateExerciseValues();
        }
        private void ClearDB_Clicked(object sender, EventArgs e)
        {
            App.Database.ClearDatabase();
            DisplayAlert("Info", "Puhastatud ajalugu", "OK");
            TrennidLabel.Text = "0";
            MinutesLabel.Text = "0";
            KaalLabel.Text = "0";
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new program7x4());
        }
    }

}
