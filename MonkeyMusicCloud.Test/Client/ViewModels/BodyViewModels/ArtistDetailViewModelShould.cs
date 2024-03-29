﻿using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.ViewModels.BodyViewModels;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Test.Helper;
using MonkeyMusicCloud.Test.Helper.EventCatchers;
using Moq;

namespace MonkeyMusicCloud.Test.Client.ViewModels
{
    [TestClass]
    public class ArtistDetailViewModelShould : ViewModelsBaseTest
    {
        [TestMethod]
        public void GetArtistAlbumsAndArtistSongsWhenArtistIsSetted()
        {
            const string artist = "artist";
            ObservableCollection<string> expectedAlbums = new ObservableCollection<string>();
            ObservableCollection<Song> expectedSongs = new ObservableCollection<Song>();
            ArtistDetailViewModel viewModel = new ArtistDetailViewModel();
            MockService.Setup(s => s.GetAlbumsByArtist(artist)).Returns(expectedAlbums);
            MockService.Setup(s => s.GetByArtist(artist)).Returns(expectedSongs);

            viewModel.Artist = artist;

            MockService.Verify(s => s.GetAlbumsByArtist(artist), Times.Once());
            MockService.Verify(s => s.GetByArtist(artist), Times.Once());
            Assert.AreEqual(expectedAlbums, viewModel.AlbumList);
            Assert.AreEqual(expectedSongs, viewModel.SongList);
        }

        [TestMethod]
        public void RaiseOpenNewArtistView()
        {
            const string album = "artist";
            ArtistDetailViewModel viewModel = new ArtistDetailViewModel();
            ContentBodyEventCatcher catcher = new ContentBodyEventCatcher();

            viewModel.OpenAlbumCommand.Execute(album);

            Assert.IsTrue(catcher.ChangeContentViewInvoked);
            Assert.AreEqual(album, catcher.MenuItem.Label);
        }
    }
}
