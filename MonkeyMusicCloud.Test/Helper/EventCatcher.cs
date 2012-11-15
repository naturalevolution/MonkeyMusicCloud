using System.Collections.ObjectModel;
using MonkeyMusicCloud.Client.Events;
using MonkeyMusicCloud.Domain.Model;

namespace MonkeyMusicCloud.Test.Helper
{
    public class EventCatcher
    {
        public EventCatcher()
        {
            EventsManager.AddToPlayList += delegate(ObservableCollection<Song> songs) { 
                AddToPlayListInvoked = true;
                AddToPlayListSongs = songs;
            };

            EventsManager.PlaySong += delegate(Song song)
            {
                PlaySongInvoked = true;
                SongToPlay = song;
            };
        }

        public ObservableCollection<Song> AddToPlayListSongs { get; set; }
        public Song SongToPlay { get; set; }
        public bool AddToPlayListInvoked;
        public bool PlaySongInvoked;
    }
}
