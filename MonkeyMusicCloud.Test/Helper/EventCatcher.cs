﻿using System;
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

            EventsManager.CurrentSongFinished += delegate()
            {
                SongFinishedInvoked = true;
            };


            EventsManager.PauseSong += delegate()
            {
                PauseSongInvoked = true;
            };


            EventsManager.ResumeSong += delegate()
            {
                ResumeSongInvoked = true;
            };
        }

        public ObservableCollection<Song> AddToPlayListSongs { get; set; }
        public Song SongToPlay { get; set; }
        public bool AddToPlayListInvoked;
        public bool PlaySongInvoked;
        public bool ResumeSongInvoked;
        public bool SongFinishedInvoked;
        public bool PauseSongInvoked;
    }
}