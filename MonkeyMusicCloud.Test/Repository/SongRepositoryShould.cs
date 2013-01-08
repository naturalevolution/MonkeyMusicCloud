using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Repository;

namespace MonkeyMusicCloud.Test.Repository
{
    [TestClass]
    public class SongRepositoryShould : BaseRepositoryTests
    {
        private SongRepository Repository { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            Repository = new SongRepository();
        }
        [TestMethod]
        public void GetMusicByFilter()
        {
            Song expectedMusic1 = new Song { Album = "fIlter", Artist = "fail1",  MediaFileId = new Guid(), Title = "fail1" };
            Song expectedMusic2 = new Song { Album = "fail2", Artist = "fail2", MediaFileId = new Guid(), Title = "fIlTer" };
            Song expectedMusic3 = new Song { Album = "fail3", Artist = "fIlTer", MediaFileId = new Guid(), Title = "fail3" };
            Song expectedMusic4 = new Song { Album = "fail4", Artist = "fail4", MediaFileId = new Guid(), Title = "fail4" };

            Repository.Add(expectedMusic1);
            Repository.Add(expectedMusic2);
            Repository.Add(expectedMusic3);
            Repository.Add(expectedMusic4);
            IList<Song> gettedMusics = Repository.GetByFilter("ilt", true, true, true);

            Assert.AreEqual(3, gettedMusics.Count);
            CollectionAssert.Contains(gettedMusics.Select(g => g.Title).ToList(), expectedMusic1.Title);
            CollectionAssert.Contains(gettedMusics.Select(g => g.Title).ToList(), expectedMusic2.Title);
            CollectionAssert.Contains(gettedMusics.Select(g => g.Title).ToList(), expectedMusic3.Title);
            CollectionAssert.DoesNotContain(gettedMusics.Select(g => g.Title).ToList(), expectedMusic4.Title);
        }

        [TestMethod]
        public void GetMusicByAlbum()
        {

            Song expectedMusic1 = new Song { Album = "album1", Artist = "fail1", MediaFileId = new Guid(), Title = "fail1" };
            Song expectedMusic2 = new Song { Album = "album1", Artist = "fail2", MediaFileId = new Guid(), Title = "fIlTer" };
            Song expectedMusic3 = new Song { Album = "album2", Artist = "fIlTer", MediaFileId = new Guid(), Title = "fail3" };
            Song expectedMusic4 = new Song { Album = "album1", Artist = "fail4", MediaFileId = new Guid(), Title = "fail4" };
            
            Repository.Add(expectedMusic1);
            Repository.Add(expectedMusic2);
            Repository.Add(expectedMusic3);
            Repository.Add(expectedMusic4);
            IList<Song> gettedMusics = Repository.GetByAlbum("album1");

            Assert.AreEqual(3, gettedMusics.Count);
            CollectionAssert.Contains(gettedMusics.Select(g => g.Title).ToList(), expectedMusic1.Title);
            CollectionAssert.Contains(gettedMusics.Select(g => g.Title).ToList(), expectedMusic2.Title);
            CollectionAssert.Contains(gettedMusics.Select(g => g.Title).ToList(), expectedMusic4.Title);
        }

        [TestMethod]
        public void GetMusicByArtist()
        {
            const string artist = "artist1";
            Song expectedMusic1 = new Song { Artist = artist, Album = "fail1", MediaFileId = new Guid(), Title = "fail1" };
            Song expectedMusic2 = new Song { Artist = artist, Album = "fail2", MediaFileId = new Guid(), Title = "fIlTer" };
            Song expectedMusic3 = new Song { Artist = "artist2", Album = "fIlTer", MediaFileId = new Guid(), Title = "fail3" };
            Song expectedMusic4 = new Song { Artist = artist, Album = "fail4", MediaFileId = new Guid(), Title = "fail4" };

            Repository.Add(expectedMusic1);
            Repository.Add(expectedMusic2);
            Repository.Add(expectedMusic3);
            Repository.Add(expectedMusic4);
            IList<Song> gettedMusics = Repository.GetByArtist(artist);

            Assert.AreEqual(3, gettedMusics.Count);
            CollectionAssert.Contains(gettedMusics.Select(g => g.Title).ToList(), expectedMusic1.Title);
            CollectionAssert.Contains(gettedMusics.Select(g => g.Title).ToList(), expectedMusic2.Title);
            CollectionAssert.Contains(gettedMusics.Select(g => g.Title).ToList(), expectedMusic4.Title);
        }

        [TestMethod]
        public void GetAlbumByArtist()
        {
            const string artist = "artist1";
            Song expectedMusic1 = new Song { Artist = artist, Album = "album1", MediaFileId = new Guid(), Title = "fail1" };
            Song expectedMusic2 = new Song { Artist = artist, Album = "album1", MediaFileId = new Guid(), Title = "fIlTer" };
            Song expectedMusic3 = new Song { Artist = "artist2", Album = "fIlTer", MediaFileId = new Guid(), Title = "fail3" };
            Song expectedMusic4 = new Song { Artist = artist, Album = "album2", MediaFileId = new Guid(), Title = "fail4" };

            Repository.Add(expectedMusic1);
            Repository.Add(expectedMusic2);
            Repository.Add(expectedMusic3);
            Repository.Add(expectedMusic4);
            IList<string> gettedAlbums = Repository.GetAlbumsByArtist(artist);

            Assert.AreEqual(2, gettedAlbums.Count);
            CollectionAssert.Contains(gettedAlbums.ToList(), expectedMusic1.Album);
            CollectionAssert.Contains(gettedAlbums.ToList(), expectedMusic4.Album);
        }

    }
}
