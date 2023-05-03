using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Treeni.Models
{
    public class TrenRepos
    {
        SQLiteConnection database;
        public TrenRepos(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Tren>();
        }
        public IEnumerable<Tren> GetItems()
        {
            return database.Table<Tren>().ToList();
        }
        public Tren GetItem(int id)
        {
            return database.Get<Tren>(id);
        }
        public int DeleteItem(int id)
        {
            return database.Delete<Tren>(id);
        }
        public int SaveItem(Tren item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }
    }
}
