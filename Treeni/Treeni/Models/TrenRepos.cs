using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Treeni.Models
{
    public class TrenRepos
    {
        private readonly SQLiteConnection database;

        public TrenRepos(string dbPath)
        {
            database = new SQLiteConnection(dbPath);
            database.CreateTable<Tren>();
        }
        public List<Tren> GetAllExercises()
        {
            return database.Table<Tren>().ToList();
        }

        public void AddExercise(Tren exercise)
        {
            database.Insert(exercise);
        }

        public void UpdateExercise(Tren exercise)
        {
            database.Update(exercise);
        }

        public void DeleteExercise(int id)
        {
            database.Delete<Tren>(id);
        }
        public void ClearDatabase()
        {
            database.DropTable<Tren>();
            database.CreateTable<Tren>();
        }
    }
}
