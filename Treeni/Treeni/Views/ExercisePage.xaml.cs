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
    public partial class ExercisePage : ContentPage
    {
        private List<string> exercises;
        private List<string> exerciseImages;

        public ExercisePage(List<string> exercises, List<string> exerciseImages)
        {
            InitializeComponent();
            this.exercises = exercises;
            this.exerciseImages = exerciseImages;

            var scrollView = new ScrollView();

            var exerciseStackLayout = new StackLayout
            {
                Margin = new Thickness(10)
            };

            for (int i = 0; i < exercises.Count; i++)
            {
                var exerciseLabel = new Label
                {
                    Text = exercises[i],
                    FontSize = Device.GetNamedSize(NamedSize.Title, typeof(Label)),
                    FontFamily = "Sigmar.ttf#Sigmar",
                    Margin = new Thickness(0, 0, 0, 10)
                };
                exerciseStackLayout.Children.Add(exerciseLabel);

                var exerciseImage = new Image
                {
                    Aspect = Aspect.AspectFit,
                    Source = exerciseImages[i],
                    Margin = new Thickness(0, 0, 0, 10)
                };
                exerciseStackLayout.Children.Add(exerciseImage);
            }

            scrollView.Content = exerciseStackLayout;

            Content = scrollView;
        }
    }
}