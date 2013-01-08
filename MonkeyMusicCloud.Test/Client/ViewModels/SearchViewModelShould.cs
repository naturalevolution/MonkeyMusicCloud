using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.ViewModels.BodyViewModels;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Test.Helper.EventCatchers;
using Moq;

namespace MonkeyMusicCloud.Test.Client.ViewModels
{
    [TestClass]
    public class SearchViewModelShould : ViewModelsBaseTest
    {
        

        [TestMethod]
        public void InitializeCorrectly()
        {
            SearchViewModel viewModel = new SearchViewModel();

            Assert.IsTrue(viewModel.SearchOnTitle);
            Assert.IsTrue(viewModel.SearchOnArtist);
            Assert.IsTrue(viewModel.SearchOnAlbum);
        }

        [TestMethod]
        public void CallServiceAndRaiseNewSearchEventWithFilter()
        {
            const string filter = "filter";
            ContentBodyEventCatcher catcher = new ContentBodyEventCatcher();
            ObservableCollection<Song> songs = new ObservableCollection<Song>();
            MockService.Setup(s => s.SearchSongs(filter, true, true, true)).Returns(songs);
            SearchViewModel viewModel = new SearchViewModel();

            viewModel.SearchSongsListCommand.Execute(filter);

            MockService.Verify(s => s.SearchSongs(filter, true, true, true), Times.Once());
            Assert.IsTrue(catcher.NewSearchInvoked);
            Assert.AreEqual(songs, catcher.SearchSongList);
            Assert.AreEqual(filter, catcher.SearchFilter);
        }


        [TestMethod]
        public void CallOnlySearchTitle()
        {
            const string filter = "filter";
            SearchViewModel viewModel = new SearchViewModel() { SearchOnAlbum = false, SearchOnArtist = false };

            viewModel.SearchSongsListCommand.Execute(filter);

            MockService.Verify(s => s.SearchSongs(filter, true, false, false), Times.Once());
        }

        [TestMethod]
        public void CallOnlySearchArtist()
        {
            const string filter = "filter";
            SearchViewModel viewModel = new SearchViewModel() { SearchOnAlbum = false, SearchOnTitle = false };

            viewModel.SearchSongsListCommand.Execute(filter);

            MockService.Verify(s => s.SearchSongs(filter, false, true, false), Times.Once());
        }


        [TestMethod]
        public void CallOnlySearchAlbum()
        {
            const string filter = "filter";
            SearchViewModel viewModel = new SearchViewModel() { SearchOnArtist = false, SearchOnTitle = false };

            viewModel.SearchSongsListCommand.Execute(filter);

            MockService.Verify(s => s.SearchSongs(filter, false, false, true), Times.Once());
        }

        [TestMethod]
        public void CallOnlySearchOnTitleAndArtist()
        {
            const string filter = "filter";
            SearchViewModel viewModel = new SearchViewModel() { SearchOnAlbum = false };

            viewModel.SearchSongsListCommand.Execute(filter);

            MockService.Verify(s => s.SearchSongs(filter, true, true, false), Times.Once());
        }

        [TestMethod]
        public void CallOnlySearchOnTitleAndAlbum()
        {
            const string filter = "filter";
            SearchViewModel viewModel = new SearchViewModel() { SearchOnArtist = false };

            viewModel.SearchSongsListCommand.Execute(filter);

            MockService.Verify(s => s.SearchSongs(filter, true, false, true), Times.Once());
        }

        [TestMethod]
        public void CallOnlySearchOnArtistAndAlbum()
        {
            const string filter = "filter";
            SearchViewModel viewModel = new SearchViewModel() { SearchOnTitle = false };

            viewModel.SearchSongsListCommand.Execute(filter);

            MockService.Verify(s => s.SearchSongs(filter, false, true, true), Times.Once());
        }
    }
}
