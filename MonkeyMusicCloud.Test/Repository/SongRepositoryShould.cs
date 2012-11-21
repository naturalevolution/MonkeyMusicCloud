using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Repository;
using MonkeyMusicCloud.Test.Helper;

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
            Song expectedMusic1 = new Song(Create.File(), "fIlTer", "fail1", "fail1");
            Song expectedMusic2 = new Song(Create.File(), "fail2", "fail2", "fIlTer");
            Song expectedMusic3 = new Song(Create.File(), "fail3", "fIlTer", "fail3");
            Song expectedMusic4 = new Song(Create.File(), "fail4", "fail4", "fail4");

            Repository.Add(expectedMusic1);
            Repository.Add(expectedMusic2);
            Repository.Add(expectedMusic3);
            Repository.Add(expectedMusic4);
            IList<Song> gettedMusics = Repository.GetByFilter("ilt");

            Assert.AreEqual(3, gettedMusics.Count);
            CollectionAssert.Contains(gettedMusics.Select(g => g.Title).ToList(), expectedMusic1.Title);
            CollectionAssert.Contains(gettedMusics.Select(g => g.Title).ToList(), expectedMusic2.Title);
            CollectionAssert.Contains(gettedMusics.Select(g => g.Title).ToList(), expectedMusic3.Title);
            CollectionAssert.DoesNotContain(gettedMusics.Select(g => g.Title).ToList(), expectedMusic4.Title);
        }

        [TestMethod]
        public void GetMusicByAlbum()
        {
            Song expectedMusic1 = new Song(Create.File(), "title1", "album1", "artist");
            Song expectedMusic2 = new Song(Create.File(), "title2", "album1", "artist");
            Song expectedMusic3 = new Song(Create.File(), "title3", "album2", "artist");
            Song expectedMusic4 = new Song(Create.File(), "title4", "album1", "artist");

            Repository.Add(expectedMusic1);
            Repository.Add(expectedMusic2);
            Repository.Add(expectedMusic3);
            Repository.Add(expectedMusic4);
            IList<Song> gettedMusics = Repository.GetByAlbum("album1");

            Assert.AreEqual(3, gettedMusics.Count);
            CollectionAssert.Contains(gettedMusics.Select(g => g.Title).ToList(), expectedMusic1.Title);
            CollectionAssert.Contains(gettedMusics.Select(g => g.Title).ToList(), expectedMusic2.Title);
            CollectionAssert.DoesNotContain(gettedMusics.Select(g => g.Title).ToList(), expectedMusic3.Title);
            CollectionAssert.Contains(gettedMusics.Select(g => g.Title).ToList(), expectedMusic4.Title);
        }

    }
}
