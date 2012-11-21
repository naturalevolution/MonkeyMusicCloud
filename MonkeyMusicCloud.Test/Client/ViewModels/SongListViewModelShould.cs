using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.ViewModels.BodyViewModels;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Test.Helper;
using Moq;

namespace MonkeyMusicCloud.Test.Client.ViewModels
{
    [TestClass]
    public class SongListViewModelShould : ViewModelsBaseTest
    {
        [TestMethod]
        public void GetAllSongsAfterRefresh()
        {

            ObservableCollection<Song> songs = new ObservableCollection<Song>();
            Service.Setup(s => s.GetAllSongs()).Returns(songs);
            SongListViewModel viewModel = new SongListViewModel();

            viewModel.RefreshSongListCommand.Execute(null);
            
            Service.Verify(s => s.GetAllSongs(), Times.Once());
            Assert.AreEqual(songs, viewModel.SongList);
        }

        [TestMethod]
        public void RaiseAddToPlayListEvent()
        {
            Song expectedSong = Create.Song();
            EventCatcher eventCatcher = new EventCatcher();
            SongListViewModel viewModel = new SongListViewModel();

            viewModel.AddSongToPlayListCommand.Execute(expectedSong);

            Assert.IsTrue(eventCatcher.AddToPlayListInvoked);
            Assert.AreEqual(expectedSong,eventCatcher.AddToPlayListSong);
        }

        [TestMethod]
        public void CallServiceForSearchMusicsByFilter()
        {
            const string filter = "filter";
            ObservableCollection<Song> songs = new ObservableCollection<Song>();
            Service.Setup(s => s.SearchSongs(filter)).Returns(songs);
            SongListViewModel viewModel = new SongListViewModel();

            viewModel.SearchSongsListCommand.Execute(filter);

            Service.Verify(s => s.SearchSongs(filter), Times.Once());
            Assert.AreEqual(songs, viewModel.SongList);
        }

        [TestMethod]
        public void RaiseOpenNewAlbumView()
        {
            const string album = "album";
            SongListViewModel viewModel = new SongListViewModel();
            EventCatcher catcher = new EventCatcher();

            viewModel.OpenAlbumCommand.Execute(album);
            
            Assert.IsTrue(catcher.ChangeContentViewInvoked);
            Assert.AreEqual(album, catcher.Item.Label);
        }
    }
}
