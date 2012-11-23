﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MicroMvvm;
using MonkeyMusicCloud.Client.Observers;
using MonkeyMusicCloud.Domain.Model;

namespace MonkeyMusicCloud.Client.ViewModels
{
    public class PlayListViewModel : ViewModelBase
    {
        public PlayListViewModel()
        {
            SongList = new ObservableCollection<Song>();
            PlayerObserver.AddToPlayList += delegate(ObservableCollection<Song> songs)
                                               {
                                                   foreach (Song song in songs)
                                                   {
                                                       if (!SongList.Contains(song))
                                                       {
                                                           SongList.Add(song);
                                                       } 
                                                   }
                                               };
            PlayerObserver.CurrentSongFinished += delegate
                                                     {
                                                         if (SongList.IndexOf(ActualPlayedSong) == SongList.Count - 1)
                                                         {
                                                             ClearPlayer();
                                                         }
                                                         RaiseNewNextSongDemand();
                                                     };
            PlayerState = State.Stop;
        }
        
        private ObservableCollection<Song> songList;

        public ObservableCollection<Song> SongList
        {
            get { return songList; }
            set
            {
                songList = value;
                RaisePropertyChanged("SongList");
            }
        }

        private Song actualPlayedSong;

        public Song ActualPlayedSong
        {
            get { return actualPlayedSong; }
            set
            {
                actualPlayedSong = value;
                RaisePropertyChanged("ActualPlayedSong");
            }
        }

        private void ClearPlayer()
        {
            ActualPlayedSong = null;
            PlayerState = State.Stop;
        }

        private void RaiseNewPlaySongDemand(Song song)
        {
            if (SongList.Count > 0)
            {
                Song songToPlay = song ?? SongList.First();
                PlayerObserver.NotifyPlayNewSong(songToPlay);
                ActualPlayedSong = songToPlay;
                PlayerState = State.Play;
            }
        }

        private void RaiseNewPauseSongDemand()
        {
            if (PlayerState == State.Play)
            {
                PlayerObserver.NotifyPauseSong();
                PlayerState = State.Pause;
            }
        }

        private void RaiseNewNextSongDemand()
        {
            if (ActualPlayedSong != null && SongList.IndexOf(ActualPlayedSong) != SongList.Count - 1)
            {
                int indexOfActualSong = SongList.IndexOf(ActualPlayedSong);
                RaiseNewPlaySongDemand(SongList[indexOfActualSong + 1]);
            }
        }

        private void RaiseNewPreviousSongDemand()
        {
            if (ActualPlayedSong != null && SongList.IndexOf(ActualPlayedSong) != 0)
            {
                int indexOfActualSong = SongList.IndexOf(ActualPlayedSong);
                RaiseNewPlaySongDemand(SongList[indexOfActualSong - 1]);
            }
        }
        
        private void RaiseClearPlayListDemand()
        {
            SongList.Clear();
            PlayerState = State.Stop;
            PlayerObserver.NotifyStopSong();
        }

        public ICommand PlaySongCommand { get { return new RelayCommand<Song>(RaiseNewPlaySongDemand); } }
        public ICommand ClearPlayListCommand { get { return new RelayCommand(RaiseClearPlayListDemand); } }
        public ICommand NextSongCommand { get { return new RelayCommand(RaiseNewNextSongDemand); } }
        public ICommand PreviousSongCommand { get { return new RelayCommand(RaiseNewPreviousSongDemand); } }
        public ICommand PauseSongCommand { get { return new RelayCommand(RaiseNewPauseSongDemand); } }
        public ICommand ResumeSongCommand { get { return new RelayCommand(RaiseNewResumeSongDemand); } }

        private void RaiseNewResumeSongDemand()
        {
            if (PlayerState == State.Pause)
            {
                PlayerObserver.NotifyResumeSong();
                PlayerState = State.Play;
            }
        }

        private State playerState;

        public State PlayerState
        {
            get { return playerState; }
            set { 
                playerState = value;
                RaisePropertyChanged("PlayerState");
            }
        }
    }

    public enum State
    {
        Play, 
        Pause, 
        Stop
    }
}