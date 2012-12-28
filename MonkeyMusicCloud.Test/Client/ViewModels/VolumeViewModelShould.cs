using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.ViewModels;
using Moq;

namespace MonkeyMusicCloud.Test.Client.ViewModels
{
    [TestClass]
    public class VolumeViewModelShould : ViewModelsBaseTest
    {
        [TestMethod]
        public void InstantiateCorrectly()
        {
            VolumeViewModel viewModel = new VolumeViewModel();

            Assert.AreEqual(1, viewModel.Volume);
        }

        [TestMethod]
        public void CallMusicPlayerChangeVolumeWhenVolumePropertyIsSetted()
        {
            const int volume = 20;
            VolumeViewModel viewModel = new VolumeViewModel {Volume = volume};

            MockMusicPlayer.Verify(mp => mp.ChangeVolume(volume), Times.Once());
            Assert.AreEqual(volume, viewModel.Volume);
        }
    }
}
