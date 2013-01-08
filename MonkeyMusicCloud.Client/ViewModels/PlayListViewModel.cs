using System.Collections.ObjectModel;
using System.Windows.Input;
using MicroMvvm;
using MonkeyMusicCloud.Client.Observers;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels;
using MonkeyMusicCloud.Domain.Model;

namespace MonkeyMusicCloud.Client.ViewModels
{
    public class PlayListViewModel : ViewModelBase
    {


        #region Attributes

        private State playerState;
        private bool shuffle;
        private bool repeat;

        #endregion

        public PlayListViewModel()
        {
            PlayList = new PlayList();
            PlayerObserver.AddToPlayList += delegate(ObservableCollection<Song> songs)
                {
                    PlayList.AddSongs(songs);
                    PlayList.Mix();
                };
            PlayerObserver.CurrentSongFinished += OnCurrentSongFinished;
            PlayerState = State.Stop;
        }

        #region Properties

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

        public bool Shuffle
        {
            get { return shuffle; }
            set
            {
                shuffle = value;
                RaisePropertyChanged("Shuffle");
            }
        }
        
        #endregion

        #region ICommands

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

        public ICommand SwitchShuffleModeCommand
        {
            get { return new RelayCommand(RaiseNewSwitchShuffleModeDemand); }
        }

        public PlayList PlayList { get; set; }

        #endregion

        private void RaiseNewSwitchRepeatModeDemand()
        {
            Repeat = !Repeat;
        }

        private void RaiseNewSwitchShuffleModeDemand()
        {
            Shuffle = !Shuffle;
            if (Shuffle)
            {
                PlayList.Mix();
            }
            else
            {
                PlayList.Restaure();
            }

        }

        private void OnCurrentSongFinished()
        {
            RaiseNewNextSongDemand();
        }

        private void ClearPlayer()
        {
            PlayerState = State.Stop;
            PlayerObserver.NotifyStopSong();
            PlayList.ResetAllSongs();
        }

        private void RaiseNewPlaySongDemand(SongToPlay song)
        {
            if (!PlayList.IsEmpty)
            {
                SongToPlay songToPlay = song ?? PlayList.GetFirst();
                if (PlayList.ActualSong != null)
                {
                    PlayList.ActualSong.IsPlaying = false;
                }
                songToPlay.IsPlaying = true;
                songToPlay.Played = true;
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
            if (PlayList.IsFinished)
            {
                if (Repeat)
                {
                    PlayList.ResetAllSongs();
                    RaiseNewPlaySongDemand(PlayList.GetFirst());
                }
                else
                {
                    ClearPlayer();
                }
            }
            else
            {
                RaiseNewPlaySongDemand(PlayList.GetNextSong());
            }
        }

        private void RaiseNewPreviousSongDemand()
        {

            if (PlayList.PlayingTheFirst)
            {
                if (Repeat)
                {
                    PlayList.ResetAllSongs();
                    RaiseNewPlaySongDemand(PlayList.GetLast());
                }
            }
            else
            {
                RaiseNewPlaySongDemand(PlayList.GetPreviousSong());
            }
        }
        
        private void RaiseClearPlayListDemand()
        {
            PlayList.Clear();
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