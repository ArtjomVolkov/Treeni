using Plugin.SimpleAudioPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Treeni.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class muusika : ContentPage
    {
        private ISimpleAudioPlayer audioPlayer;

        public muusika()
        {
            InitializeComponent();
            audioPlayer = CrossSimpleAudioPlayer.Current;

            LoadPlaylist();
        }

        private void LoadPlaylist()
        {
            // Загрузка песен для плейлиста 1 и привязка к ListView
            var songs = new[]
            {
                new Song { Title = "I Kissed A Girl", CoverImage = "song1.jpg", FilePath = "song1.mp3" },
                new Song { Title = "Heads Will Roll", CoverImage = "song2.jpg", FilePath = "song2.mp3" },
                // Добавьте остальные песни плейлиста 1
            };

            PlaylistListView.ItemsSource = songs;
        }

        private void PlaySong(object sender, ItemTappedEventArgs e)
        {
            var song = e.Item as Song;
            audioPlayer.Load(song.FilePath);
            audioPlayer.Play();
        }

        private void PauseSong(object sender, EventArgs e)
        {
            if (audioPlayer.IsPlaying)
                audioPlayer.Pause();
            else
                audioPlayer.Play();
        }
    }

    public class Song
    {
        public string Title { get; set; }
        public string CoverImage { get; set; }
        public string FilePath { get; set; }
    }
}