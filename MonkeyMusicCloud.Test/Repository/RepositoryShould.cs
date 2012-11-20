using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Repository;
using MonkeyMusicCloud.Test.Helper;

namespace MonkeyMusicCloud.Test.Repository
{

    [TestClass]
    public class RepositoryShould : BaseRepositoryTests
    {
        readonly SongRepository songRepository = new SongRepository();
        readonly Repository<File> songFileRepository = new Repository<File>();

        [TestMethod]
        public void InsertAndGetAnObject()
        {
            
            File expectedFile = Create.File();

            songFileRepository.Add(expectedFile);
            File gettedFile = songFileRepository.GetAll().First();

            Assert.AreEqual(expectedFile.Content.Length, gettedFile.Content.Length);
        }

        [TestMethod]
        public void InsertAndGetComplexeObject()
        {
            Song expectedMusic = Create.Song();

            songRepository.Add(expectedMusic);
            Song gettedMusic = songRepository.GetAll().First();

            Assert.AreEqual(expectedMusic.File.Content.Length, gettedMusic.File.Content.Length);
        }

        [TestMethod]
        public void GetAllObject()
        {
            Song expectedMusic1 = Create.Song();
            Song expectedMusic2 = Create.Song();
            Song expectedMusic3 = Create.Song();

            songRepository.Add(expectedMusic1);
            songRepository.Add(expectedMusic2);
            songRepository.Add(expectedMusic3);
            IList<Song> gettedMusics = songRepository.GetAll();

            Assert.AreEqual(3, gettedMusics.Count);
            CollectionAssert.Contains(gettedMusics.Select(g => g.Title).ToList(), expectedMusic1.Title);
            CollectionAssert.Contains(gettedMusics.Select(g => g.Title).ToList(), expectedMusic2.Title);
            CollectionAssert.Contains(gettedMusics.Select(g => g.Title).ToList(), expectedMusic3.Title);
        }

       
    }
}

