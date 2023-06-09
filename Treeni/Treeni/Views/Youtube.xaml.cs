﻿using System;
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
    public partial class Youtube : ContentPage
    {
        private List<(string, string, string, string)> youtubers = new List<(string, string, string, string)>
        {
            ("FitnessBlender", "fitnessblender.png", "https://www.youtube.com/user/FitnessBlender", "Kanal, kus on üle 600 treeningvideo õpetuse erinevatel teemadel ja raskusastmetel."),
            ("Popsugar", "popsugar.png", "https://www.youtube.com/user/popsugartvfit", "Fitness-sellelt kanalilt leiate palju huvitavaid tervise-ja spordivideoid."),
            ("BeFit", "befit.png", "https://www.youtube.com/user/BeFit", "Kanal, kus on lai valik erineva intensiivsuse ja fookusega treeningprogramme."),
            ("Jooga Adriene", "joogaadriene.png", "https://www.youtube.com/user/yogawithadriene", "Iga on huvitav ja kasulik kanal kõigi treeningutasemete joogasõpradele."),
            ("HASfit", "hasfit.png", "https://www.youtube.com/user/KozakSportsPerform", "See pakub palju tasuta treeningvideo õpetusi, samuti soovitusi toitumise ja tervisliku eluviisi kohta."),
            ("Blogilates", "blogilates.png", "https://www.youtube.com/user/blogilates", "Spordikanal, millel on oma ainulaadsed treeningprogrammid."),
            ("Body Project", "bodyproject.png", "https://www.youtube.com/user/BodyProjectFitness", "Sellel kanalil on mitmesuguseid treeninguid algajatele ja kogenud sportlastele ning soovitusi õige toitumise ja tervisemurede kohta."),
            ("Tone It Up", "toneitup.png", "https://www.youtube.com/user/ToneItUpcom", "Veel üks suurepärane kanal neile, kes soovivad hoolitseda oma tervise ja figuuri eest."),
            ("Fitness Marshall", "fitnessmarshall.png", "https://www.youtube.com/user/TheFitnessMarshall", "Tantsutundidega kanal, mis aitab teil treeningut mitmekesistada ja meeleolu tõsta."),
            ("MadFit", "madfit.png", "https://www.youtube.com/channel/UCpQ34afVgk8cRQBjSJ1xuJQ", "Uus kanal, millel on lühikesed, kuid tõhusad treeningud kõigile lihasrühmadele.")
        };
        public Youtube()
        {
            InitializeComponent();

            StackLayout st = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.Gray
            };

            for (int i = 0; i < youtubers.Count; i++)
            {
                Label lblName = new Label
                {
                    Text = youtubers[i].Item1,
                    FontSize = Device.GetNamedSize(NamedSize.Title, typeof(Label)),
                    TextColor = Color.Black,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    Margin=30,
                };

                Image image = new Image
                {
                    Source = ImageSource.FromFile(youtubers[i].Item2),
                    WidthRequest = 200,
                    HeightRequest = 200,
                };
                Label lbldesc = new Label
                {
                    Text = youtubers[i].Item4,
                    TextColor = Color.Black,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    WidthRequest = 250,
                };
                var tapGesture = new TapGestureRecognizer();
                int index = i; // Store the current index for the event handler
                tapGesture.Tapped += (s, e) => {
                    // Open the YouTube channel in a browser
                    Device.OpenUri(new Uri(youtubers[index].Item3));
                };
                image.GestureRecognizers.Add(tapGesture);
                st.Children.Add(lblName);
                st.Children.Add(image);
                st.Children.Add(lbldesc);
            }

            ScrollView scrollView = new ScrollView { Content = st };
            Content = scrollView;
        }
    }
}