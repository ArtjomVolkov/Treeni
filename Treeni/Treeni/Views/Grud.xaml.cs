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
        private DateTime _pageTime;
        private int curExer = 0;
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

        private TimeSpan exerciseTimer = TimeSpan.FromSeconds(60);
        private TimeSpan CurTime = TimeSpan.Zero;
        private bool timer = false;
        public int duraction = 0;

        public Grud()
        {
            InitializeComponent();
            BindingContext = this;

            curExer = 0;
            ExerciseName.Text = _exercises[curExer].Item1;
            ExerciseImage.Source = ImageSource.FromFile(_exercises[curExer].Item2);
            ExerciseDescription.Text = _exercises[curExer].Item3;

            CurTime = exerciseTimer;
            TimerLabel.Text = CurTime.ToString(@"mm\:ss");
            _pageTime = DateTime.Now;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _pageTime = DateTime.Now;
        }
        private void StartTimerButton_Clicked(object sender, EventArgs e)
        {
            StartBtn.IsEnabled = false;
            timer = true;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (CurTime.TotalSeconds <= 0)
                {
                    NextExercise();
                    return false;
                }

                CurTime -= TimeSpan.FromSeconds(1);
                TimerLabel.Text = CurTime.ToString(@"mm\:ss");

                return timer;
            });

        }

        private void EndTimerButton_Clicked(object sender, EventArgs e)
        {
            timer = false;
            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Load("bud.mp3");
            player.Stop();
            CurTime = exerciseTimer;
            TimerLabel.Text = CurTime.ToString(@"mm\:ss");
            StartBtn.IsEnabled = true;
        }

        private async void NextExercise()
        {
            timer = false;
            StartBtn.IsEnabled = true;
            TimerLabel.Text = CurTime.ToString(@"mm\:ss");
            var pageLeavingTime = DateTime.Now;
            duraction = (int)pageLeavingTime.Subtract(_pageTime).TotalSeconds;
            Console.WriteLine("Time: " + duraction + " minutes");
            curExer++;
            if (curExer >= _exercises.Count)
            {
                timer = false;
                curExer = 0;
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
                ExerciseName.Text = _exercises[curExer].Item1;
                ExerciseImage.Source = ImageSource.FromFile(_exercises[curExer].Item2);
                ExerciseDescription.Text = _exercises[curExer].Item3;
                CurTime = exerciseTimer;
                TimerLabel.Text = CurTime.ToString(@"mm\:ss");

                if (timer)
                {
                    var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                    player.Load("bud.mp3");
                    player.Play();
                    StartBtn.IsEnabled = true;
                }
            }

        }

        private void NextExerciseButton_Clicked(object sender, EventArgs e)
        {
            NextExercise();
        }
    }
}