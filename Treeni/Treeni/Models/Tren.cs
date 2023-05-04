using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Treeni.Models
{
    [Table("Tren")]
    public class Tren
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public int Trennid { get; set; }
        public int Kaal { get; set; }
        public int Minutes { get; set; }
    }
}
