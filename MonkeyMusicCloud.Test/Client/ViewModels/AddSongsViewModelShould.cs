using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.ViewModels.BodyViewModels;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Test.Helper;
using Moq;

namespace MonkeyMusicCloud.Test.Client.ViewModels
{
    [TestClass]
    public class AddSongsViewModelShould : ViewModelsBaseTest
    {
        [TestMethod]
        public void CallServiceWhenAddSongMethodIsHandled()
        {
            Song songToAdd1 = Create.Song();
            Song songToAdd2 = Create.Song();
            Song songToAdd3 = Create.Song();
            AddSongsViewModel viewModel = new AddSongsViewModel
                                              {
                                                  SongsToAdd = new ObservableCollection<SongToAdd>
                                                                   {
                                                                       new SongToAdd {IsSelected = true, Song = songToAdd1},
                                                                       new SongToAdd {IsSelected = false, Song = songToAdd2},
                                                                       new SongToAdd {IsSelected = true, Song = songToAdd3},
                                                                   }
                                              };

            viewModel.AddSongCommand.Execute(null);

            Service.Verify(s => s.AddASong(songToAdd1), Times.Once());
            Service.Verify(s => s.AddASong(songToAdd2), Times.Never());
            Service.Verify(s => s.AddASong(songToAdd3), Times.Once());
        }

        [TestMethod]
        public void DoNothingIfThereIsMusicToAddAreNotSelected()
        {
            Song songToAdd = Create.Song();
            AddSongsViewModel viewModel = new AddSongsViewModel { SongsToAdd = new ObservableCollection<SongToAdd> { new SongToAdd { IsSelected = false, Song = songToAdd } } };

            viewModel.AddSongCommand.Execute(null);

            Service.Verify(s => s.AddASong(It.IsAny<Song>()), Times.Never());
        }

        [TestMethod]
        public void DoNothingIfThereIsNoMusicToAdd()
        {
            AddSongsViewModel viewModel = new AddSongsViewModel { SongsToAdd = new ObservableCollection<SongToAdd>() };

            viewModel.AddSongCommand.Execute(null);

            Service.Verify(s => s.AddASong(It.IsAny<Song>()), Times.Never());
        }

        [TestMethod]
        public void DoNothingIfTheSongsAddListIsNull()
        {
            AddSongsViewModel viewModel = new AddSongsViewModel();

            viewModel.AddSongCommand.Execute(null);

            StreamHelper.Verify(sh => sh.ReadToEnd(It.IsAny<string>()), Times.Never());
            Service.Verify(s => s.AddASong(It.IsAny<Song>()), Times.Never());
        }

        /// <summary>
        /// "Maid with the Flaxen Hair" => test1.mp3 dans le dossier TestFiles\mp3
        /// "Sleep Away" => test2.mp3 dans le dossier TestFiles
        /// </summary>
        [TestMethod]
        public void GetAllLocalMusicFromARootNodeAndGetTheRealNameOfTheseOne()
        {
            string rootPath = AppDomain.CurrentDomain.BaseDirectory + "\\Helper\\TestFiles";
            AddSongsViewModel viewModel = new AddSongsViewModel
                                              {
                                                  RootPath = rootPath
                                              };

            Assert.AreEqual(2, viewModel.SongsToAdd.Count);
            Assert.IsTrue(viewModel.SongsToAdd.All(s => s.IsSelected));
            CollectionAssert.Contains(viewModel.SongsToAdd.Select(s => s.Song.Title).ToList(), "Maid with the Flaxen Hair");
            CollectionAssert.Contains(viewModel.SongsToAdd.Select(s => s.Song.Album).ToList(), "Fine Music, Vol. 1");
            CollectionAssert.Contains(viewModel.SongsToAdd.Select(s => s.Song.Artist).ToList(), "Richard Stoltzman");
            CollectionAssert.Contains(viewModel.SongsToAdd.Select(s => s.Path).ToList(), rootPath + "\\mp3\\test1.mp3");
            CollectionAssert.Contains(viewModel.SongsToAdd.Select(s => s.Song.Title).ToList(), "Sleep Away");
            CollectionAssert.Contains(viewModel.SongsToAdd.Select(s => s.Song.Album).ToList(), "Bob Acri");
            CollectionAssert.Contains(viewModel.SongsToAdd.Select(s => s.Song.Artist).ToList(), "Bob Acri");
            CollectionAssert.Contains(viewModel.SongsToAdd.Select(s => s.Path).ToList(), rootPath + "\\test2.mp3");
        }

        [TestMethod]
        public void DoNothingIfRootPathDoesNotExist()
        {
            AddSongsViewModel viewModel = new AddSongsViewModel
            {
                RootPath = AppDomain.CurrentDomain.BaseDirectory + "\\unknown"
            };

            Assert.AreEqual(0, viewModel.SongsToAdd.Count);
        }
    }
}
