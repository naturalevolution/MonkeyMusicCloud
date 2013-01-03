using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.ViewModels.BodyViewModels;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels.Tasks;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Resource;
using MonkeyMusicCloud.Test.Helper;
using MonkeyMusicCloud.Test.Helper.EventCatchers;

namespace MonkeyMusicCloud.Test.Client.ViewModels.BodyViewModels
{
    [TestClass]
    public class SongListViewModelShould : ViewModelsBaseTest
    {
        [TestMethod]
        public void RaiseAddToPlayListEvent()
        {
            Song expectedSong = Create.Song();
            PlayerEventCatcher eventCatcher = new PlayerEventCatcher();
            SongListViewModel viewModel = new SongListViewModel();

            viewModel.AddOneSongToPlayListCommand.Execute(expectedSong);

            Assert.IsTrue(eventCatcher.AddToPlayListInvoked);
            Assert.AreEqual(1, eventCatcher.AddToPlayListSong.Count);
            CollectionAssert.Contains(eventCatcher.AddToPlayListSong, expectedSong);

        }  
        
        [TestMethod]
        public void RaiseAddToPlayListEventWithSeveralSong()
        {
            Song expectedSong1 = Create.Song();
            Song expectedSong2 = Create.Song();
            ObservableCollection<Song> expectedSongs = new ObservableCollection<Song>(){expectedSong1, expectedSong2};

            PlayerEventCatcher eventCatcher = new PlayerEventCatcher();
            SongListViewModel viewModel = new SongListViewModel();

            viewModel.AddSongsToPlayListCommand.Execute(expectedSongs);

            Assert.IsTrue(eventCatcher.AddToPlayListInvoked);
            Assert.AreEqual(2, eventCatcher.AddToPlayListSong.Count);
            CollectionAssert.Contains(eventCatcher.AddToPlayListSong, expectedSong1);
            CollectionAssert.Contains(eventCatcher.AddToPlayListSong, expectedSong1);
        }


        [TestMethod]
        public void RaiseDownloadTaskEventWith()
        {
            Song expectedSong = Create.Song();

            TaskEventCatcher eventCatcher = new TaskEventCatcher();
            SongListViewModel viewModel = new SongListViewModel();

            viewModel.DownloadOneSongCommand.Execute(expectedSong);

            Assert.IsTrue(eventCatcher.AddTaskInvoked);
            Assert.AreEqual(1, eventCatcher.AddTaskTimes);
            Assert.AreEqual(1, eventCatcher.TaskListToAdd.Count);
            Assert.IsTrue( eventCatcher.TaskListToAdd[0] is DownloadTask);
            DownloadTask expectedTask = (DownloadTask) eventCatcher.TaskListToAdd[0];
            Assert.AreEqual(expectedSong, expectedTask.Song);
        }


        [TestMethod]
        public void RaiseDownloadTaskEventWithSeveralSongs()
        {
            Song expectedSong1 = Create.Song();
            Song expectedSong2 = Create.Song();

            TaskEventCatcher eventCatcher = new TaskEventCatcher();
            SongListViewModel viewModel = new SongListViewModel();

            viewModel.DownloadSongListCommand.Execute(new ObservableCollection<Song>
                {
                    expectedSong1, 
                    expectedSong2
                });

            Assert.IsTrue(eventCatcher.AddTaskInvoked);
            Assert.AreEqual(2, eventCatcher.AddTaskTimes);
            Assert.AreEqual(2, eventCatcher.TaskListToAdd.Count);
            Assert.IsTrue(eventCatcher.TaskListToAdd[0] is DownloadTask);
            Assert.IsTrue(eventCatcher.TaskListToAdd[1] is DownloadTask);
            DownloadTask expectedTask1 = (DownloadTask)eventCatcher.TaskListToAdd[0];
            Assert.AreEqual(expectedSong1, expectedTask1.Song);
            DownloadTask expectedTask2 = (DownloadTask)eventCatcher.TaskListToAdd[1];
            Assert.AreEqual(expectedSong2, expectedTask2.Song);
        }

        [TestMethod]
        public void DoNothingIfParameterIsNull()
        {
            PlayerEventCatcher eventCatcher = new PlayerEventCatcher();
            SongListViewModel viewModel = new SongListViewModel();

            viewModel.AddSongsToPlayListCommand.Execute(null);

            Assert.IsFalse(eventCatcher.AddToPlayListInvoked);
        }

        [TestMethod]
        public void RaiseOpenNewAlbumView()
        {
            const string album = "album";
            SongListViewModel viewModel = new SongListViewModel();
            ContentBodyEventCatcher catcher = new ContentBodyEventCatcher();

            viewModel.OpenAlbumCommand.Execute(album);
            
            Assert.IsTrue(catcher.ChangeContentViewInvoked);
            Assert.AreEqual(string.Format(MusicResource.AlbumTab, album), catcher.MenuItem.Label);
        }

        [TestMethod]
        public void RaiseOpenNewArtistView()
        {
            const string artist = "artist";
            SongListViewModel viewModel = new SongListViewModel();
            ContentBodyEventCatcher catcher = new ContentBodyEventCatcher();

            viewModel.OpenArtistCommand.Execute(artist);
            
            Assert.IsTrue(catcher.ChangeContentViewInvoked);
            Assert.AreEqual(string.Format(MusicResource.ArtistTab, artist), catcher.MenuItem.Label);
        }
    }
}
