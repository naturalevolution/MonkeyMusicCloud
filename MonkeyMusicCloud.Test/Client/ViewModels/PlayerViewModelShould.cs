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
            new PlayerViewModel();

            EventsManager.InvokePlayNewSong(songToPlay);

            MusicPlayer.Verify(mp => mp.Stop(), Times.Once());
            MusicPlayer.Verify(mp => mp.Play(songToPlay.File.Content), Times.Once());
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
        public void RaiseNewSongFinishedEventWhenASongIsFinished()
        {
            EventCatcher catcher = new EventCatcher();
            new PlayerViewModel();

            MusicPlayer.Raise(mp => mp.SongFinished += null);

            Assert.IsTrue(catcher.SongFinishedInvoked);

            
        }

        [TestMethod]
        public void RefreshTimePlayed()
        {
            const int purcentage = 50;
            PlayerViewModel viewModel = new PlayerViewModel();

            MusicPlayer.Raise(mp => mp.PurcentagePlayed += null, purcentage);

            Assert.AreEqual(purcentage, viewModel.PurcentagePlayed);
        }
    }
}
