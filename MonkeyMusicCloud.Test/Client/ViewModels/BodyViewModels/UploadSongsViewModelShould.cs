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
    public class UploadSongsViewModelShould : ViewModelsBaseTest
    {
        [TestMethod]
        public void CallServiceWhenAddSongMethodIsHandled()
        {
            MediaFile mediaFile1 = Create.MediaFile();
            MediaFile mediaFile2 = Create.MediaFile();
            MediaFile mediaFile3 = Create.MediaFile();
            SongToAdd songToAdd1 = new SongToAdd() { IsSelected = true, Song = Create.Song(), MediaFile = mediaFile1 };
            SongToAdd songToAdd2 = new SongToAdd() { IsSelected = false, Song = Create.Song(), MediaFile = mediaFile2 };
            SongToAdd songToAdd3 = new SongToAdd() { IsSelected = true, Song = Create.Song(), MediaFile = mediaFile3 };
            


            UploadSongsViewModel viewModel = new UploadSongsViewModel
                                              {
                                                  SongsToAdd = new ObservableCollection<SongToAdd>
                                                                   {
                                                                      songToAdd1,
                                                                      songToAdd2,
                                                                      songToAdd3,
                                                                   }
                                              };

            viewModel.AddSongCommand.Execute(null);

            Service.Verify(s => s.AddASong(songToAdd1.Song, mediaFile1), Times.Once());
            Service.Verify(s => s.AddASong(songToAdd2.Song, mediaFile2), Times.Never());
            Service.Verify(s => s.AddASong(songToAdd3.Song, mediaFile3), Times.Once());
        }

        [TestMethod]
        public void DoNothingIfThereIsMusicToAddAreNotSelected()
        {
            Song songToAdd = Create.Song();
            UploadSongsViewModel viewModel = new UploadSongsViewModel { SongsToAdd = new ObservableCollection<SongToAdd> { new SongToAdd { IsSelected = false, Song = songToAdd } } };

            viewModel.AddSongCommand.Execute(null);

            Service.Verify(s => s.AddASong(It.IsAny<Song>(), It.IsAny<MediaFile>()), Times.Never());
        }

        [TestMethod]
        public void DoNothingIfThereIsNoMusicToAdd()
        {
            UploadSongsViewModel viewModel = new UploadSongsViewModel { SongsToAdd = new ObservableCollection<SongToAdd>() };

            viewModel.AddSongCommand.Execute(null);

            Service.Verify(s => s.AddASong(It.IsAny<Song>(), It.IsAny<MediaFile>()), Times.Never());
        }

        [TestMethod]
        public void DoNothingIfTheSongsAddListIsNull()
        {
            UploadSongsViewModel viewModel = new UploadSongsViewModel();

            viewModel.AddSongCommand.Execute(null);

            StreamHelper.Verify(sh => sh.ReadToEnd(It.IsAny<string>()), Times.Never());
            Service.Verify(s => s.AddASong(It.IsAny<Song>(), It.IsAny<MediaFile>()), Times.Never());
        }

        /// <summary>
        /// "Maid with the Flaxen Hair" => test1.mp3 dans le dossier TestFiles\mp3
        /// "Sleep Away" => test2.mp3 dans le dossier TestFiles
        /// </summary>
        [TestMethod]
        public void GetAllLocalMusicFromARootNodeAndGetTheRealNameOfTheseOne()
        {
            string rootPath = AppDomain.CurrentDomain.BaseDirectory + "\\Helper\\TestFiles";
            UploadSongsViewModel viewModel = new UploadSongsViewModel
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
            UploadSongsViewModel viewModel = new UploadSongsViewModel
            {
                RootPath = AppDomain.CurrentDomain.BaseDirectory + "\\unknown"
            };

            Assert.AreEqual(0, viewModel.SongsToAdd.Count);
        }
    }
}
