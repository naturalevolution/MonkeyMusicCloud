using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        private Mock<SongRepository> SongRepository { get; set; }
        private Mock<Repository<MediaFile>> MediaFileRepository { get; set; }

        [TestInitialize]
        public void TestInitialize(){
            SongRepository = new Mock<SongRepository>();
            MediaFileRepository = new Mock<Repository<MediaFile>>();
        }

        [TestMethod]
        public void SetRepositoryAtTheInstantiate(){
            MusicService service = new MusicService(SongRepository.Object, MediaFileRepository.Object);
            Assert.AreEqual(SongRepository.Object, service.SongRepository);
            Assert.AreEqual(MediaFileRepository.Object, service.MediaFileRepository);
        }

        [TestMethod]
        public void GetAllSongs(){
            ObservableCollection<Song> expectedSongs = new ObservableCollection<Song>();
            SongRepository.Setup(r => r.GetAll()).Returns(expectedSongs);
            MusicService service = new MusicService(SongRepository.Object, MediaFileRepository.Object);

            IList<Song> findMusics = service.GetAllSongs();

            SongRepository.Verify(r => r.GetAll(), Times.Once());
            Assert.AreEqual(expectedSongs, findMusics);
        }

        [TestMethod]
        public void SearchSongs()
        {
            const string filter = "filter";
            ObservableCollection<Song> expectedSongs = new ObservableCollection<Song>();
            SongRepository.Setup(r => r.GetByFilter(filter)).Returns(expectedSongs);
            MusicService service = new MusicService(SongRepository.Object, MediaFileRepository.Object);

            IList<Song> findMusics = service.SearchSongs(filter);

            SongRepository.Verify(r => r.GetByFilter(filter), Times.Once());
            Assert.AreEqual(expectedSongs, findMusics);
        }

        [TestMethod]
        public void SearchSongsAndReturnReturnAnEmptyListIfFilterIsEmpty()
        {
            ObservableCollection<Song> expectedSongs = new ObservableCollection<Song>();
            SongRepository.Setup(r => r.GetByFilter(It.IsAny<string>())).Returns(expectedSongs);
            MusicService service = new MusicService(SongRepository.Object, MediaFileRepository.Object);

            IList<Song> findMusics = service.SearchSongs(string.Empty);

            SongRepository.Verify(r => r.GetByFilter(It.IsAny<string>()), Times.Never());
            Assert.AreEqual(0, findMusics.Count);
        }

        [TestMethod]
        public void AddASong()
        {
            Song song = Create.Song();
            MusicService service = new MusicService(SongRepository.Object, MediaFileRepository.Object);
            MediaFile file = new MediaFile();
            MediaFileRepository.Setup(mfr => mfr.Add(file)).Returns(file);

            service.AddASong(song, file);

            SongRepository.Verify(r => r.Add(song), Times.Once());
            MediaFileRepository.Verify(r => r.Add(file), Times.Once());
            Assert.AreEqual(song.MediaFileId, file.Id);
        }

        [TestMethod]
        public void GetSongsByAlbum()
        {
            const string album = "album";
            ObservableCollection<Song> expectedSongs = new ObservableCollection<Song>();
            SongRepository.Setup(r => r.GetByAlbum(album)).Returns(expectedSongs);
            MusicService service = new MusicService(SongRepository.Object, MediaFileRepository.Object);

            IList<Song> findMusics = service.GetByAlbum(album);

            SongRepository.Verify(r => r.GetByAlbum(album), Times.Once());
            Assert.AreEqual(expectedSongs, findMusics);
        }

        [TestMethod]
        public void GetMediaFileById()
        {
            Guid id = new Guid();
            MediaFile expectedFile =  Create.MediaFile(id);
            MediaFileRepository.Setup(mfr => mfr.GetById(id)).Returns(expectedFile);

            MusicService service = new MusicService(SongRepository.Object, MediaFileRepository.Object);

            MediaFile gettedFile = service.GetMediaFileById(id);

            MediaFileRepository.Verify(mfr => mfr.GetById(id), Times.Once());
            Assert.AreEqual(expectedFile, gettedFile);
        }
    }
}
