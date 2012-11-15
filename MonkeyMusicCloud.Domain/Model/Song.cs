using System.Runtime.Serialization;

namespace MonkeyMusicCloud.Domain.Model
{
    [DataContract]
    public class Song : Media
    {
        public Song(File file) : base(file){}

        public int Length { get { return File.Content.Length; } }
    }
}
