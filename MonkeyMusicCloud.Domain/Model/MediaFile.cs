using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace MonkeyMusicCloud.Domain.Model
{
    [DataContract]
    public class MediaFile : Entity
    {
        [DataMember]
        [BsonRequired]
        public virtual byte[] Content { get; set; }
    }
}
