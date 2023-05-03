using System;
using Treeni.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace Treeni
{
    public partial class App : Application
    {
        public static TrenRepos database;
        public static TrenRepos Database
        {
            get
            {
                if (database == null)
                {
                    database = new TrenRepos(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "treeni.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
