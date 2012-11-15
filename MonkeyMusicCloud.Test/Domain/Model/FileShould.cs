using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Test.Helper;

namespace MonkeyMusicCloud.Test.Domain.Model
{
    [TestClass]
    public class FileShould
    {
        [TestMethod]
        public void InstantiateCorrectly()
        {
            byte[] content = new byte[5];
            File file = new File(content);
            Assert.AreEqual(content, file.Content);
        }

        [TestMethod]
        public void GetSetProperties()
        {
            File file = Create.File();
            byte[] content = new byte[5];

            file.Content = content;

            Assert.AreEqual(content, file.Content);
        }
    }
}
