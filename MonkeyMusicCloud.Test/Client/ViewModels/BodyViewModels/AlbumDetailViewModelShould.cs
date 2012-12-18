using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.ViewModels.BodyViewModels;
using MonkeyMusicCloud.Domain.Model;
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
            MockService.Setup(s => s.GetByAlbum(album)).Returns(expectedSongs);

            viewModel.Album = album;

            MockService.Verify(s => s.GetByAlbum(album), Times.Once());
            Assert.AreEqual(expectedSongs, viewModel.SongList);
        }

        [TestMethod]
        public void LoadImagePath()
        {
            const string album = "album";
            const string path = "path";
            AlbumDetailViewModel viewModel = new AlbumDetailViewModel();
            MockImageSearch.Setup(i => i.GetImagePath(album)).Returns(path);

            viewModel.Album = album;

            MockImageSearch.Verify(i => i.GetImagePath(album), Times.Once());
            Assert.AreEqual(path, viewModel.AlbumImagePath );
        }
    }
}
