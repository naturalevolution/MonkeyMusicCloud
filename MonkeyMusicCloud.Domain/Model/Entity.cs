using System;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace MonkeyMusicCloud.Domain.Model
{
    [DataContract]
    public class Entity
    {
        [DataMember]
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public virtual Guid Id { get; set; }

        public override bool Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Media)) return false;
            return Equals((Media)obj);
        }

        public bool Equals(Media p)
        {
            if (ReferenceEquals(null, p)) return false;
            if (ReferenceEquals(this, p)) return true;
            return Equals(p.Id, Id);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }
    }
}
