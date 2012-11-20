using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.Events;
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
        public void ACallMusicPlayerPlayWhenAPlaySongEventIsCatched()
        {
            Song songToPlay = Create.Song();
            PlayerViewModel viewModel = new PlayerViewModel();

            EventsManager.InvokePlayNewSong(songToPlay);

            MusicPlayer.Verify(mp => mp.Stop(), Times.Once());
            MusicPlayer.Verify(mp => mp.Play(songToPlay.File.Content), Times.Once());
            Assert.AreEqual(songToPlay, viewModel.CurrentSong);
        }


        [TestMethod]
        public void CallMusicPlayerResumeWhenAResumeSongEventIsCatched()
        {
            new PlayerViewModel();

            EventsManager.InvokeResumeSong();

            MusicPlayer.Verify(mp => mp.Resume(), Times.Once());
        }

        [TestMethod]
        public void CallMusicPlayerPauseWhenAPauseSongEventIsCatched()
        {
            new PlayerViewModel();

            EventsManager.InvokePauseSong();

            MusicPlayer.Verify(mp => mp.Pause(), Times.Once());
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
