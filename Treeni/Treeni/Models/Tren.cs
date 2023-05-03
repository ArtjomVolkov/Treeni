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
        public string Trennid { get; set; }
        public string Kaal { get; set; }
        public string Minutes { get; set; }
        public ImageSource image { get; set; }
        public string Description { get; set; }
        public TimeSpan timer { get; set; }
        public string execrise { get; set; }
    }
}
