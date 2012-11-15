using System.Collections.Generic;
using System.ServiceModel;
using MonkeyMusicCloud.Domain.Model;

namespace MonkeyMusicCloud.Service
{
    [ServiceContract]
    public interface IMusicService
    {
        [OperationContract]
        IList<Song> GetAllSongs();

        [OperationContract]
        void AddASong(Song song);
    }
}
