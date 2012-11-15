using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Domain.IRepository;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Service;
using MonkeyMusicCloud.Test.Helper;
using Moq;

namespace MonkeyMusicCloud.Test.Service
{
    [TestClass]
    public class MusicServiceShould
    {
        private Mock<IRepository<Song>> Repository { get; set; }

        [TestInitialize]
        public void TestInitialize(){
            Repository = new Mock<IRepository<Song>>();
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
        public void AddASong()
        {
            Song song = Create.Song();
            MusicService service = new MusicService(Repository.Object);

            service.AddASong(song);

            Repository.Verify(r => r.Add(song), Times.Once());
        }
    }
}
