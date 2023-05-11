using System;
using Treeni.Models;
using Xamarin.Forms;
using Treeni.Views;
using Xamarin.Forms.Xaml;
using System.IO;

namespace Treeni
{
    public partial class App : Application
    {
        public static UserRepos databases;
        public static UserRepos Databases
        {
            get
            {
                if (databases == null)
                {
                    databases = new UserRepos(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "user.db3"));
                }
                return databases;
            }
        }

        public static TrenRepos database;
        public static TrenRepos Database
        {
            get
            {
                if (database == null)
                {
                    database = new TrenRepos(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "treen.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MyMasterPage());
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
