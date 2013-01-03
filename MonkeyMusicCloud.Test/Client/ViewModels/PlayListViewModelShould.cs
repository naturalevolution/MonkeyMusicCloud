using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.Observers;
using MonkeyMusicCloud.Client.ViewModels;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Test.Helper;
using MonkeyMusicCloud.Test.Helper.EventCatchers;

namespace MonkeyMusicCloud.Test.Client.ViewModels
{
    [TestClass]
    public class PlayListViewModelShould : ViewModelsBaseTest
    {
        private PlayListViewModel ViewModel { get; set; }

        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
            ViewModel = new PlayListViewModel();
            Assert.AreEqual(State.Stop, ViewModel.PlayerState);
        }

        [TestMethod]
        public void RefreshSongWhenAddToPlayListEventIsCatched()
        {
            Song song1 = Create.Song();
            ViewModel.SongList = new ObservableCollection<SongToPlay>() ;
            

            PlayerObserver.NotifyAddToPlayList(new ObservableCollection<Song>{song1});

            Assert.AreEqual(1, ViewModel.SongList.Count);
            CollectionAssert.Contains(ViewModel.SongList.Select(sl => sl.Song).ToList(), song1);
        }

        [TestMethod]
        public void ClearPlayListAndRaiseStopEvent()
        {
            PlayerEventCatcher eventCatcher = new PlayerEventCatcher();
            Song song1 = Create.Song();
            ViewModel.PlayerState = State.Play;
            ViewModel.SongList = new ObservableCollection<SongToPlay>() { new SongToPlay(){Song = song1}};

            ViewModel.ClearPlayListCommand.Execute(null);

            Assert.AreEqual(0, ViewModel.SongList.Count);
            Assert.IsTrue(eventCatcher.StopSongInvoked);
            Assert.AreEqual(State.Stop, ViewModel.PlayerState);
        }
        
        [TestMethod]
        public void DoNotDuplicateSongIntoSongListWhenAddToPlayListEventIsCatched()
        {
            Song song1 = Create.Song();

            PlayerObserver.NotifyAddToPlayList(new ObservableCollection<Song>{song1});
            PlayerObserver.NotifyAddToPlayList(new ObservableCollection<Song>{song1});

            Assert.AreEqual(1, ViewModel.SongList.Count);
            CollectionAssert.Contains(ViewModel.SongList.Select(sl => sl.Song).ToList(), song1);
        }

        [TestMethod]
        public void SwitchRepeatMode()
        {
            Assert.IsFalse(ViewModel.Repeat);

            ViewModel.SwitchRepeatModeCommand.Execute(null);

            Assert.IsTrue(ViewModel.Repeat);

            ViewModel.SwitchRepeatModeCommand.Execute(null);

            Assert.IsFalse(ViewModel.Repeat);
        }

        [TestMethod]
        public void SwitchRandomMode()
        {
            Assert.IsFalse(ViewModel.Random);

            ViewModel.SwitchRandomModeCommand.Execute(null);

            Assert.IsTrue(ViewModel.Random);

            ViewModel.SwitchRandomModeCommand.Execute(null);

            Assert.IsFalse(ViewModel.Random);
        }

        #region PlayCommands
            [TestMethod]
            public void RaiseNewPlayDemandEvent()
            {
                PlayerEventCatcher eventCatcher = new PlayerEventCatcher();
                SongToPlay song1 = new SongToPlay() { Song = Create.Song() };
                ViewModel.SongList = new ObservableCollection<SongToPlay> {song1 };

                ViewModel.PlaySongCommand.Execute(song1);

                Assert.IsTrue(eventCatcher.PlaySongInvoked);
                Assert.AreEqual(song1.Song, eventCatcher.SongToPlay);
                Assert.AreEqual(State.Play, ViewModel.PlayerState);
            }

            [TestMethod]
            public void SetIsPlayingAndAlereadyPlayedPropertiesOnPlayCommand()
            {
                SongToPlay song1 = new SongToPlay() { Song = Create.Song() };
                SongToPlay song2 = new SongToPlay() { Song = Create.Song() };
                ViewModel.SongList = new ObservableCollection<SongToPlay> { song1, song2 };
                
                Assert.IsNull(ViewModel.ActualPlayedSong);

                ViewModel.PlaySongCommand.Execute(song1);
                
                Assert.IsTrue(song1.IsPlaying);
                Assert.IsFalse(song1.AlreadyPlayed);
                Assert.IsFalse(song2.IsPlaying);
                Assert.IsFalse(song2.AlreadyPlayed);

                ViewModel.PlaySongCommand.Execute(song2);

                Assert.IsFalse(song1.IsPlaying);
                Assert.IsTrue(song1.AlreadyPlayed);
                Assert.IsTrue(song2.IsPlaying);
                Assert.IsFalse(song2.AlreadyPlayed);
            }

            [TestMethod]
            public void DoNotRaiseTheEventIfTheListIsEmpty()
            {
                PlayerEventCatcher eventCatcher = new PlayerEventCatcher();

                ViewModel.PlaySongCommand.Execute(null);

                Assert.IsFalse(eventCatcher.PlaySongInvoked);
                Assert.AreEqual(State.Stop, ViewModel.PlayerState);
            }

            [TestMethod]
            public void RaiseThePlayEventWithTheFirstSongOfTheListIfNoSongIsSelected()
            {
                SongToPlay song1 = new SongToPlay() { Song = Create.Song() };
                SongToPlay song2 = new SongToPlay() { Song = Create.Song() };
                PlayerEventCatcher eventCatcher = new PlayerEventCatcher();
                ViewModel.SongList = new ObservableCollection<SongToPlay> { song1, song2 };

                ViewModel.PlaySongCommand.Execute(null);

                Assert.IsTrue(eventCatcher.PlaySongInvoked);
                Assert.AreEqual(song1.Song, eventCatcher.SongToPlay);
                Assert.AreEqual(song1, ViewModel.ActualPlayedSong);
                Assert.AreEqual(State.Play, ViewModel.PlayerState);
            }

            [TestMethod]
            public void ChangeTheActualSelectedSongOnPlayDemand()
            {
                SongToPlay song1 = new SongToPlay() { Song = Create.Song() };
                SongToPlay song2 = new SongToPlay() { Song = Create.Song() };

                ViewModel.SongList = new ObservableCollection<SongToPlay> { song1, song2 };
                ViewModel.PlaySongCommand.Execute(song2);

                Assert.AreEqual(song2, ViewModel.ActualPlayedSong);
                Assert.AreEqual(State.Play, ViewModel.PlayerState);
            }
        #endregion

        #region PauseCommands

            [TestMethod]
            public void RaisePauseDemandEvent()
            {
                PlayerEventCatcher eventCatcher = new PlayerEventCatcher();
                ViewModel.PlayerState = State.Play;

                ViewModel.PauseSongCommand.Execute(null);

                Assert.IsTrue(eventCatcher.PauseSongInvoked);
                Assert.AreEqual(State.Pause, ViewModel.PlayerState);
            }

            [TestMethod]
            public void DoNothingOnPauseCommandIfPlayerIsNotPlaying()
            {
                PlayerEventCatcher eventCatcher = new PlayerEventCatcher();
                ViewModel.SongList = new ObservableCollection<SongToPlay>();

                ViewModel.PlayerState = State.Pause;
                ViewModel.PauseSongCommand.Execute(null);

                Assert.IsFalse(eventCatcher.PauseSongInvoked);
                Assert.AreEqual(State.Pause, ViewModel.PlayerState);

                ViewModel.PlayerState = State.Stop;
                ViewModel.PauseSongCommand.Execute(null);

                Assert.IsFalse(eventCatcher.PauseSongInvoked);
                Assert.AreEqual(State.Stop, ViewModel.PlayerState);
            }
            
            [TestMethod]
            public void RaiseResumeDemandEvent()
            {
                PlayerEventCatcher eventCatcher = new PlayerEventCatcher();
                ViewModel.SongList = new ObservableCollection<SongToPlay>();
                ViewModel.PlayerState = State.Pause;

                ViewModel.ResumeSongCommand.Execute(null);

                Assert.IsTrue(eventCatcher.ResumeSongInvoked);
                Assert.AreEqual(State.Play, ViewModel.PlayerState);
            }

            [TestMethod]
            public void DoNothingOnResumeCommandIfPlayerIsNotOnPause()
            {
                PlayerEventCatcher eventCatcher = new PlayerEventCatcher();
                ViewModel.SongList = new ObservableCollection<SongToPlay>();

                ViewModel.PlayerState = State.Play;
                ViewModel.ResumeSongCommand.Execute(null);

                Assert.IsFalse(eventCatcher.ResumeSongInvoked);
                Assert.AreEqual(State.Play, ViewModel.PlayerState);

                ViewModel.PlayerState = State.Stop;
                ViewModel.ResumeSongCommand.Execute(null);

                Assert.IsFalse(eventCatcher.ResumeSongInvoked);
                Assert.AreEqual(State.Stop, ViewModel.PlayerState);
            }


        #endregion

        #region PreviousCommands
            [TestMethod]
            public void PlayPreviousSong()
            {
                SongToPlay song1 = new SongToPlay(){Song = Create.Song()};
                SongToPlay song2 = new SongToPlay(){Song = Create.Song()};
                PlayerEventCatcher eventCatcher = new PlayerEventCatcher();
                PlayListViewModel viewModel = new PlayListViewModel
                {
                    SongList = new ObservableCollection<SongToPlay> { song1, song2 },
                    ActualPlayedSong = song2
                };

                viewModel.PreviousSongCommand.Execute(null);

                Assert.IsTrue(eventCatcher.PlaySongInvoked);
                Assert.AreEqual(song1.Song, eventCatcher.SongToPlay);
                Assert.AreEqual(song1, viewModel.ActualPlayedSong);
            }

            [TestMethod]
            public void DoNothingIfThereIsNoActualSongOnPreviousSongCommand()
            {
                PlayerEventCatcher eventCatcher = new PlayerEventCatcher();
                PlayListViewModel viewModel = new PlayListViewModel { SongList = new ObservableCollection<SongToPlay>() };

                viewModel.PreviousSongCommand.Execute(null);

                Assert.IsFalse(eventCatcher.PlaySongInvoked);
            }

            [TestMethod]
            public void DoNothingIfThereIsNoActualSongOnSongFinished()
            {
                PlayerEventCatcher eventCatcher = new PlayerEventCatcher();

                PlayerObserver.NotifyCurrentSongFinished();

                Assert.IsFalse(eventCatcher.PlaySongInvoked);
            }

            [TestMethod]
            public void DoNothingIfTheActualSOngIsTheFirstOfTheList()
            {
                SongToPlay song1 = new SongToPlay() { Song = Create.Song() };
                SongToPlay song2 = new SongToPlay() { Song = Create.Song() };
                PlayerEventCatcher eventCatcher = new PlayerEventCatcher();
                ViewModel.SongList = new ObservableCollection<SongToPlay> {song1, song2};
                ViewModel.ActualPlayedSong = song1;

                ViewModel.PreviousSongCommand.Execute(null);

                Assert.IsFalse(eventCatcher.PlaySongInvoked);
                Assert.AreEqual(song1, ViewModel.ActualPlayedSong);
            }
        #endregion

        #region NextCommands

            [TestMethod]
            public void PlayNextSong()
            {
                SongToPlay song1 = new SongToPlay() { Song = Create.Song() };
                SongToPlay song2 = new SongToPlay() { Song = Create.Song() };
                PlayerEventCatcher eventCatcher = new PlayerEventCatcher();
                ViewModel.SongList = new ObservableCollection<SongToPlay> {song1, song2};
                ViewModel.ActualPlayedSong = song1;

                ViewModel.NextSongCommand.Execute(null);

                Assert.IsTrue(eventCatcher.PlaySongInvoked);
                Assert.AreEqual(song2.Song, eventCatcher.SongToPlay);
                Assert.AreEqual(song2, ViewModel.ActualPlayedSong);
            }

            [TestMethod]
            public void PlayNextSongWhenTheCurrentIsFinished()
            {
                SongToPlay song1 = new SongToPlay() { Song = Create.Song() };
                SongToPlay song2 = new SongToPlay() { Song = Create.Song() };
                PlayerEventCatcher eventCatcher = new PlayerEventCatcher();
                ViewModel.SongList = new ObservableCollection<SongToPlay> {song1, song2};
                ViewModel.ActualPlayedSong = song1;

                PlayerObserver.NotifyCurrentSongFinished();

                Assert.IsTrue(eventCatcher.PlaySongInvoked);
                Assert.AreEqual(song2.Song, eventCatcher.SongToPlay);
                Assert.AreEqual(song2, ViewModel.ActualPlayedSong);
            }

            [TestMethod]
            public void DoNothingIfThereIsNoActualSongOnNextSongCommand()
            {
                PlayerEventCatcher eventCatcher = new PlayerEventCatcher();
                ViewModel.SongList = new ObservableCollection<SongToPlay>();

                ViewModel.NextSongCommand.Execute(null);

                Assert.IsFalse(eventCatcher.PlaySongInvoked);
            }
        
            [TestMethod]
            public void DoNothingIfThereIsTheEndOfPlayListOnNextSongDemand()
            {
                SongToPlay song1 = new SongToPlay() { Song = Create.Song() };
                PlayerEventCatcher eventCatcher = new PlayerEventCatcher();
                ViewModel.SongList = new ObservableCollection<SongToPlay> {song1};
                ViewModel.ActualPlayedSong = song1;

                ViewModel.NextSongCommand.Execute(null);

                Assert.IsFalse(eventCatcher.PlaySongInvoked);
                Assert.AreEqual(song1, ViewModel.ActualPlayedSong);
            }

            [TestMethod]
            public void ClearPlayerIfThereIsTheEndOfPlayListOnSongFinished()
            {
                SongToPlay song1 = new SongToPlay() { Song = Create.Song() };
                PlayerEventCatcher eventCatcher = new PlayerEventCatcher();
                ViewModel.SongList = new ObservableCollection<SongToPlay> {song1};
                ViewModel.ActualPlayedSong = song1;
                ViewModel.PlayerState = State.Play;

                PlayerObserver.NotifyCurrentSongFinished();

                Assert.IsFalse(eventCatcher.PlaySongInvoked);
                Assert.IsNull(ViewModel.ActualPlayedSong);
                Assert.AreEqual(State.Stop, ViewModel.PlayerState);
            }

            [TestMethod]
            public void IfPlayListIsFinishedAndRepeatIsActivatedPlayTheFirstSong()
            {
                SongToPlay song1 = new SongToPlay { Song = Create.Song(), AlreadyPlayed = true};
                SongToPlay song2 = new SongToPlay { Song = Create.Song(), AlreadyPlayed = true, IsPlaying = true};
                PlayerEventCatcher eventCatcher = new PlayerEventCatcher();
                ViewModel.SongList = new ObservableCollection<SongToPlay> { song1, song2 };
                ViewModel.ActualPlayedSong = song2;
                ViewModel.PlayerState = State.Play;
                ViewModel.Repeat = true;

                PlayerObserver.NotifyCurrentSongFinished();

                Assert.IsTrue(eventCatcher.PlaySongInvoked);
                Assert.AreEqual(song1.Song, eventCatcher.SongToPlay);
                Assert.AreEqual(song1, ViewModel.ActualPlayedSong);
                Assert.AreEqual(State.Play, ViewModel.PlayerState);
                Assert.IsTrue(ViewModel.SongList.All(sl => sl.AlreadyPlayed == false));
                Assert.IsTrue(song1.IsPlaying);
            }


           

        #endregion
    }
}
