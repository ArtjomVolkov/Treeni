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
    public partial class Grud : ContentPage
    {
        private DateTime _pageAppearingTime;
        private int _currentExercise = 0;
        private List<(string, string, string)> _exercises = new List<(string, string, string)>
        {
            ("Tavaline kätekõverdus", "pushup.png", "Asetage käed õlgade laiusele. Langetage keha kätekõverdustega põrandale, kuni rind puudutab seda peaaegu. Tõstke keha tagasi algasendisse. Tehke seda 30 sekundit kuni minut."),
            ("Plangukõnnak", "wallpushup.png", "Asetage keha planguasendisse, käed õlgade all ja keha sirge. Kõnni kätega edasi ja tagasi, hoides keha paigal. Tehke seda 30 sekundit kuni minut."),
            ("Käsivarte pumpamine", "dumbbellpress.png", "Asetage käed toolile või lauale, hoides keha sirge. Painutage käsi ja langetage keha toolile või lauale, kuni rind puudutab seda peaaegu. Tõstke keha tagasi algasendisse. Tehke seda 30 sekundit kuni minut."),
            ("Hantli surumine lamades", "cheststretch.png", "Lamage selili ja hoidke hantleid rinnal. Tõstke hantlid üles, surudes neid rinnale, ja seejärel langetage tagasi algasendisse. Tehke seda 30 sekundit kuni minut."),
            ("Hüppekätekõverdused", "dumbbellfly.png", "Asetage käed õlgade laiusele ja hüpake kätekõverdustesse. Hüpake tagasi algasendisse ja korrake. Tehke seda 30 sekundit kuni minut."),
            ("Kurvides hantlite surumine", "walljump.png", "Istuge toolil, hoides hantleid õlgade kõrgusel. Painutage käsi, tõstke hantlid üles ja suruge need tagasi algasendisse. Tehke seda 30 sekundit kuni minut."),
            ("Plangukätekõverdused", "diver.png", "Asetage keha planguasendisse, käed õlgade all ja keha sirge. Painutage käsi ja langetage keha kätekõverdustega põrandale, seejärel tõstke tagasi algasendisse. Tehke seda 30 sekundit kuni minut.")
         };

        private TimeSpan _exerciseDuration = TimeSpan.FromSeconds(60);
        private TimeSpan _currentTime = TimeSpan.Zero;
        private bool _timerRunning = false;
        public int duraction = 0;

        public Grud()
        {
            InitializeComponent();
            BindingContext = this;

            _currentExercise = 0;
            ExerciseName.Text = _exercises[_currentExercise].Item1;
            ExerciseImage.Source = ImageSource.FromFile(_exercises[_currentExercise].Item2);
            ExerciseDescription.Text = _exercises[_currentExercise].Item3;

            _currentTime = _exerciseDuration;
            TimerLabel.Text = _currentTime.ToString(@"mm\:ss");
            _pageAppearingTime = DateTime.Now;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _pageAppearingTime = DateTime.Now;
        }
        private void StartTimerButton_Clicked(object sender, EventArgs e)
        {
            _timerRunning = true;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                _currentTime -= TimeSpan.FromSeconds(1);
                TimerLabel.Text = _currentTime.ToString(@"mm\:ss");

                if (_currentTime.TotalSeconds <= 0)
                {
                    NextExercise();
                    return false;
                }

                return _timerRunning;
            });
        }

        private void EndTimerButton_Clicked(object sender, EventArgs e)
        {
            _timerRunning = false;
            _currentTime = _exerciseDuration;
            TimerLabel.Text = _currentTime.ToString(@"mm\:ss");
        }

        private async void NextExercise()
        {
            var pageLeavingTime = DateTime.Now;
            duraction = (int)pageLeavingTime.Subtract(_pageAppearingTime).TotalSeconds;
            Console.WriteLine("Time: " + duraction + " minutes");
            _currentExercise++;
            if (_currentExercise >= _exercises.Count)
            {
                _timerRunning = false;
                _currentExercise = 0;
                await DisplayAlert("Palju õnne!", "Olete kõik harjutused täitnud.", "OK");
                int Kaal = duraction * 7;
                int Trennid = 1;
                Tren exercise = new Tren
                {
                    Kaal = Kaal,
                    Minutes = duraction,
                    Trennid = Trennid
                };
                App.Database.AddExercise(exercise);
                await Navigation.PopAsync();
            }
            else
            {
                ExerciseName.Text = _exercises[_currentExercise].Item1;
                ExerciseImage.Source = ImageSource.FromFile(_exercises[_currentExercise].Item2);
                ExerciseDescription.Text = _exercises[_currentExercise].Item3;
                _currentTime = _exerciseDuration;
                TimerLabel.Text = _currentTime.ToString(@"mm\:ss");

            }

        }

        private void NextExerciseButton_Clicked(object sender, EventArgs e)
        {
            NextExercise();
        }
    }
}