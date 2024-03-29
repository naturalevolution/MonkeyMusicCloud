﻿using System.Collections.ObjectModel;
using MonkeyMusicCloud.Domain.Model;

namespace MonkeyMusicCloud.Client.Observers
{
    public class PlayerObserver
    {
        public static event AddToPlayListHandler AddToPlayList;
        public static void NotifyAddToPlayList(ObservableCollection<Song> songs)
        {
            AddToPlayListHandler handler = AddToPlayList;
            if (handler != null) handler(songs);
        }

        public static event PlaySongHandler PlaySong;
        public static void NotifyPlayNewSong(Song song)
        {
            PlaySongHandler handler = PlaySong;
            if (handler != null) handler(song);
        }

        public static event StopSongHandler StopSong;
        public static void NotifyStopSong()
        {
            StopSongHandler handler = StopSong;
            if (handler != null) handler();
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
    public delegate void StopSongHandler();
    public delegate void PauseSongHandler();
    public delegate void ResumeSongHandler();
    public delegate void AddToPlayListHandler(ObservableCollection<Song> songs);
}
