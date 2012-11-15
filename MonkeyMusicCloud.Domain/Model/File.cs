using System.Runtime.Serialization;

namespace MonkeyMusicCloud.Domain.Model
{
    [DataContract]
    public class File : Entity
    {
        public File(){}

        public File(byte[] file)
        {
            Content = file;
        }

        [DataMember]
        public virtual byte[] Content { get; set; }
    }
}
