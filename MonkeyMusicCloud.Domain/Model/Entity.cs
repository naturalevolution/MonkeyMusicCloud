using System;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace MonkeyMusicCloud.Domain.Model
{
    [DataContract]
    public class Entity
    {
        [DataMember]
        [BsonId]
        public virtual Guid Id { get; set; }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            var p = obj as Entity;
            if ((Object) p == null)
            {
                return false;
            }
            return (Id == p.Id);
        }

        public bool Equals(Entity p)
        {
            if ((object) p == null)
            {
                return false;
            }
            return (Id == p.Id) ;
        }



        public static bool operator ==(Entity a, Entity b)
        {
            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object) a == null) || ((object) b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.Id == b.Id;
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}