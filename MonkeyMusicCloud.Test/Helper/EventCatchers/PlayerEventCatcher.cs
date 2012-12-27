using System.Collections.ObjectModel;
using MonkeyMusicCloud.Client.Observers;
using MonkeyMusicCloud.Domain.Model;

namespace MonkeyMusicCloud.Test.Helper.EventCatchers
{
    public class PlayerEventCatcher
    {
        public PlayerEventCatcher()
        {
            PlayerObserver.AddToPlayList += delegate(ObservableCollection<Song> song) { 
                AddToPlayListInvoked = true;
                AddToPlayListSong = song;
            };

            PlayerObserver.PlaySong += delegate(Song song)
            {
                PlaySongInvoked = true;
                SongToPlay = song;
            };

            PlayerObserver.StopSong += delegate()
            {
                StopSongInvoked = true;
            };

            
            PlayerObserver.CurrentSongFinished += delegate()
            {
                SongFinishedInvoked = true;
            };


            PlayerObserver.PauseSong += delegate()
            {
                PauseSongInvoked = true;
            };


            PlayerObserver.ResumeSong += delegate()
            {
                ResumeSongInvoked = true;
            };

          
        }


        public ObservableCollection<Song> AddToPlayListSong { get; set; }
        public Song SongToPlay { get; set; }

        public bool AddToPlayListInvoked = false;
        public bool PlaySongInvoked = false;
        public bool ResumeSongInvoked = false;
        public bool SongFinishedInvoked = false;
        public bool PauseSongInvoked = false;
        public bool ChangeContentViewInvoked = false;
        public bool NewSearchInvoked = false;
        public bool StopSongInvoked = false;
    }
}
