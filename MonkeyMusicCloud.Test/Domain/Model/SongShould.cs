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
            MediaFile mediaFile = Create.MediaFile();
            const string title = "title";
            const string album = "album";
            const string artist = "artist";

            Song song = new Song {Title = title, Album = album, Artist = artist, MediaFileId = mediaFile.Id};

            Assert.AreEqual(mediaFile.Id, song.MediaFileId);
            Assert.AreEqual(title, song.Title);
            Assert.AreEqual(album, song.Album);
            Assert.AreEqual(artist, song.Artist);
        }
    }
}
