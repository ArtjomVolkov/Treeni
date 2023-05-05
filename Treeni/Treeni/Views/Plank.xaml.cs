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
        private int _currentExercise = 0;
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

        private TimeSpan _exerciseDuration = TimeSpan.FromSeconds(60);
        private TimeSpan _currentTime = TimeSpan.Zero;
        private bool _timerRunning = false;

        public Plank()
        {
            InitializeComponent();
            BindingContext = this;

            _currentExercise = 0;
            ExerciseName.Text = _exercises[_currentExercise].Item1;
            ExerciseImage.Source = ImageSource.FromFile(_exercises[_currentExercise].Item2);
            ExerciseDescription.Text = _exercises[_currentExercise].Item3;

            _currentTime = _exerciseDuration;
            TimerLabel.Text = _currentTime.ToString(@"mm\:ss");
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
            _currentExercise++;
            if (_currentExercise >= _exercises.Count)
            {
                _timerRunning = false;
                _currentExercise = 0;
                await DisplayAlert("Palju õnne!", "Olete kõik harjutused täitnud.", "OK");
                int Kaal = 0;
                int Minutes = 0;
                int Trennid = 0;
                Tren exercise = new Tren
                {
                    Kaal = Kaal + 200,
                    Minutes = Minutes + 25, 
                    Trennid = Trennid + 1 
                };
                App.Database.UpdateExercise(exercise);
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
