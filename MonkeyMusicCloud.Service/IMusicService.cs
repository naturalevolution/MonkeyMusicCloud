using System;
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
        IList<Song> SearchSongs(string filter);

        [OperationContract]
        void AddASong(Song song, MediaFile mediaFile);

        [OperationContract]
        IList<Song> GetByAlbum(string album);

        [OperationContract]
        MediaFile GetMediaFileById(Guid mediaFileId);

        [OperationContract]
        IList<string> GetAlbumsByArtist(string artist);

        [OperationContract]
        IList<Song> GetByArtist(string artist);
    }
}
