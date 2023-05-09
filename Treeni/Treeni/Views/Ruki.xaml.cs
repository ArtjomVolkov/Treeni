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
    public partial class Ruki : ContentPage
    {
        private DateTime _pageTime;
        private int curExer = 0;
        private List<(string, string, string)> _exercises = new List<(string, string, string)>
        {
            ("Pushup", "pushups.png", "Alustage keerutatud planguasendis peopesade ja sokkide toel. Langetage oma rinnakorv maapinna poole, seejärel pöörduge tagasi algasendisse. Korda 10-15 korda."),
            ("Diamond-pushup", "diamondpushup.png", "Võtke surumisasend, asetage peopesad üksteise peale, nii et nimetissõrmed ja pöidlad moodustavad teemantkuju. Langetage oma rinnakorv maapinna poole, seejärel pöörduge tagasi algasendisse. Korda 10-15 korda."),
            ("Ühe käe tõuked", "onearmpushup.png", "Alustage surumisasendis, üks käsi selili. Langetage oma rinnakorv maapinna poole, seejärel pöörduge tagasi algasendisse. Korda 5-10 korda mõlemal küljel."),
            ("Laiad tõuked", "widepushup.png", "Alustage surumisasendis, peopesad laiali, laiemad kui õlgade laius. Langetage oma rinnakorv maapinna poole, seejärel pöörduge tagasi algasendisse. Korda 10-15 korda."),
            ("Kätekõverdused kõrgel pinnal", "inclinedpushup.png", "Leidke pind, näiteks laud või batuut, ja asetage peopesad sellele. Liigutage jalad ruumi tagaosa poole, nii et selg oleks nurga all. Langetage oma rinnakorv pinna poole, seejärel pöörduge tagasi algasendisse. Korda 10-15 korda."),
            ("Tricep-Pushup", "triceppushup.png", "Võtke surumisasend Kitsa käe paigutusega. Langetage oma rinnakorv maapinna poole, seejärel pöörduge tagasi algasendisse. Korda 10-15 korda."),
            ("Mitmesuunalised tõuked", "staggeredpushup.png", "Võtke surumisasend, asetades ühe käe teisest veidi kõrgemale ja laiemale. Langetage küünarnukke painutades alla ja seejärel tagasi üles. Korda seda teisel küljel. Tehke 10-15 kordust mõlemal küljel."),
        };

        private TimeSpan exerciseTimer = TimeSpan.FromSeconds(60);
        private TimeSpan CurTime = TimeSpan.Zero;
        private bool timer = false;
        public int duraction = 0;

        public Ruki()
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
                CurTime -= TimeSpan.FromSeconds(1);
                TimerLabel.Text = CurTime.ToString(@"mm\:ss");

                if (CurTime.TotalSeconds <= 0)
                {
                    NextExercise();
                    return false;
                }

                return timer;
            });
            
        }

        private void EndTimerButton_Clicked(object sender, EventArgs e)
        {
            timer = false;
            StartBtn.IsEnabled = true;
            CurTime = exerciseTimer;
            TimerLabel.Text = CurTime.ToString(@"mm\:ss");
        }

        private async void NextExercise()
        {
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
                }
            }

        }

        private void NextExerciseButton_Clicked(object sender, EventArgs e)
        {
            NextExercise();
        }
    }
}