namespace MonkeyMusicCloud.Repository
{
    public static class Repositories
    {
        public static SongRepository Song { get { return new SongRepository(); } }
    }
}
