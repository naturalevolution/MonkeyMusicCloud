

using MonkeyMusicCloud.Domain.Model;

namespace MonkeyMusicCloud.Test.Helper
{
    public static class Create
    {
        private static int compteurSong;
        private static int compteurFile;

        public static File File()
        {
            compteurFile += 1;
            return new File(new byte[compteurFile]);
        }

        public static Song Song()
        {
            compteurSong += 1;
            return new Song(File(), "title" + compteurSong, "album"+compteurSong , "artist"+compteurSong);
        }
    }
}
