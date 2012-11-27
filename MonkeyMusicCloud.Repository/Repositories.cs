using MonkeyMusicCloud.Domain.Model;

namespace MonkeyMusicCloud.Repository
{
    public static class Repositories
    {
        public static SongRepository Song { get { return new SongRepository(); } }
        public static Repository<MediaFile> MediaFile { get { return new Repository<MediaFile>(); } }
    }
}
