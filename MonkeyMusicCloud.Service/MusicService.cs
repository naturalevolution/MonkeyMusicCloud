using System.Collections.Generic;
using MonkeyMusicCloud.Domain.IRepository;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Repository;

namespace MonkeyMusicCloud.Service
{
    public class MusicService : IMusicService
    {
        public IRepository<Song> Repository { get; set; }
        public MusicService() : this(Repositories.Music) { }
        public MusicService(IRepository<Song> repository)
        {
            Repository = repository;
        }
        
        public IList<Song> GetAllSongs()
        {
            return Repository.GetAll();
        }

        public void AddASong(Song song)
        {
            Repository.Add(song);
        }
    }
}
