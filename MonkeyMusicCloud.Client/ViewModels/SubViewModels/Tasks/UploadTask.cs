using System.Threading;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Resource;

namespace MonkeyMusicCloud.Client.ViewModels.SubViewModels.Tasks
{
    public class UploadTask : Tasks.Task
    {
        public UploadTask(Song song, MediaFile mediaFile)
        {
            Song = song;
            MediaFile = mediaFile;
        }

        public Song Song { get; set; }
        public MediaFile MediaFile { get; set; }

        public override string StringDescription
        {
            get { return string.Format(MusicResource.UploadSongTask, Song.Title); }
        }

        protected override void DoAction()
        {
            Thread.Sleep(1000);
            Service.AddASong(Song, MediaFile);
        }
    }
}