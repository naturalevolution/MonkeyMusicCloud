using System;
using MonkeyMusicCloud.Domain.Model;

namespace MonkeyMusicCloud.Test.Helper
{
    public static class Create
    {
        private static int compteurSong;
        private static int compteurFile;

        public static MediaFile MediaFile()
        {
            compteurFile++;
            return new MediaFile(){Content = new byte[compteurFile], Id = Guid.NewGuid()};
        }

        public static Song Song(MediaFile file)
        {
            compteurSong += 1;
            return new Song { Album = "album" + compteurSong, Title = "title" + compteurSong, Artist = "artist" + compteurSong, MediaFileId = MediaFile().Id, Id = Guid.NewGuid() };
        }

        public static Song Song()
        {
            return Song(MediaFile());
        }

        public static MediaFile MediaFile(Guid id)
        {
            compteurFile++;
            return new MediaFile { Content = new byte[compteurFile], Id = id };
        }
    }
}
