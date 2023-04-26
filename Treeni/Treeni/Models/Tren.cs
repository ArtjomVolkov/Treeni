using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Treeni.Models
{
    public class Tren
    {
        public string Trennid { get; set; }
        public string Kaal { get; set; }
        public string Minutes { get; set; }
        public Image image { get; set; }
        public string Description { get; set; }
        public TimeSpan timer { get; set; }
        public string execrise { get; set; }
    }
}
