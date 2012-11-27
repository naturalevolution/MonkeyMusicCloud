using System;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace MonkeyMusicCloud.Domain.Model
{
    [DataContract]
    public class Media : Entity
    {
        protected Media(){}

        [DataMember]
        [BsonRequired]
        public virtual Guid MediaFileId { get; set; }
    }

    
}
