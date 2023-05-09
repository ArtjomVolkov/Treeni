using SQLite;
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
    public partial class Plank : ContentPage
    {
        private DateTime _pageTime;
        private int curExer = 0;
        private List<(string, string, string)> _exercises = new List<(string, string, string)>
        {
            ("Põhiplank", "basicplank.png", "Võtke tavaline plangu asend nii, et küünarvarred on maas ja keha on sirgjooneliselt. Hoidke seda asendit 60 sekundit kuni minut või nii kaua kui võimalik."),
            ("Külgplank", "kulgplank.png", "Võtke plangu asend, kuid pöörake keha ühele küljele, toetades end ühe käe ja ühe jala küljega. Hoidke 60 sekundit, seejärel vahetage külgi."),
            ("Plangu tungraud", "plangu.png", "Võtke planguasend nii, et jalad on lähestikku. Hüppa oma jalad külgedele, seejärel tagasi kokku, säilitades samal ajal plangu asendi. Tehke seda 30 sekundit kuni minut."),
            ("Jalgade tõstmisega plank", "jalgplank.png", "Võtke plangu asend, seejärel tõstke üks jalg maast üles ja hoidke seda mõni sekund. Langetage jalg ja korrake seda teise jalaga. Jätkake vaheldumisi 30 sekundit kuni minut."),
            ("Õlakappidega plank", "kappplank.png", "Võtke plangu asend, seejärel tõstke üks käsi maast lahti ja koputage vastasõla. Langetage käsi ja korrake teise käega. Jätkake vaheldumisi 30 sekundit kuni minut."),
            ("Plangu puusa tõstmine", "puusaplank.png", "Asuge küünarvartele plangu asendisse. Tõstke üks verejalg üles, hoides verevarustust kannast peani. Hoidke jalga selles asendis paar sekundit, seejärel langetage ja korrake teisega. Jätkake jalgade vaheldumisi 30 sekundit kuni minut."),
            ("Plangu tõstmine", "plangut.png", "Seadke oma küünarvartele planguasendisse. Tõstke üks käsi üles, hoides ülejäänud tilka käest kannani. Laske end selles asendis mõni sekund olla. Jätkake käte vaheldumisi 30 sekundit kuni minut.")
        };

        private TimeSpan exerciseTimer = TimeSpan.FromSeconds(60);
        private TimeSpan CurTime = TimeSpan.Zero;
        private bool timer = false;
        public int duraction = 0;

        public Plank()
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
