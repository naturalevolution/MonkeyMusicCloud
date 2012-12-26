using System.Configuration;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Resource;
using MonkeyMusicCloud.Test.Helper;
using Moq;

namespace MonkeyMusicCloud.Test.Client.ViewModels.SubViewModels
{
    [TestClass]
    public class DownloadTaskShould : ViewModelsBaseTest
    {
        [TestMethod]
        public void InstantiateCorrectly()
        {
            SongToAdd songToAdd = new SongToAdd();
            DownloadTask uploadTask = new DownloadTask(songToAdd.Song);

            Assert.AreEqual(songToAdd.Song, uploadTask.Song);
        }


        [TestMethod]
        public void CallServiceOnDoActionAndRaiseFinishedTaskEvent()
        {
            Song song = Create.Song();
            
            DownloadTask downloadTask = new DownloadTask(song);
            downloadTask.DoActionInNewThread();
            while (downloadTask.Worker.IsBusy) { }

            Assert.AreEqual(string.Format(MusicResource.DownloadSongTask, song.Title), downloadTask.StringDescription);
            MockService.Verify(s => s.GetMediaFileById(song.MediaFileId), Times.Once());
            string songPath = ConfigurationManager.AppSettings["LibraryRoot"] + song.Artist + "\\" + song.Album + "\\" + song.Title + ".mp3";
            Assert.IsTrue(File.Exists(songPath));
        }
    }
}
