using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Treeni.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class program7x4 : ContentPage
    {
        List<string> esmasExercises = new List<string> { "Plank", "Kükk", "Punnid", "Ronimine", "Burpee" };
        List<string> teisiExercises = new List<string> { "Kõhulihaste harjutused", "Astumised", "Kangitõmbed", "Kõhulihased", "Hüppenöör" };
        List<string> kolmaExercises = new List<string> { "Punnid", "Kükk", "Kangitõmbed", "Plank", "Burpee" };
        List<string> neljaExercises = new List<string> { "Kõhulihaste harjutused", "Astumised", "Kõhulihased", "Ronimine", "Hüppenöör" };
        List<string> reedeExercises = new List<string> { "Plank", "Punnid", "Ronimine", "Kõhulihased", "Hüppenöör" };
        List<string> laulExercises = new List<string> { "Kõhulihaste harjutused", "Astumised", "Kangitõmbed", "Ronimine", "Burpee" };
        List<string> puhaExercises = new List<string> { "Punnid", "Kükk", "Kangitõmbed", "Plank", "Hüppenöör" };

        List<string> esmasExerciseImages = new List<string> { "plank.jpg", "squat.png", "pushups.png", "climbing.png", "burpees.png" };
        List<string> teisiExerciseImages = new List<string> { "crunches.png", "lunge.png", "pullups.png", "abs.png", "jumprope.png" };
        List<string> kolmaExerciseImages = new List<string> { "pushups.png", "squat.png", "pullups.png", "plank.jpg", "burpees.png" };
        List<string> neljaExerciseImages = new List<string> { "crunches.png", "lunge.png", "abs.png", "climbing.png", "jumprope.png" };
        List<string> reedeExerciseImages = new List<string> { "plank.jpg", "pushups.png", "climbing.png", "abs.png", "jumprope.png" };
        List<string> laupaevExerciseImages = new List<string> { "crunches.png", "lunge.png", "pullups.png", "climbing.png", "burpees.png" };
        List<string> puhaExerciseImages = new List<string> { "pushups.png", "squat.png", "pullups.png", "plank.jpg", "jumprope.png" };

        public program7x4()
        {
            InitializeComponent();
            Dictionary<string, List<string>> exercisesDictionary = new Dictionary<string, List<string>>
            {
                { "esmaspaev", esmasExercises },
                { "teisipaev", teisiExercises },
                { "kolmapaev", kolmaExercises },
                { "neljapaev", neljaExercises },
                { "reede", reedeExercises },
                { "laupaev", laulExercises },
                { "puhapaev", puhaExercises },
            };
            Dictionary<string, List<string>> exerciseImagesDictionary = new Dictionary<string, List<string>>
            {
                { "esmaspaev", esmasExerciseImages },
                { "teisipaev", teisiExerciseImages },
                { "kolmapaev", kolmaExerciseImages },
                { "neljapaev", neljaExerciseImages },
                { "reede", reedeExerciseImages },
                { "laupaev", laupaevExerciseImages },
                { "puhapaev", puhaExerciseImages },
            };

            // Создание 4 недель
            for (int i = 0; i < 4; i++)
            {
                var weekStackLayout = new StackLayout
                {
                    BackgroundColor = Color.White,
                    Margin = new Thickness(5),
                    Padding = new Thickness(10)
                };

                var weekLabel = new Label
                {
                    Text = $"Nädal {i + 1}",
                    FontFamily = "Sigmar.ttf#Sigmar",
                    TextColor = Color.Black,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    FontSize = Device.GetNamedSize(NamedSize.Title, typeof(Label))
                };
                weekStackLayout.Children.Add(weekLabel);

                var daysGrid = new Grid
                {
                    ColumnDefinitions =
                {
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                },
                    RowDefinitions =
                {
                    new RowDefinition(),
                }
                };

                weekStackLayout.Children.Add(daysGrid);
                int currentWeek = 0;
                string[] dayNames = { "esmaspaev", "teisipaev", "kolmapaev", "neljapaev", "reede", "laupaev", "puhapaev" };
                for (int j = 0; j < dayNames.Length; j++)
                {
                    Console.WriteLine("'{0}'", dayNames[j].ToString());

                    if (currentWeek == 0 && j > 0)
                    {
                        var emptyDayImage = new Image
                        {
                            BackgroundColor = Color.Transparent,
                            WidthRequest = 80,
                            HeightRequest = 60,
                            Margin = new Thickness(2, 20, 0, 0),
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Center,
                        };

                        Grid.SetColumn(emptyDayImage, j);
                        daysGrid.Children.Add(emptyDayImage);
                        continue;
                    }

                    var dayImage = new Image
                    {
                        Aspect = Aspect.AspectFit,
                        Source = $"{dayNames[j]}.png",
                        WidthRequest = 80,
                        HeightRequest = 60,
                        Margin = new Thickness(2, 20, 0, 0),
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center,
                    };

                    int index = j;

                    var tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Command = new Command(async () =>
                    {
                        string day = dayNames[index];
                        if (exercisesDictionary.ContainsKey(day) && exerciseImagesDictionary.ContainsKey(day))
                        {
                            var exercises = exercisesDictionary[day];
                            var exerciseImages = exerciseImagesDictionary[day];
                            await Navigation.PushAsync(new ExercisePage(exercises, exerciseImages));
                        }
                    });
                    dayImage.GestureRecognizers.Add(tapGestureRecognizer);
                    Grid.SetColumn(dayImage, j);
                    daysGrid.Children.Add(dayImage);
                    currentWeek++;
                }

                var scrollContentStackLayout = (StackLayout)((ScrollView)Content).Content;
                scrollContentStackLayout.Children.Add(new Frame
                {
                    BorderColor = Color.Black,
                    Padding = new Thickness(10),
                    Margin = new Thickness(5),
                    Content = weekStackLayout
                });
            }
        }
    }
}