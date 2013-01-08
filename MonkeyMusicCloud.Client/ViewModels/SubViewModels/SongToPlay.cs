using MonkeyMusicCloud.Domain.Model;

namespace MonkeyMusicCloud.Client.ViewModels.SubViewModels
{
    public class SongToPlay : ViewModelBase
    {
        private Song song;
        private bool isPlaying;
        private bool played;

        public Song Song
        {
            get { return song; }
            set { 
                song = value;
                RaisePropertyChanged("Song");
            }
        }

        public bool IsPlaying
        {
            get { return isPlaying; }
            set
            {
                isPlaying = value;
                RaisePropertyChanged("IsPlaying");
            }
        }

        public bool Played
        {
            get { return played; }
            set
            {
                played = value;
                RaisePropertyChanged("Played");
            }
        }
    }
}