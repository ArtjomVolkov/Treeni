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
    public partial class Nogi : ContentPage
    {
        private DateTime _pageTime;
        private int curExer = 0;
        private List<(string, string, string)> _exercises = new List<(string, string, string)>
        {
            ("Kükid", "squat.png", "Seisa sirgelt, jalad õlgade laiuselt laiali, käed tõsta ettepoole. Alusta aeglast kükki, painutades põlvi ja langetades vaagnat, kuni reied on paralleelsed põrandaga. Seejärel tõuse aeglaselt tagasi algasendisse. Korda 10-15 korda."),
            ("Astepulgad", "lunge.png", "Seisa sirgelt, aseta üks jalg teise ette umbes 1,5-2 jalalaba kaugusele. Astu edasi, painutades põlvi, kuni esijalg on 90-kraadise nurga all. Seejärel tõuse aeglaselt tagasi algasendisse. Korda 10-15 korda mõlemal jalal."),
            ("Päkakõnd", "calfraise.png", "Seisa sirgelt, jalad õlgade laiuselt laiali, käed vöökohal. Tõsta kontsad üles, püüdes hoida keha sirge, hoia sekund aeglaselt üleval ja lase aeglaselt alla. Korda 10-15 korda."),
            ("Jalalöögid", "legswing.png", "Seisa sirgelt, toetudes seina või tooli külge tasakaalu saavutamiseks. Alusta aeglast ühe jala õõtsumist edasi-tagasi, hoides jalga sirgena, kuid mitte liiga tugevasti. Korda 10-15 korda mõlemal jalal."),
            ("Küljeastungid", "sidelunge.png", "Seisa sirgelt, jalad õlgade laiuselt laiali. Astu suure sammuga küljele ja langeta keha, painutades ühte jalga põlves ja sirutades teist jalga. Tule tagasi algasendisse ja korda teisel poolel. Korda 10-15 korda mõlemal poolel."),
            ("Ühe jala kükid", "singlelegsquat.png", "Seisa ühel jalal, teine jalg ettepoole tõstetud. Kallutu ettepoole, painutades töötava jala põlve ja lastes vaagna alla, kuni reie on paralleelne põrandaga. Erinevalt tavalistest kükkidest pole siin vaja mõlemale jalale laskuda. Korda 10-15 korda mõlemal jalal."),
            ("Surnud tõmme", "deadlift.png", "Võta hantlid või veepudelid kätte, seistes õlgade laiuses. Kallutu ettepoole, painutades põlvi ja reisi ning aseta hantlid või pudelid põrandale. Seejärel tõuse aeglaselt tagasi algasendisse. Korda 10-15 korda.")
        };

        private TimeSpan exerciseTimer = TimeSpan.FromSeconds(60);
        private TimeSpan CurTime = TimeSpan.Zero;
        private bool timer = false;
        public int duraction = 0;

        public Nogi()
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
            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Load("bud.mp3");
            player.Stop();
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