using System;
using System.Collections.Generic;
using MonkeyMusicCloud.Domain.IRepository;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Repository;

namespace MonkeyMusicCloud.Service
{
    public class MusicService : IMusicService
    {
        public SongRepository Repository { get; set; }
        public MusicService() : this(Repositories.Song) { }
        public MusicService(SongRepository repository)
        {
            Repository = repository;
        }
        
        public IList<Song> GetAllSongs()
        {
            return Repository.GetAll();
        }

        public IList<Song> SearchSongs(string filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                return Repository.GetByFilter(filter);    
            }
            return new List<Song>();
        }

        public void AddASong(Song song)
        {
            Repository.Add(song);
        }
    }
}
