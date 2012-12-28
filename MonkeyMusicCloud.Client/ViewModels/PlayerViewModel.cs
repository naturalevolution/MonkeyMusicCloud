using System;
using System.Globalization;
using System.Windows.Input;
using MicroMvvm;
using MonkeyMusicCloud.Client.Exceptions;
using MonkeyMusicCloud.Client.Observers;
using MonkeyMusicCloud.Client.Utilities;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Utilities.Interface;

namespace MonkeyMusicCloud.Client.ViewModels
{
    public class PlayerViewModel : ViewModelBase
    {
        private Song currentSong;
        private string elapsedTime;
        private int purcentagePlayed;
        private string totalTime;

        public PlayerViewModel()
        {
            PlayerObserver.PlaySong += PlayNewSong;
            PlayerObserver.PauseSong += () => MusicPlayer.Pause();
            PlayerObserver.ResumeSong += () => MusicPlayer.Resume();
            PlayerObserver.StopSong += OnStopSong;
            MusicPlayer.PurcentagePlayed += RefreshSongTimes;
            MusicPlayer.SongFinished += OnSongFinished;
        }
        
        public int PurcentagePlayed
        {
            get { return purcentagePlayed; }
            set
            {
                purcentagePlayed = value;
                RaisePropertyChanged("PurcentagePlayed");
            }
        }

        public bool SliderIsOnDrag { get; set; }

        public Song CurrentSong
        {
            get { return currentSong; }
            set
            {
                currentSong = value;
                RaisePropertyChanged("CurrentSong");
            }
        }

        public string ElapsedTime
        {
            get { return elapsedTime; }
            set
            {
                elapsedTime = value;
                RaisePropertyChanged("ElapsedTime");
            }
        }

        public string TotalTime
        {
            get { return totalTime; }
            set
            {
                totalTime = value;
                RaisePropertyChanged("TotalTime");
            }
        }

        public ICommand StartDragCommand
        {
            get { return new RelayCommand(() => { SliderIsOnDrag = true; }); }
        }

        public ICommand StopDragCommand
        {
            get { return new RelayCommand<double>(StopDragExecute); }
        }

        private void OnStopSong()
        {
            ClearPlayer();
            MusicPlayer.Stop();
        }

        private void OnSongFinished()
        {
            ClearPlayer();
            PlayerObserver.NotifyCurrentSongFinished();
        }

        private void RefreshSongTimes(int elapsedTimeFromEvent, int totalTimeFromEvent)
        {
            PurcentagePlayed = !SliderIsOnDrag ? (100*elapsedTimeFromEvent/totalTimeFromEvent) : PurcentagePlayed;
            ElapsedTime = TimeSpan.FromSeconds(elapsedTimeFromEvent).ToString("T", DateTimeFormatInfo.InvariantInfo);
            TotalTime = TimeSpan.FromSeconds(totalTimeFromEvent).ToString("T", DateTimeFormatInfo.InvariantInfo);
        }

        private void ClearPlayer()
        {
            CurrentSong = null;
            TotalTime = null;
            ElapsedTime = null;
            PurcentagePlayed = 0;
        }

        private void PlayNewSong(Song song)
        {
            MusicPlayer.Stop();
            CurrentSong = song;
            MediaFile fileToPlay = Service.GetMediaFileById(song.MediaFileId);
            try
            {
                MusicPlayer.Play(fileToPlay.Id, fileToPlay.Content);
            }
            catch (Exception e)
            {
                new PlayException();
            }
        }

        private void StopDragExecute(double value)
        {
            if (CurrentSong != null)
            {
                SliderIsOnDrag = false;
                MusicPlayer.PlayAt(value);
            }
            else
            {
                PurcentagePlayed = 0;
            }
        }
    }
}