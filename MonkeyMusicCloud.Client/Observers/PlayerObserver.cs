using System.Collections.ObjectModel;
using MonkeyMusicCloud.Domain.Model;

namespace MonkeyMusicCloud.Client.Observers
{
    /// <summary>
    /// TODO : Surement mieux à faire
    /// Classe fourre-tout, mais permet de s'abstraire d'une relation entre les vueModel. 
    /// Permet aussi de pouvoir partager un event commun
    /// </summary>
    public class PlayerObserver
    {
        public static event AddToPlayListHandler AddToPlayList;
        public static void NotifyAddToPlayList(Song song)
        {
            AddToPlayListHandler handler = AddToPlayList;
            if (handler != null) handler(song);
        }

        public static event PlaySongHandler PlaySong;
        public static void NotifyPlayNewSong(Song song)
        {
            PlaySongHandler handler = PlaySong;
            if (handler != null) handler(song);
        }

        public static event PauseSongHandler PauseSong;
        public static void NotifyPauseSong()
        {
            PauseSongHandler handler = PauseSong;
            if (handler != null) handler();
        }

        public static event CurrentSongFinishedHandler CurrentSongFinished;
        public static void NotifyCurrentSongFinished()
        {
            CurrentSongFinishedHandler handler = CurrentSongFinished;
            if (handler != null) handler();
        }


        public static event ResumeSongHandler ResumeSong;
        public static void NotifyResumeSong()
        {
            ResumeSongHandler handler = ResumeSong;
            if (handler != null) handler();
        }
    }

    public delegate void CurrentSongFinishedHandler();
    public delegate void PlaySongHandler(Song song);
    public delegate void PauseSongHandler();
    public delegate void ResumeSongHandler();
    public delegate void AddToPlayListHandler(Song songs);
}
