using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            ("Exercise 1", "exercise1.png", "Description of exercise 1."),
            ("Exercise 2", "exercise2.png", "Description of exercise 2."),
            ("Exercise 3", "exercise3.png", "Description of exercise 3."),
            ("Exercise 4", "exercise4.png", "Description of exercise 4."),
            ("Exercise 5", "exercise5.png", "Description of exercise 5."),
            ("Exercise 6", "exercise6.png", "Description of exercise 6."),
            ("Exercise 7", "exercise7.png", "Description of exercise 7.")
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
                await DisplayAlert("Congratulations!", "You have completed all exercises.", "OK");
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
