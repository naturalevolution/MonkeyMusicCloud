using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.Observers;
using MonkeyMusicCloud.Client.ViewModels;
using MonkeyMusicCloud.Client.ViewModels.BodyViewModels;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Test.Helper;
using Moq;

namespace MonkeyMusicCloud.Test.Client.ViewModels
{
    [TestClass]
    public class AlbumDetailViewModelShould : ViewModelsBaseTest
    {
        [TestMethod]
        public void GetAlbumSongsWhenAlbumIsSetted()
        {
            const string album = "album";
            ObservableCollection<Song> expectedSongs = new ObservableCollection<Song>();
            AlbumDetailViewModel viewModel = new AlbumDetailViewModel();
            Service.Setup(s => s.GetByAlbum(album)).Returns(expectedSongs);

            viewModel.Album = album;

            Service.Verify(s => s.GetByAlbum(album), Times.Once());
            Assert.AreEqual(expectedSongs, viewModel.Songs);
        }
    }
}
