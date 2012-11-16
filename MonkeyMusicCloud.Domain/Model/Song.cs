using System.Runtime.Serialization;

namespace MonkeyMusicCloud.Domain.Model
{
    [DataContract]
    public class Song : Media
    {
        public Song(File file, string title, string album, string artist) : base(file)
        {
            Title = title;
            Album = album;
            Artist = artist;
        }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Album { get; set; }

        [DataMember]
        public string Artist { get; set; }
    }
}
