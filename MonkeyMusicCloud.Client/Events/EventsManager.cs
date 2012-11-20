using System;
using System.Collections.ObjectModel;
using MonkeyMusicCloud.Client.Views.BodyViews;
using MonkeyMusicCloud.Domain.Model;

namespace MonkeyMusicCloud.Client.Events
{
    /// <summary>
    /// TODO : Surement mieux à faire
    /// Classe fourre-tout, mais permet de s'abstraire d'une relation entre les vueModel. 
    /// Permet aussi de pouvoir partager un event commun
    /// </summary>
    public class EventsManager
    {
        public static event AddToPlayListHandler AddToPlayList;

       

        public static void InvokeAddToPlayList(ObservableCollection<Song> songs)
        {
            AddToPlayListHandler handler = AddToPlayList;
            if (handler != null) handler(songs);
        }

        public static event ChangeContentViewHandler ChangeContentView;
        public static void InvokeChangeContentView(IBodyView view)
        {
            ChangeContentViewHandler handler = ChangeContentView;
            if (handler != null) handler(view);
        }

        public static event PlaySongHandler PlaySong;
        public static void InvokePlayNewSong(Song song)
        {
            PlaySongHandler handler = PlaySong;
            if (handler != null) handler(song);
        }

        public static event PauseSongHandler PauseSong;
        public static void InvokePauseSong()
        {
            PauseSongHandler handler = PauseSong;
            if (handler != null) handler();
        }

        public static event CurrentSongFinishedHandler CurrentSongFinished;
        public static void InvokeCurrentSongFinished()
        {
            CurrentSongFinishedHandler handler = CurrentSongFinished;
            if (handler != null) handler();
        }


        public static event ResumeSongHandler ResumeSong;
        public static void InvokeResumeSong()
        {
            ResumeSongHandler handler = ResumeSong;
            if (handler != null) handler();
        }
    }

    public delegate void CurrentSongFinishedHandler();
    public delegate void PlaySongHandler(Song song);
    public delegate void PauseSongHandler();
    public delegate void ResumeSongHandler();
    public delegate void ChangeContentViewHandler(IBodyView view);
    public delegate void AddToPlayListHandler(ObservableCollection<Song> songs);
}
