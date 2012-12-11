using System.Threading;

namespace MonkeyMusicCloud.Client.ViewModels.SubViewModels
{
    public class UploadTask : Task
    {
        public SongToAdd SongToAdd { get; set; }

        public UploadTask(SongToAdd songToAdd)
        {
            SongToAdd = songToAdd;
        }

        protected override void DoAction()
        {
            Service.AddASong(SongToAdd.Song, SongToAdd.MediaFile);
        }
    }
}
