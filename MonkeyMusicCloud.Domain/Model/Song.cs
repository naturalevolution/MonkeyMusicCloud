using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace MonkeyMusicCloud.Domain.Model
{
    [DataContract]
    public class Song : Media
    {
        [DataMember]
        [BsonRequired]
        public string Title { get; set; }

        [DataMember]
        [BsonRequired]
        [BsonDefaultValue("UnknownAlbum")]
        public string Album { get; set; }

        [DataMember]
        [BsonRequired]
        [BsonDefaultValue("UnknownArtist")]
        public string Artist { get; set; }
    }
}
