using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.ViewModels.BodyViewModels;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Test.Helper;

namespace MonkeyMusicCloud.Test.Client.ViewModels
{
    [TestClass]
    public class SongListViewModelShould : ViewModelsBaseTest
    {
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
