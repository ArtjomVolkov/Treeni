using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Treeni.Models
{
    [Table("UserSettings")]
    public class UserSettings
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public DateTime Birthday { get; set; }

        public string Email { get; set; }

        public string Telefon { get; set; }
    }
}
