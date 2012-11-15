using System.Runtime.Serialization;

namespace MonkeyMusicCloud.Domain.Model
{
    [DataContract]
    public class Media : Entity
    {
        protected Media(File file)
        {
            File = file;
        }

        [DataMember]
        public virtual File File { get; set; }
    }

    
}
