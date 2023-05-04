using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Treeni.Models;
using Treeni.Views;
using Xamarin.Forms;

namespace Treeni
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }


        private async void Ple4i_DC(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page());
        }
        private async void Plank_Dc(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Plank());
        }
        private async void Grud_Dc(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page());
        }
        private async void Ruki_Dc(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page());
        }
        private async void Nogi_Dc(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page());
        }
    }
}
