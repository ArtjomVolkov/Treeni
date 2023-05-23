using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
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
    public partial class Seaded : ContentPage
    {

        public Seaded()
        {
            InitializeComponent();

            UpdatePageData();
        }

        private async void UpdatePageData()
        {
            // Load user settings from SQLite database
            List<UserSettings> userSettingsList = App.Databases.GetUserSettingsAsync();
            UserSettings userSettings = null;
            if (userSettingsList.Count > 0)
            {
                userSettings = userSettingsList[0];
            }
            else
            {
                userSettings = new UserSettings();
            }

            // Bind user settings to the page's controls
            nameEntry.Text = userSettings.Name;
            genderPicker.SelectedItem = userSettings.Gender;
            birthdayDatePicker.Date = userSettings.Birthday;
            emailEntry.Text = userSettings.Email;
            telEntry.Text = userSettings.Telefon;
        }


        private async void OnPrivacyPolicyClicked(object sender, EventArgs e)
        {
            //модальное окно
            var popup = new PopupPage();
            popup.WidthRequest = 100;
            popup.BackgroundColor = Color.FromHex("#80000000");
            popup.HasSystemPadding = true;
            
            //stacklayout
            var layout = new StackLayout();
            layout.BackgroundColor = Color.White;
            layout.VerticalOptions = LayoutOptions.CenterAndExpand;
            layout.HorizontalOptions = LayoutOptions.CenterAndExpand;
            layout.Padding = new Thickness(20);
            //lbl
            var titleLabel = new Label();
            titleLabel.Text = "Meie rakenduse andmete privaatsus ja turvalisus on meile väga olulised. Selles dokumendis kirjeldame, milliseid andmeid me kogume, kuidas me neid kasutame ja kuidas me neid kaitseme.\r\n\r\nKogumine:\r\nKogume ainult vajalikke andmeid, mille esitate vabatahtlikult meie rakenduse kasutamisel. Need andmed võivad sisaldada teie nime, sugu, sünnikuupäeva ja meie rakenduse hinnanguid. Me ei kogu mingeid isikuandmeid, välja arvatud need, mida te meile esitate.\r\n\r\nAndmete kasutamine:\r\nMe kasutame kogutud andmeid ainult eesmärkidel, mis on seotud meie rakenduse täiustamisega ja teile parima võimaliku kasutuskogemuse pakkumisega. Me ei jaga, müü ega vaheta teie isikuandmeid kolmandate osapoolte ettevõtetega.\r\n\r\nAndmekaitse:\r\nMe võtame kõik vajalikud meetmed, et kaitsta teie andmeid volitamata juurdepääsu, muutmise, avalikustamise või hävitamise eest. Kasutame kaasaegseid krüptimismeetodeid ja andmete salvestamist turvalistes serverites.\r\n\r\nPrivaatsuspoliitika muudatused:\r\nMe võime aeg-ajalt oma privaatsuspoliitikat muuta. Juhul, kui teeme mingeid muudatusi, teavitame teid sellest, avaldades uuendatud privaatsuspoliitika meie rakenduses.\r\n\r\nKui teil on küsimusi meie privaatsuspoliitika kohta, võtke meiega ühendust.";
            titleLabel.TextColor = Color.Black;
            titleLabel.FontSize = 20;
            titleLabel.WidthRequest = 250;
            titleLabel.HeightRequest = 500;
            titleLabel.HorizontalOptions = LayoutOptions.Center;
            layout.Children.Add(titleLabel);

            popup.Content = layout;
            await Navigation.PushPopupAsync(popup);
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            
            // Update user settings with the data entered on the page
            UserSettings userSettings = new UserSettings();
            userSettings.Name = nameEntry.Text;
            userSettings.Gender = genderPicker.SelectedItem as string;
            userSettings.Birthday = birthdayDatePicker.Date;
            userSettings.Email = emailEntry.Text;
            userSettings.Telefon = telEntry.Text;
            App.Databases.DeleteUserSettings();
            // Save user settings to SQLite database
            App.Databases.SaveUserSettingsAsync(userSettings);

            await DisplayAlert("Saved", "Your settings have been saved.", "OK");
            UpdatePageData();
        }
    }
}