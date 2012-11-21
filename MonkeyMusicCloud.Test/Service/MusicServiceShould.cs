using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Domain.IRepository;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Repository;
using MonkeyMusicCloud.Service;
using MonkeyMusicCloud.Test.Helper;
using Moq;

namespace MonkeyMusicCloud.Test.Service
{
    [TestClass]
    public class MusicServiceShould
    {
        private Mock<SongRepository> Repository { get; set; }

        [TestInitialize]
        public void TestInitialize(){
            Repository = new Mock<SongRepository>();
        }

        [TestMethod]
        public void SetRepositoryAtTheInstantiate(){
            MusicService service = new MusicService(Repository.Object);
            Assert.AreEqual(Repository.Object, service.Repository);
        }

        [TestMethod]
        public void GetAllSongs(){
            ObservableCollection<Song> expectedSongs = new ObservableCollection<Song>();
            Repository.Setup(r => r.GetAll()).Returns(expectedSongs);
            MusicService service = new MusicService(Repository.Object);

            IList<Song> findMusics = service.GetAllSongs();

            Repository.Verify(r => r.GetAll(), Times.Once());
            Assert.AreEqual(expectedSongs, findMusics);
        }

        [TestMethod]
        public void SearchSongs()
        {
            const string filter = "filter";
            ObservableCollection<Song> expectedSongs = new ObservableCollection<Song>();
            Repository.Setup(r => r.GetByFilter(filter)).Returns(expectedSongs);
            MusicService service = new MusicService(Repository.Object);

            IList<Song> findMusics = service.SearchSongs(filter);

            Repository.Verify(r => r.GetByFilter(filter), Times.Once());
            Assert.AreEqual(expectedSongs, findMusics);
        }

        [TestMethod]
        public void SearchSongsAndReturnReturnAnEmptyListIfFilterIsEmpty()
        {
            ObservableCollection<Song> expectedSongs = new ObservableCollection<Song>();
            Repository.Setup(r => r.GetByFilter(It.IsAny<string>())).Returns(expectedSongs);
            MusicService service = new MusicService(Repository.Object);

            IList<Song> findMusics = service.SearchSongs(string.Empty);

            Repository.Verify(r => r.GetByFilter(It.IsAny<string>()), Times.Never());
            Assert.AreEqual(0, findMusics.Count);
        }

        [TestMethod]
        public void AddASong()
        {
            Song song = Create.Song();
            MusicService service = new MusicService(Repository.Object);

            service.AddASong(song);

            Repository.Verify(r => r.Add(song), Times.Once());
        }

        [TestMethod]
        public void GetSongsByAlbum()
        {
            const string album = "album";
            ObservableCollection<Song> expectedSongs = new ObservableCollection<Song>();
            Repository.Setup(r => r.GetByAlbum(album)).Returns(expectedSongs);
            MusicService service = new MusicService(Repository.Object);

            IList<Song> findMusics = service.GetByAlbum(album);

            Repository.Verify(r => r.GetByAlbum(album), Times.Once());
            Assert.AreEqual(expectedSongs, findMusics);
        }
    }
}
