#region Usings

using MonkeyMusicCloud.Domain.Model;

#endregion

namespace MonkeyMusicCloud.Repository
{
    public static class Repositories
    {
        public static SongRepository Song
        {
            get { return new SongRepository(); }
        }

        public static Repository<MediaFile> MediaFile
        {
            get { return new Repository<MediaFile>(); }
        }
    }
}