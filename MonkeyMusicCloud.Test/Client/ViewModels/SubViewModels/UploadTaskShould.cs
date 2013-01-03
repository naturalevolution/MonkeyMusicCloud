using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels.Tasks;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Resource;
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
            UploadTask uploadTask = new UploadTask(songToAdd.Song, songToAdd.MediaFile);

            Assert.AreEqual(songToAdd.Song, uploadTask.Song);
            Assert.AreEqual(songToAdd.MediaFile, uploadTask.MediaFile);
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

            UploadTask uploadTask = new UploadTask(songToAdd.Song, songToAdd.MediaFile);
            uploadTask.DoActionInNewThread();
            while (uploadTask.Worker.IsBusy) { }

            Assert.AreEqual(string.Format(MusicResource.UploadSongTask, songToAdd.Song.Title), uploadTask.StringDescription);
            MockService.Verify(s => s.AddASong(songToAdd.Song, songToAdd.MediaFile), Times.Once());
        }
    }
}
