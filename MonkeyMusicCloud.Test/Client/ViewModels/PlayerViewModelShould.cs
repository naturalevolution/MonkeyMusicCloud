using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.Observers;
using MonkeyMusicCloud.Client.ViewModels;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Test.Helper;
using Moq;

namespace MonkeyMusicCloud.Test.Client.ViewModels
{
    [TestClass]
    public class PlayerViewModelShould : ViewModelsBaseTest
    {
        [TestMethod]
        public void CallMusicPlayerPlayWhenAPlaySongEventIsCatched()
        {
            Song songToPlay = Create.Song();
            PlayerViewModel viewModel = new PlayerViewModel();

            PlayerObserver.NotifyPlayNewSong(songToPlay);

            MusicPlayer.Verify(mp => mp.Stop());
            MusicPlayer.Verify(mp => mp.Play(songToPlay.File.Content));
            Assert.AreEqual(songToPlay, viewModel.CurrentSong);
        }

        [TestMethod]
        public void CallMusicPlayerResumeWhenAResumeSongEventIsCatched()
        {
            new PlayerViewModel();

            PlayerObserver.NotifyResumeSong();

            MusicPlayer.Verify(mp => mp.Resume(), Times.Once());
        }

        [TestMethod]
        public void CallMusicPlayerPauseWhenAPauseSongEventIsCatched()
        {
            new PlayerViewModel();

            PlayerObserver.NotifyPauseSong();

            MusicPlayer.Verify(mp => mp.Pause(), Times.Once());
        }
        
        [TestMethod]
        public void CallMusicPlayerStopWhenAStopSongEventIsCatched()
        {
            PlayerViewModel viewModel = new PlayerViewModel();

            PlayerObserver.NotifyStopSong();

            MusicPlayer.Verify(mp => mp.Stop());
            Assert.IsNull(viewModel.ElapsedTime);
            Assert.IsNull(viewModel.TotalTime);
            Assert.IsNull(viewModel.CurrentSong);
            Assert.AreEqual(0, viewModel.PurcentagePlayed);
        }

        [TestMethod]
        public void RaiseNewSongFinishedEventWhenASongIsFinishedAndCleanAllValue()
        {
            EventCatcher catcher = new EventCatcher();
            PlayerViewModel viewModel = new PlayerViewModel();

            MusicPlayer.Raise(mp => mp.SongFinished += null);

            Assert.IsTrue(catcher.SongFinishedInvoked);
            Assert.IsNull(viewModel.ElapsedTime);
            Assert.IsNull(viewModel.TotalTime);
            Assert.IsNull(viewModel.CurrentSong);
            Assert.AreEqual(0, viewModel.PurcentagePlayed);
        }

        [TestMethod]
        public void RefreshTimePlayed()
        {
            const int elapsedTime = 10;
            const int totalTime = 3690;

            PlayerViewModel viewModel = new PlayerViewModel();

            MusicPlayer.Raise(mp => mp.PurcentagePlayed += null, elapsedTime, totalTime );

            Assert.AreEqual(1000/20000 * 100, viewModel.PurcentagePlayed);
            Assert.AreEqual("00:00:10", viewModel.ElapsedTime);
            Assert.AreEqual("01:01:30", viewModel.TotalTime);
        }
    }
}
