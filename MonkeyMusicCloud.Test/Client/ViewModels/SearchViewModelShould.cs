using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.ViewModels.BodyViewModels;
using MonkeyMusicCloud.Domain.Model;
using Moq;

namespace MonkeyMusicCloud.Test.Client.ViewModels
{
    [TestClass]
    public class SearchViewModelShould : ViewModelsBaseTest
    {
        [TestMethod]
        public void CallServiceForSearchMusicsByFilter()
        {
            const string filter = "filter";
            ObservableCollection<Song> songs = new ObservableCollection<Song>();
            Service.Setup(s => s.SearchSongs(filter)).Returns(songs);
            SearchViewModel viewModel = new SearchViewModel();

            viewModel.SearchSongsListCommand.Execute(filter);

            Service.Verify(s => s.SearchSongs(filter), Times.Once());
            Assert.AreEqual(songs, viewModel.SongList);
        }

    }
}
