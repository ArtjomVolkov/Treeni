using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Treeni.Models
{
    public class UserRepos
    {
        private readonly SQLiteConnection databases;

        public UserRepos(string dbPath)
        {
            databases = new SQLiteConnection(dbPath);
            databases.CreateTable<UserSettings>();
        }
        public List<UserSettings> GetUserSettingsAsync()
        {
            return databases.Table<UserSettings>().ToList();
        }
        public void SaveUserSettingsAsync(UserSettings userSettings)
        {
            databases.Insert(userSettings);
        }
    }
}
