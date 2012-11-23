﻿using System;
using System.Collections.ObjectModel;
using MonkeyMusicCloud.Client.Observers;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels;
using MonkeyMusicCloud.Domain.Model;

namespace MonkeyMusicCloud.Test.Helper
{
    public class EventCatcher
    {
        public EventCatcher()
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

            ContentBodyObserver.ChangeContentView += delegate(MenuItem view)
            {
                ChangeContentViewInvoked = true;
                MenuItem = view;
            };

            ContentBodyObserver.NewSearch += delegate(ObservableCollection<Song> songs, string search)
            {
                NewSearchInvoked = true;
                SearchSongList = songs;
                SearchFilter = search;
            };
        }

        public MenuItem MenuItem { get; set; }
        public ObservableCollection<Song> AddToPlayListSong { get; set; }
        public ObservableCollection<Song> SearchSongList { get; set; }
        public Song SongToPlay { get; set; }

        public string SearchFilter;

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
