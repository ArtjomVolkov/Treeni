using System;
using System.Collections.Generic;
using System.Linq;
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
        public program7x4()
        {
            InitializeComponent();

            // Создание 4 недель
            for (int i = 0; i < 4; i++)
            {
                // Создание нового стекового макета
                var weekStackLayout = new StackLayout
                {
                    BackgroundColor = Color.White,
                    Margin = new Thickness(5),
                    Padding = new Thickness(10)
                };

                // Создание заголовка недели
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

                // Создание таблицы с днями недели
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
                    new ColumnDefinition()
                },
                    RowDefinitions =
                {
                    new RowDefinition()
                }
                };
                weekStackLayout.Children.Add(daysGrid);

                // Создание дней недели
                string[] dayNames = { "monday", "tuesday", "wensday", "neljapaev", "reede", "laupaev", "puhapaev" };
                for (int j = 0; j < dayNames.Length; j++)
                {
                    var dayImage = new Image
                    {
                        Aspect = Aspect.AspectFit,
                        Source = $"{dayNames[j]}.png",
                        WidthRequest = 80,
                        HeightRequest = 50,
                        Margin = new Thickness(0, 20, 0, 0),
                        VerticalOptions = LayoutOptions.Start
                    };
                    Grid.SetColumn(dayImage, j);
                    daysGrid.Children.Add(dayImage);
                }

                // Добавление стекового макета в область прокрутки
                var scrollContentStackLayout = (StackLayout)Content;
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