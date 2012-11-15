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
        public void CallMusicPlayerPlayWhenAPlaySongEventIsCatched()
        {
            Song songToPlay = Create.Song();
            PlayerViewModel viewModel = new PlayerViewModel();

            EventsManager.InvokePlaySong(songToPlay);

            MusicPlayer.Verify(mp => mp.Play(songToPlay.File.Content), Times.Once());
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
