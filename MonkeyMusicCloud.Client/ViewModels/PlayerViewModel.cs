using System;
using System.Globalization;
using System.Windows.Input;
using MicroMvvm;
using MonkeyMusicCloud.Client.Observers;
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

        private Song currentSong;
        private string elapsedTime;
        private string totalTime;

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
            get {
                return elapsedTime;
            }
            set {
                elapsedTime = value;
                RaisePropertyChanged("ElapsedTime");
            }
        }

        public string TotalTime
        {
            get {
                return totalTime;
            }
            set {
                totalTime = value;
                RaisePropertyChanged("TotalTime");
            }
        }

        public PlayerViewModel()
        {
            PlayerObserver.PlaySong += PlayNewSong;
            PlayerObserver.PauseSong += delegate
                {
                    MusicPlayer.Pause();
                };
            PlayerObserver.ResumeSong += delegate
                {
                    MusicPlayer.Resume();
                };
            PlayerObserver.StopSong += delegate
                                           {
                                               ClearPlayer();
                                               MusicPlayer.Stop();
                                           };

            MusicPlayer.PurcentagePlayed += delegate(int elapsedTime, int totalTime)
                                                {
                                                    PurcentagePlayed = !SliderIsOnDrag?(100 * elapsedTime / totalTime ) : PurcentagePlayed;
                                                    ElapsedTime = TimeSpan.FromSeconds(elapsedTime).ToString("T", DateTimeFormatInfo.InvariantInfo);
                                                    TotalTime = TimeSpan.FromSeconds(totalTime).ToString("T", DateTimeFormatInfo.InvariantInfo);
                                                };

            MusicPlayer.SongFinished += delegate
                                            {
                                                ClearPlayer();
                                                PlayerObserver.NotifyCurrentSongFinished();
                                            };
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
            MediaFile fileToPlay = Service.GetMediaFileById(song.MediaFileId);
            MusicPlayer.Play(fileToPlay.Id, fileToPlay.Content);
            CurrentSong = song;
        }

        public ICommand StartDragCommand { get { return new RelayCommand(() => { SliderIsOnDrag = true; }); } }

        public ICommand StopDragCommand { get { return new RelayCommand<double>(StopDragExecute); } }

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