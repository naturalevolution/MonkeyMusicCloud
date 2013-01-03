using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MicroMvvm;
using MonkeyMusicCloud.Client.Observers;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels;
using MonkeyMusicCloud.Domain.Model;

namespace MonkeyMusicCloud.Client.ViewModels
{
    public class PlayListViewModel : ViewModelBase
    {
        private SongToPlay actualPlayedSong;
        private State playerState;
        private bool random;
        private bool repeat;
        private ObservableCollection<SongToPlay> songList;

        public PlayListViewModel()
        {
            SongList = new ObservableCollection<SongToPlay>();
            PlayerObserver.AddToPlayList += delegate(ObservableCollection<Song> songs)
                {
                    foreach (Song song in songs)
                    {
                        if (!SongList.Select(sl => sl.Song).Contains(song))
                        {
                            SongList.Add(new SongToPlay {Song = song});
                        }
                    }
                };
            PlayerObserver.CurrentSongFinished += OnCurrentSongFinished;
            PlayerState = State.Stop;
        }

        public ObservableCollection<SongToPlay> SongList
        {
            get { return songList; }
            set
            {
                songList = value;
                RaisePropertyChanged("SongList");
            }
        }

        public SongToPlay ActualPlayedSong
        {
            get { return actualPlayedSong; }
            set
            {
                actualPlayedSong = value;
                RaisePropertyChanged("ActualPlayedSong");
            }
        }

        public ICommand PlaySongCommand
        {
            get { return new RelayCommand<SongToPlay>(RaiseNewPlaySongDemand); }
        }

        public ICommand ClearPlayListCommand
        {
            get { return new RelayCommand(RaiseClearPlayListDemand); }
        }

        public ICommand NextSongCommand
        {
            get { return new RelayCommand(RaiseNewNextSongDemand); }
        }

        public ICommand PreviousSongCommand
        {
            get { return new RelayCommand(RaiseNewPreviousSongDemand); }
        }

        public ICommand PauseSongCommand
        {
            get { return new RelayCommand(RaiseNewPauseSongDemand); }
        }

        public ICommand ResumeSongCommand
        {
            get { return new RelayCommand(RaiseNewResumeSongDemand); }
        }

        public ICommand SwitchRepeatModeCommand
        {
            get { return new RelayCommand(RaiseNewSwitchRepeatModeDemand); }
        }

        public ICommand SwitchRandomModeCommand
        {
            get { return new RelayCommand(RaiseNewSwitchRandomModeDemand); }
        }

        public State PlayerState
        {
            get { return playerState; }
            set
            {
                playerState = value;
                RaisePropertyChanged("PlayerState");
            }
        }

        public bool Repeat
        {
            get { return repeat; }
            set
            {
                repeat = value;
                RaisePropertyChanged("Repeat");
            }
        }

        public bool Random
        {
            get { return random; }
            set
            {
                random = value;
                RaisePropertyChanged("Random");
            }
        }

        private void RaiseNewSwitchRepeatModeDemand()
        {
            Repeat = !Repeat;
        }

        private void RaiseNewSwitchRandomModeDemand()
        {
            Random = !Random;
        }

        private void OnCurrentSongFinished()
        {
            if (SongList.IndexOf(ActualPlayedSong) == SongList.Count - 1)
            {
                ClearPlayer();
                if (Repeat)
                {
                    foreach (SongToPlay songToPlay in SongList)
                    {
                        songToPlay.AlreadyPlayed = false;
                    }
                    RaiseNewPlaySongDemand(SongList[0]);
                }
            }
            else
            {
                RaiseNewNextSongDemand();
            }
        }

        private void ClearPlayer()
        {
            ActualPlayedSong = null;
            PlayerState = State.Stop;
        }

        private void RaiseNewPlaySongDemand(SongToPlay song)
        {
            if (SongList.Count > 0)
            {
                SongToPlay songToPlay = song ?? SongList.First();
                if (ActualPlayedSong != null)
                {
                    ActualPlayedSong.AlreadyPlayed = true;
                    ActualPlayedSong.IsPlaying = false;
                }

                ActualPlayedSong = songToPlay;
                songToPlay.IsPlaying = true;
                PlayerObserver.NotifyPlayNewSong(songToPlay.Song);
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
            if (ActualPlayedSong != null && PlayListIsNotFinished())
            {
                int indexOfActualSong = SongList.IndexOf(ActualPlayedSong);
                RaiseNewPlaySongDemand(SongList[indexOfActualSong + 1]);
            }
        }

        private bool PlayListIsNotFinished()
        {
            return SongList.IndexOf(ActualPlayedSong) != SongList.Count - 1;
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

        private void RaiseNewResumeSongDemand()
        {
            if (PlayerState == State.Pause)
            {
                PlayerObserver.NotifyResumeSong();
                PlayerState = State.Play;
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