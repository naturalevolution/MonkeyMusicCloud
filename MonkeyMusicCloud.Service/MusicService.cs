using System;
using System.Collections.Generic;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Repository;

namespace MonkeyMusicCloud.Service
{
    public class MusicService : IMusicService
    {
        public SongRepository SongRepository { get; set; }
        public Repository<MediaFile> MediaFileRepository { get; set; }
        public MusicService() : this(Repositories.Song, Repositories.MediaFile) { }
        public MusicService(SongRepository songRepository, Repository<MediaFile> mediaFileRepository)
        {
            SongRepository = songRepository;
            MediaFileRepository = mediaFileRepository;
        }
        
        public IList<Song> GetAllSongs()
        {
            return SongRepository.GetAll();
        }

        public IList<Song> SearchSongs(string filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                return SongRepository.GetByFilter(filter);    
            }
            return new List<Song>();
        }

        public void AddASong(Song song, MediaFile mediaFile)
        {
            song.MediaFileId = MediaFileRepository.Add(mediaFile).Id;
            SongRepository.Add(song);
        }
        
        public IList<Song> GetByAlbum(string album)
        {
            return SongRepository.GetByAlbum(album);
        }

        public MediaFile GetMediaFileById(Guid mediaFileId)
        {
            return MediaFileRepository.GetById(mediaFileId);
        }
    }
}
