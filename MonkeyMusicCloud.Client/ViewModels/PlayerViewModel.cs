using MonkeyMusicCloud.Client.Events;
using MonkeyMusicCloud.Client.Utilities;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Utilities.Interface;

namespace MonkeyMusicCloud.Client.ViewModels
{
    public class PlayerViewModel : ViewModelBase
    {
        private int purcentagePlayed;
        private IMusicPlayer MusicPlayer { get { return MusicPlayerInstance.GetInstance().Player; } }
        
        public int PurcentagePlayed
        {
            get {
                return purcentagePlayed;
            }
            set {
                purcentagePlayed = value;
                RaisePropertyChanged("PurcentagePlayed");
            }
        }

        public PlayerViewModel()
        {
            EventsManager.PlaySong += PlaySong;
            MusicPlayer.PurcentagePlayed += delegate(int purcentage)
                                                {
                                                    PurcentagePlayed = purcentage;
                                                };

            MusicPlayer.SongFinished += EventsManager.InvokeCurrentSongFinished;
        }

        private void PlaySong(Song song)
        {
            MusicPlayer.Play(song.File.Content);
        }
    }
}