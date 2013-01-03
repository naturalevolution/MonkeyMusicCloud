using System.Configuration;
using System.IO;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Resource;

namespace MonkeyMusicCloud.Client.ViewModels.SubViewModels.Tasks
{
    public class DownloadTask : Task
    {
        public DownloadTask(Song song)
        {
            Song = song;
        }

        public override string StringDescription
        {
            get { return string.Format(MusicResource.DownloadSongTask, Song.Title); }
        }

        public Song Song { get; set; }

        protected override void DoAction()
        {
            string rootPath = ConfigurationManager.AppSettings["LibraryRoot"];
            MediaFile file = Service.GetMediaFileById(Song.MediaFileId);
            CreateFile(rootPath, file);
        }

        private void CreateFile(string rootPath, MediaFile file)
        {
            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }
            string artistPath = rootPath + Song.Artist + "\\";
            if (!Directory.Exists(artistPath))
            {
                Directory.CreateDirectory(artistPath);
            }
            string albumPath = artistPath + Song.Album + "\\";
            if (!Directory.Exists(albumPath))
            {
                Directory.CreateDirectory(albumPath);
            }
            string fullPath = albumPath + Song.Title + ".mp3";

            File.WriteAllBytes(fullPath, file.Content);
        }
    }
}