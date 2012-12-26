using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.ViewModels.BodyViewModels;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Test.Helper;

namespace MonkeyMusicCloud.Test.Client.ViewModels.BodyViewModels
{
    [TestClass]
    public class LibraryViewModelShould : ViewModelsBaseTest
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
            string path = ConfigurationManager.AppSettings["LibraryRoot"];
            Directory.Delete(path,true);
            Directory.CreateDirectory(path);
            string artist1Path = path + "artist1\\";
            Directory.CreateDirectory(artist1Path);
            string artist2Path = path + "artist2\\";
            Directory.CreateDirectory(artist2Path);
            Directory.CreateDirectory(artist1Path + "album1");
            Directory.CreateDirectory(artist1Path + "album2"); 

        }

        [TestMethod]
        public void InstantiateOrgItemsCorrectly()
        {
            LibraryViewModel viewModel = new LibraryViewModel();

            Assert.AreEqual(2, viewModel.Folders.Count);
            Assert.AreEqual("artist1", viewModel.Folders[0].Data.Name);
            Assert.AreEqual(2, viewModel.Folders[0].Children.Count);
            Assert.AreEqual("album1", viewModel.Folders[0].Children[0].Data.Name);
            Assert.AreEqual("album2", viewModel.Folders[0].Children[1].Data.Name);
            Assert.AreEqual("artist2", viewModel.Folders[1].Data.Name);
        }
    }
}
