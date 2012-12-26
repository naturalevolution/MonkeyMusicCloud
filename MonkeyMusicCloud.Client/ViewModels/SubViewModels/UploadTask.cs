using System.Threading;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Resource;

namespace MonkeyMusicCloud.Client.ViewModels.SubViewModels
{
    public class UploadTask : Task
    {
        public Song Song { get; set; }
        public MediaFile MediaFile { get; set; }

        public UploadTask(Song song, MediaFile mediaFile)
        {
            Song = song;
            MediaFile = mediaFile;
        }

        protected override void DoAction()
        {
            Thread.Sleep(1000);
            Service.AddASong(Song, MediaFile);
        }

        public override string StringDescription
        {
            get { return string.Format(MusicResource.UploadSongTask, Song.Title); }
        }
    }
}
