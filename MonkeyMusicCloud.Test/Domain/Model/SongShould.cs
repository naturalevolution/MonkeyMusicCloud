using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Test.Helper;

namespace MonkeyMusicCloud.Test.Domain.Model
{
    [TestClass]
    public class SongShould
    {
        [TestMethod]
        public void InstantiateCorrectly()
        {
            File file = Create.File();
            const string title = "title";
            const string album = "album";
            const string artist = "artist";

            Song song = new Song(file, title, album, artist);

            Assert.AreEqual(file.Content.Length, song.File.Content.Length);
            Assert.AreEqual(title, song.Title);
            Assert.AreEqual(album, song.Album);
            Assert.AreEqual(artist, song.Artist);
        }
    }
}
