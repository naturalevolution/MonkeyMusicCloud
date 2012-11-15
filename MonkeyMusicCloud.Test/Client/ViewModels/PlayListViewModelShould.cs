using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.Events;
using MonkeyMusicCloud.Client.ViewModels;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Test.Helper;

namespace MonkeyMusicCloud.Test.Client.ViewModels
{
    [TestClass]
    public class PlayListViewModelShould : ViewModelsBaseTest
    {
        [TestMethod]
        public void RefreshSongWhenAddToPlayListEventIsCatched()
        {
            Song song1 = Create.Song();
            Song song2 = Create.Song();
            ObservableCollection<Song> songs= new ObservableCollection<Song> {song1};
            PlayListViewModel viewModel = new PlayListViewModel { SongList = new ObservableCollection<Song> { song2 } };

            EventsManager.InvokeAddToPlayList(songs);
            
            Assert.AreEqual(2, viewModel.SongList.Count);
            CollectionAssert.Contains(viewModel.SongList, song1);
            CollectionAssert.Contains(viewModel.SongList, song2);
        }

        [TestMethod]
        public void RaiseNewPlayDemandEvent()
        {
            EventCatcher eventCatcher = new EventCatcher();
            Song song = Create.Song();
            PlayListViewModel viewModel = new PlayListViewModel {SongList = new ObservableCollection<Song>{song}};
            
            viewModel.PlaySongCommand.Execute(song);

            Assert.IsTrue(eventCatcher.PlaySongInvoked);
            Assert.AreEqual(song,  eventCatcher.SongToPlay);
        }

        [TestMethod]
        public void DoNotRaiseTheEventIfTheListIsEmpty()
        {
            EventCatcher eventCatcher = new EventCatcher();
            PlayListViewModel viewModel = new PlayListViewModel();

            viewModel.PlaySongCommand.Execute(null);

            Assert.IsFalse(eventCatcher.PlaySongInvoked);
        }

        [TestMethod]
        public void RaiseThePlayEventWithTheFirstSongOfTheListIfNoSongIsSelected()
        {
            Song song1 = Create.Song();
            Song song2 = Create.Song();
            EventCatcher eventCatcher = new EventCatcher();
            PlayListViewModel viewModel = new PlayListViewModel {SongList = new ObservableCollection<Song> {song1, song2}};

            viewModel.PlaySongCommand.Execute(null);

            Assert.IsTrue(eventCatcher.PlaySongInvoked);
            Assert.AreEqual(song1, eventCatcher.SongToPlay);
            Assert.AreEqual(song1, viewModel.ActualPlayedSong);
        }

        [TestMethod]
        public void ChangeTheActualSelectedSongOnPlayDemand()
        {
            Song song1 = Create.Song();
            Song song2 = Create.Song();

            PlayListViewModel viewModel = new PlayListViewModel { SongList = new ObservableCollection<Song> { song1, song2 } };
            viewModel.PlaySongCommand.Execute(song2);

            Assert.AreEqual(song2, viewModel.ActualPlayedSong);
        }

        [TestMethod]
        public void DoNotDuplicateSongIntoSongListWhenAddToPlayListEventIsCatched()
        {
            Song song1 = Create.Song();
            Song song2 = Create.Song();
            ObservableCollection<Song> songs = new ObservableCollection<Song> { song1 };
            PlayListViewModel viewModel = new PlayListViewModel { SongList = new ObservableCollection<Song> { song2 } };

            EventsManager.InvokeAddToPlayList(songs);
            EventsManager.InvokeAddToPlayList(songs);

            Assert.AreEqual(2, viewModel.SongList.Count);
            CollectionAssert.Contains(viewModel.SongList, song1);
            CollectionAssert.Contains(viewModel.SongList, song2);
        }
        
        #region PreviousCommands
            [TestMethod]
            public void PlayPreviousSong()
            {
                Song song1 = Create.Song();
                Song song2 = Create.Song();
                EventCatcher eventCatcher = new EventCatcher();
                PlayListViewModel viewModel = new PlayListViewModel
                {
                    SongList = new ObservableCollection<Song> { song1, song2 },
                    ActualPlayedSong = song2
                };

                viewModel.PreviousSongCommand.Execute(null);

                Assert.IsTrue(eventCatcher.PlaySongInvoked);
                Assert.AreEqual(song1, eventCatcher.SongToPlay);
                Assert.AreEqual(song1, viewModel.ActualPlayedSong);
            }

            [TestMethod]
            public void DoNothingIfThereIsNoActualSongOnPreviousSongCommand()
            {
                EventCatcher eventCatcher = new EventCatcher();
                PlayListViewModel viewModel = new PlayListViewModel { SongList = new ObservableCollection<Song>() };

                viewModel.PreviousSongCommand.Execute(null);

                Assert.IsFalse(eventCatcher.PlaySongInvoked);
            }

            [TestMethod]
            public void DoNothingIfTheActualSOngIsTheFirstOfTheList()
            {
                Song song1 = Create.Song();
                Song song2 = Create.Song();
                EventCatcher eventCatcher = new EventCatcher();
                PlayListViewModel viewModel = new PlayListViewModel
                {
                    SongList = new ObservableCollection<Song> { song1, song2 },
                    ActualPlayedSong = song1
                };

                viewModel.PreviousSongCommand.Execute(null);

                Assert.IsFalse(eventCatcher.PlaySongInvoked);
                Assert.AreEqual(song1, viewModel.ActualPlayedSong);
            }
        #endregion

        #region NextCommands

            [TestMethod]
            public void PlayNextSong()
            {
                Song song1 = Create.Song();
                Song song2 = Create.Song();
                EventCatcher eventCatcher = new EventCatcher();
                PlayListViewModel viewModel = new PlayListViewModel
                {
                    SongList = new ObservableCollection<Song> { song1, song2 },
                    ActualPlayedSong = song1
                };

                viewModel.NextSongCommand.Execute(null);

                Assert.IsTrue(eventCatcher.PlaySongInvoked);
                Assert.AreEqual(song2, eventCatcher.SongToPlay);
                Assert.AreEqual(song2, viewModel.ActualPlayedSong);
            }

            [TestMethod]
            public void PlayNextSongWhenTheCurrentIsFinished()
            {
                Song song1 = Create.Song();
                Song song2 = Create.Song();
                EventCatcher eventCatcher = new EventCatcher();
                PlayListViewModel viewModel = new PlayListViewModel
                {
                    SongList = new ObservableCollection<Song> { song1, song2 },
                    ActualPlayedSong = song1
                };

                EventsManager.InvokeCurrentSongFinished();

                Assert.IsTrue(eventCatcher.PlaySongInvoked);
                Assert.AreEqual(song2, eventCatcher.SongToPlay);
                Assert.AreEqual(song2, viewModel.ActualPlayedSong);
            }

            [TestMethod]
            public void DoNothingIfThereIsNoActualSongOnNextSongCommand()
            {
                EventCatcher eventCatcher = new EventCatcher();
                PlayListViewModel viewModel = new PlayListViewModel { SongList = new ObservableCollection<Song>() };

                viewModel.NextSongCommand.Execute(null);

                Assert.IsFalse(eventCatcher.PlaySongInvoked);
            }

            [TestMethod]
            public void DoNothingIfThereIsNoActualSongOnSongFinished()
            {
                EventCatcher eventCatcher = new EventCatcher();
                PlayListViewModel viewModel = new PlayListViewModel { SongList = new ObservableCollection<Song>() };

                EventsManager.InvokeCurrentSongFinished();

                Assert.IsFalse(eventCatcher.PlaySongInvoked);
            }

            [TestMethod]
            public void DoNothingIfThereIsTheEndOfPlayListOnNextSongDemand()
            {
                Song song1 = Create.Song();
                EventCatcher eventCatcher = new EventCatcher();
                PlayListViewModel viewModel = new PlayListViewModel
                {
                    SongList = new ObservableCollection<Song> { song1 },
                    ActualPlayedSong = song1
                };

                viewModel.NextSongCommand.Execute(null);

                Assert.IsFalse(eventCatcher.PlaySongInvoked);
                Assert.AreEqual(song1, viewModel.ActualPlayedSong);
            }

            [TestMethod]
            public void ClearPlayerIfThereIsTheEndOfPlayListOnSongFinished()
            {
                Song song1 = Create.Song();
                EventCatcher eventCatcher = new EventCatcher();
                PlayListViewModel viewModel = new PlayListViewModel
                {
                    SongList = new ObservableCollection<Song> { song1 },
                    ActualPlayedSong = song1
                };

                EventsManager.InvokeCurrentSongFinished();

                Assert.IsFalse(eventCatcher.PlaySongInvoked);
                Assert.IsNull(viewModel.ActualPlayedSong);
            }

        #endregion
    }
}
