using System;
using System.Globalization;
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
            PlayerObserver.PauseSong += MusicPlayer.Pause;
            PlayerObserver.ResumeSong += MusicPlayer.Resume;
            PlayerObserver.StopSong += delegate
                                           {
                                               ClearPlayer();
                                               MusicPlayer.Stop();
                                           };

            MusicPlayer.PurcentagePlayed += delegate(int elapsedTime, int totalTime)
                                                {
                                                    PurcentagePlayed = (elapsedTime*100/totalTime );
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
            TotalTime = null;
            ElapsedTime = null;
            PurcentagePlayed = 0;
        }

        private void PlayNewSong(Song song)
        {
            MusicPlayer.Stop();
            MusicPlayer.Play(song.File.Content);
            CurrentSong = song;
        }
    }
}