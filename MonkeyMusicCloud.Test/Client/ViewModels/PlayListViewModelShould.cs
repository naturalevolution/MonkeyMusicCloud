using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.Observers;
using MonkeyMusicCloud.Client.ViewModels;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Test.Helper;
using MonkeyMusicCloud.Test.Helper.EventCatchers;
using Moq;

namespace MonkeyMusicCloud.Test.Client.ViewModels
{
    [TestClass]
    public class PlayListViewModelShould : ViewModelsBaseTest
    {
        private readonly Mock<PlayList> mockPlayList = new Mock<PlayList>();
        private PlayListViewModel ViewModel { get; set; }

        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
            ViewModel = new PlayListViewModel
                {
                    PlayList = mockPlayList.Object
                };
        }

        [TestMethod]
        public void InstantiateCorrectly()
        {
            ViewModel = new PlayListViewModel();
            Assert.IsNotNull(ViewModel.PlayList);
            Assert.AreEqual(State.Stop, ViewModel.PlayerState);
        }

        [TestMethod]
        public void RefreshSongWhenAddToPlayListEventIsCatched()
        {
            ObservableCollection<Song> songsToAdd = new ObservableCollection<Song> {Create.Song()};

            PlayerObserver.NotifyAddToPlayList(songsToAdd);

            mockPlayList.Verify(pl => pl.AddSongs(songsToAdd), Times.Once());
        }

        [TestMethod]
        public void IfShuffleIsActivatedWhenASongIsAddedMixThePlayList()
        {
            ObservableCollection<Song> songsToAdd = new ObservableCollection<Song> {Create.Song()};
            ViewModel.Shuffle = true;

            PlayerObserver.NotifyAddToPlayList(songsToAdd);

            mockPlayList.Verify(pl => pl.Mix(), Times.Once());
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
        public void ClearPlayListAndRaiseStopEvent()
        {
            PlayerEventCatcher eventCatcher = new PlayerEventCatcher();
            ViewModel.PlayerState = State.Play;

            ViewModel.ClearPlayListCommand.Execute(null);

            Assert.IsTrue(eventCatcher.StopSongInvoked);
            Assert.AreEqual(State.Stop, ViewModel.PlayerState);
            mockPlayList.Verify(pl => pl.Clear(), Times.Once());
        }

        [TestMethod]
        public void SwitchShuffleMode()
        {
            Assert.IsFalse(ViewModel.Shuffle);

            ViewModel.SwitchShuffleModeCommand.Execute(null);

            mockPlayList.Verify(pl => pl.Mix(), Times.Once());
            Assert.IsTrue(ViewModel.Shuffle);

            ViewModel.SwitchShuffleModeCommand.Execute(null);

            mockPlayList.Verify(pl => pl.Restaure(), Times.Once());
            Assert.IsFalse(ViewModel.Shuffle);
        }

        #region PlayCommands

        [TestMethod]
        public void PlayASong()
        {
            PlayerEventCatcher eventCatcher = new PlayerEventCatcher();
            SongToPlay song = new SongToPlay {Song = Create.Song()};

            ViewModel.PlaySongCommand.Execute(song);

            Assert.IsTrue(eventCatcher.PlaySongInvoked);
            Assert.AreEqual(song.Song, eventCatcher.SongToPlay);
            Assert.AreEqual(State.Play, ViewModel.PlayerState);
        }

        [TestMethod]
        public void PlayTheFirstSongOfPlayListIfParameterIsNull()
        {
            PlayerEventCatcher eventCatcher = new PlayerEventCatcher();
            SongToPlay song = new SongToPlay {Song = Create.Song()};
            mockPlayList.Setup(pl => pl.GetFirst()).Returns(song);

            ViewModel.PlaySongCommand.Execute(null);

            Assert.IsTrue(eventCatcher.PlaySongInvoked);
            Assert.AreEqual(song.Song, eventCatcher.SongToPlay);
            Assert.AreEqual(State.Play, ViewModel.PlayerState);
        }

        [TestMethod]
        public void DoNotPlayIfPlayListIsEmpty()
        {
            PlayerEventCatcher eventCatcher = new PlayerEventCatcher();
            mockPlayList.Setup(pl => pl.IsEmpty).Returns(true);

            ViewModel.PlaySongCommand.Execute(null);

            Assert.IsFalse(eventCatcher.PlaySongInvoked);
            Assert.AreEqual(State.Stop, ViewModel.PlayerState);
        }

        [TestMethod]
        public void WhenASongIsPlayedSetPlayedProperties()
        {
            SongToPlay beforeSong = new SongToPlay {Song = Create.Song(), IsPlaying = true, Played = true};
            SongToPlay afterSong = new SongToPlay {Song = Create.Song()};
            mockPlayList.Setup(pl => pl.ActualSong).Returns(beforeSong);

            ViewModel.PlaySongCommand.Execute(afterSong);

            Assert.IsFalse(beforeSong.IsPlaying);
            Assert.IsTrue(beforeSong.Played);
            Assert.IsTrue(afterSong.IsPlaying);
            Assert.IsTrue(afterSong.Played);
        }

        [TestMethod]
        public void WhenASongIsPlayedSetPlayedUnlessActualSongIfNull()
        {
            SongToPlay beforeSong = null;
            SongToPlay afterSong = new SongToPlay {Song = Create.Song()};
            mockPlayList.Setup(pl => pl.ActualSong).Returns(beforeSong);

            ViewModel.PlaySongCommand.Execute(afterSong);

            Assert.IsTrue(afterSong.IsPlaying);
            Assert.IsTrue(afterSong.Played);
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
            ViewModel.PlayerState = State.Pause;

            ViewModel.ResumeSongCommand.Execute(null);

            Assert.IsTrue(eventCatcher.ResumeSongInvoked);
            Assert.AreEqual(State.Play, ViewModel.PlayerState);
        }

        [TestMethod]
        public void DoNothingOnResumeCommandIfPlayerIsNotOnPause()
        {
            PlayerEventCatcher eventCatcher = new PlayerEventCatcher();

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
            SongToPlay song1 = new SongToPlay {Song = Create.Song()};
            mockPlayList.Setup(pl => pl.GetPreviousSong()).Returns(song1);
            PlayerEventCatcher eventCatcher = new PlayerEventCatcher();

            ViewModel.PreviousSongCommand.Execute(null);

            mockPlayList.Verify();
            Assert.IsTrue(eventCatcher.PlaySongInvoked);
            Assert.AreEqual(song1.Song, eventCatcher.SongToPlay);
        }

        [TestMethod]
        public void IfPlayListIsTheBeginningAndRepeatIsActivatedPlayTheLastSong()
        {
            PlayerEventCatcher eventCatcher = new PlayerEventCatcher();
            SongToPlay song = new SongToPlay();
            ViewModel.PlayerState = State.Play;
            ViewModel.Repeat = true;
            mockPlayList.Setup(pl => pl.PlayingTheFirst).Returns(true);
            mockPlayList.Setup(pl => pl.GetLast()).Returns(song);

            ViewModel.PreviousSongCommand.Execute(null);

            mockPlayList.Verify(pl => pl.ResetAllSongs(), Times.Once());
            mockPlayList.Verify(pl => pl.GetLast(), Times.Once());
            Assert.IsTrue(eventCatcher.PlaySongInvoked);
            Assert.AreEqual(song.Song, eventCatcher.SongToPlay);
            Assert.AreEqual(State.Play, ViewModel.PlayerState);
        }

        #endregion

        #region NextCommands

        [TestMethod]
        public void PlayNextSongOfPlayList()
        {
            SongToPlay song = new SongToPlay {Song = Create.Song()};
            mockPlayList.Setup(pl => pl.GetNextSong()).Returns(song);
            PlayerEventCatcher eventCatcher = new PlayerEventCatcher();

            ViewModel.NextSongCommand.Execute(null);

            mockPlayList.Verify(pl => pl.GetNextSong(), Times.Once());
            Assert.IsTrue(eventCatcher.PlaySongInvoked);
            Assert.AreEqual(song.Song, eventCatcher.SongToPlay);
        }

        [TestMethod]
        public void PlayNextSongWhenTheCurrentIsFinished()
        {
            SongToPlay song = new SongToPlay {Song = Create.Song()};
            mockPlayList.Setup(pl => pl.GetNextSong()).Returns(song);
            PlayerEventCatcher eventCatcher = new PlayerEventCatcher();

            PlayerObserver.NotifyCurrentSongFinished();

            mockPlayList.Verify(pl => pl.GetNextSong(), Times.Once());
            Assert.IsTrue(eventCatcher.PlaySongInvoked);
            Assert.AreEqual(song.Song, eventCatcher.SongToPlay);
        }

        [TestMethod]
        public void DoNothingIfThereIsTheEndOfPlayListOnNextSongDemand()
        {
            PlayerEventCatcher eventCatcher = new PlayerEventCatcher();
            mockPlayList.Setup(pl => pl.IsFinished).Returns(true);

            ViewModel.NextSongCommand.Execute(null);

            Assert.IsFalse(eventCatcher.PlaySongInvoked);
        }

        [TestMethod]
        public void ClearPlayerIfThereIsTheEndOfPlayListOnSongFinished()
        {
            PlayerEventCatcher eventCatcher = new PlayerEventCatcher();
            ViewModel.PlayerState = State.Play;
            mockPlayList.Setup(pl => pl.IsFinished).Returns(true);

            PlayerObserver.NotifyCurrentSongFinished();

            Assert.IsFalse(eventCatcher.PlaySongInvoked);
            Assert.AreEqual(State.Stop, ViewModel.PlayerState);
            Assert.IsTrue(eventCatcher.StopSongInvoked);
            mockPlayList.Verify(pl => pl.ResetAllSongs(), Times.Once());
        }

        [TestMethod]
        public void IfPlayListIsFinishedAndRepeatIsActivatedPlayTheFirstSong()
        {
            PlayerEventCatcher eventCatcher = new PlayerEventCatcher();
            SongToPlay song = new SongToPlay();
            ViewModel.PlayerState = State.Play;
            ViewModel.Repeat = true;
            mockPlayList.Setup(pl => pl.IsFinished).Returns(true);
            mockPlayList.Setup(pl => pl.GetFirst()).Returns(song);

            PlayerObserver.NotifyCurrentSongFinished();

            mockPlayList.Verify(pl => pl.ResetAllSongs(), Times.Once());
            mockPlayList.Verify(pl => pl.GetFirst(), Times.Once());
            Assert.IsTrue(eventCatcher.PlaySongInvoked);
            Assert.AreEqual(song.Song, eventCatcher.SongToPlay);
            Assert.AreEqual(State.Play, ViewModel.PlayerState);
        }

        #endregion
    }
}