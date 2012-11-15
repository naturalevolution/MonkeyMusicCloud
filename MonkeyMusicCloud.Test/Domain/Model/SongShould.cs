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

            Song song = new Song(file);

            Assert.AreEqual(file.Content.Length, song.Length);
        }
    }
}
