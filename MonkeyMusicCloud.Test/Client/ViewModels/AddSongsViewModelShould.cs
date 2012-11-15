using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.ViewModels;
using MonkeyMusicCloud.Domain.Model;
using Moq;

namespace MonkeyMusicCloud.Test.Client.ViewModels
{
    [TestClass]
    public class AddSongsViewModelShould : ViewModelsBaseTest
    {

        [TestMethod]
        public void CallServiceWhenAddSongMethodIsHandled()
        {
            const string songPath = "path";
            byte [] expectedFileContent = new byte[5];
            StreamHelper.Setup(sh => sh.ReadToEnd(songPath)).Returns(expectedFileContent);
            AddSongsViewModel viewModel = new AddSongsViewModel { SongPath = songPath };

            viewModel.AddSongExecute();

            StreamHelper.Verify(sh => sh.ReadToEnd(songPath), Times.Once());
            Service.Verify(s => s.AddASong(It.Is<Song>(song => song.File.Content == expectedFileContent)), Times.Once());
        }

        [TestMethod]
        public void DoAnythingIfThePathIsEmpty()
        {
            AddSongsViewModel viewModel = new AddSongsViewModel {SongPath = string.Empty};

            viewModel.AddSongExecute();

            StreamHelper.Verify(sh => sh.ReadToEnd(It.IsAny<string>()), Times.Never());
            Service.Verify(s => s.AddASong(It.IsAny<Song>()), Times.Never());
        }

        [TestMethod]
        public void DoAnythingIfThePathIsNull()
        {
            AddSongsViewModel viewModel = new AddSongsViewModel {SongPath = null};

            viewModel.AddSongExecute();

            StreamHelper.Verify(sh => sh.ReadToEnd(It.IsAny<string>()), Times.Never());
            Service.Verify(s => s.AddASong(It.IsAny<Song>()), Times.Never());
        }
    }
}
