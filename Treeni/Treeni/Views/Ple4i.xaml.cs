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
    public partial class Ple4i : ContentPage
    {
        private DateTime _pageTime;
        private int curExer = 0;
        private List<(string, string, string)> _exercises = new List<(string, string, string)>
        {
            ("Hantli kõrvalepress ühe käega", "dumbbelllateralraise.png", "Seisa sirgelt, üks hantel käes. Tõsta hantlit käega küljele, hoides käsi sirgena kuni õlgade kõrguseni. Seejärel aeglaselt langeta tagasi alla. Korda 10-15 korda mõlemal käel."),
            ("Push-up", "pushup.png", "Aseta käed õlgade laiusele, kõhulihased pinges ja keha sirgena. Alusta aeglast käte painutamist, kuni rinnus puudutab põrandat. Seejärel aeglaselt tõuse tagasi algasendisse. Korda 10-15 korda."),
            ("Pikendused veepudelitega", "waterbottleoverheadextensions.png", "Seisa sirgelt, käes üks või kaks veepudelit. Tõsta pudel(id) kätega üle pea, hoides käed sirgena. Seejärel aeglaselt langeta pudel(id) tagasi alla. Korda 10-15 korda."),
            ("Kätekõverdused seina vastu", "wallpushups.png", "Seisa seina vastu, käed õlgade kõrgusel. Alusta aeglast käte painutamist, kuni rinnus puudutab seina. Seejärel aeglaselt tõuse tagasi algasendisse. Korda 10-15 korda."),
            ("Ühe käe hantli tõstmine ettepoole", "dumbbellfrontraise.png", "Seisa sirgelt, üks hantel käes. Tõsta hantel käega ettepoole, hoides käsi sirgena kuni õlgade kõrguseni. Seejärel aeglaselt langeta tagasi alla. Korda 10-15 korda mõlemal käel."),
            ("Hantli kõrvalepress kahe käega", "dumbbelllateralraises.png", "Seisa sirgelt, üks hantel mõlemas käes. Tõsta hantlid kätega küljele, hoides käed sirgena kuni õlgade kõrguseni. Seejärel aeglaselt langeta tagasi alla. Korda 10-15 korda."),
            ("Hantli tõstmine õlgadele", "dumbbellshoulderpress.png", "Seisa sirgelt, üks hantel mõlemas käes õlgade kõrgusel. Tõsta hantlid üles, surudes käed sirgena üle pea. Seejärel aeglaselt langeta tagasi alla. Korda 10-15 korda.")
        };

        private TimeSpan exerciseTimer = TimeSpan.FromSeconds(60);
        private TimeSpan CurTime = TimeSpan.Zero;
        private bool timer = false;
        public int duraction = 0;

        public Ple4i()
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