using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Test.Helper;
using Moq;

namespace MonkeyMusicCloud.Test.Client.ViewModels.SubViewModels
{
    [TestClass]
    public class UploadTaskShould : ViewModelsBaseTest
    {
        [TestMethod]
        public void InstantiateCorrectly()
        {
            SongToAdd songToAdd = new SongToAdd();
            UploadTask uploadTask = new UploadTask(songToAdd);

            Assert.AreEqual(songToAdd, uploadTask.SongToAdd);
        }


        [TestMethod]
        public void CallServiceOnDoActionAndRaiseFinishedTaskEvent()
        {
            MediaFile mediaFile = Create.MediaFile();
            Song song = Create.Song();
            const string path = "path";
            SongToAdd songToAdd = new SongToAdd()
                {
                    IsSelected = true,
                    MediaFile = mediaFile,
                    Path = path,
                    Song = song
                };

            UploadTask task = new UploadTask(songToAdd);
            task.DoActionInNewThread();
            while (task.Worker.IsBusy){}

            Service.Verify(s => s.AddASong(songToAdd.Song, songToAdd.MediaFile), Times.Once());
        }
    }
}
