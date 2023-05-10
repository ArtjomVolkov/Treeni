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
    public partial class Youtube : ContentPage
    {
        private List<(string, string, string)> youtubers = new List<(string, string, string)>
        {
            ("FitnessBlender", "fitnessBlender.png", "Kanal, kus on üle 600 treeningvideo õpetuse erinevatel teemadel ja raskusastmetel."),
            ("Popsugar", "Popsugar.png", "Fitness-sellelt kanalilt leiate palju huvitavaid tervise-ja spordivideoid."),
            ("BeFit", "BeFit.png", "Kanal, kus on lai valik erineva intensiivsuse ja fookusega treeningprogramme."),
            ("Jooga Adriene", "JoogaAdriene.png", "Iga on huvitav ja kasulik kanal kõigi treeningutasemete joogasõpradele."),
            ("HASfit", "HASfit.png", "See pakub palju tasuta treeningvideo õpetusi, samuti soovitusi toitumise ja tervisliku eluviisi kohta."),
            ("Blogilates", "Blogilates.png", "Spordikanal, millel on oma ainulaadsed treeningprogrammid."),
            ("Body Project", "BodyProject.png", "Sellel kanalil on mitmesuguseid treeninguid algajatele ja kogenud sportlastele ning soovitusi õige toitumise ja tervisemurede kohta."),
            ("Tone It Up", "ToneItUp.png", "Veel üks suurepärane kanal neile, kes soovivad hoolitseda oma tervise ja figuuri eest."),
            ("Fitness Marshall", "FitnessMarshall.png", "Tantsutundidega kanal, mis aitab teil treeningut mitmekesistada ja meeleolu tõsta."),
            ("MadFit", "MadFit.png", "Uus kanal, millel on lühikesed, kuid tõhusad treeningud kõigile lihasrühmadele.")
        };
        List<string> tekstid = new List<string> { "FitnessBlender", "Popsugar", "BeFit", "Jooga Adriene", "HASfit", "Blogilates", "Body Project", "DTone It Up", "Fitness Marshall", "MadFit" };
        public Youtube()
        {
            InitializeComponent();
            StackLayout st = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.Beige,
            };
            for (int i = 0; i < youtubers.Count; i++)
            {
                Label lblName = new Label
                {
                    Text = tekstid[i],
                    TextColor= Color.Black,
                };
                Image image = new Image
                {
                    Source = ImageSource.FromFile(tekstid[i]),
                    WidthRequest= 200,
                    HeightRequest= 200,
                };
                Label lbldesc = new Label
                {
                    Text = tekstid[i],
                    TextColor = Color.Black,
                };
                st.Children.Add(lblName);
                st.Children.Add(image);
                st.Children.Add(lbldesc);
            }
            ScrollView scrollView = new ScrollView { Content = st };
        }
    }
}