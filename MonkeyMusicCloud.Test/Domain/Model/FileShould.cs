using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Domain.Model;

namespace MonkeyMusicCloud.Test.Domain.Model
{
    [TestClass]
    public class FileShould
    {
        [TestMethod]
        public void InstantiateCorrectly()
        {
            byte[] content = new byte[5];
            Guid id = new Guid();
            MediaFile mediaFile = new MediaFile {Content = content, Id = id};
            Assert.AreEqual(content, mediaFile.Content);
            Assert.AreEqual(id, mediaFile.Id);
        }
    }
}