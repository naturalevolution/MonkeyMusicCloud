﻿using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MonkeyMusicCloud.Domain.Model;

namespace MonkeyMusicCloud.Repository
{
    public class SongRepository : Repository<Song>
    {
        public virtual IList<Song> GetByFilter(string filter, bool onTitle, bool onAlbum, bool onArtist)
        {
            IMongoQuery query = Query<Song>.Where(s => s.Title.ToUpper().Contains(filter.ToUpper()) ||
                                                       s.Album.ToUpper().Contains(filter.ToUpper()) ||
                                                       s.Artist.ToUpper().Contains(filter.ToUpper()));
            return Collection.FindAs<Song>(query).ToList();
        }

        public virtual IList<Song> GetByAlbum(string album)
        {
            IMongoQuery query = Query<Song>.Where(s => s.Album == album);
            return Collection.FindAs<Song>(query).ToList();
        }

        public virtual IList<Song> GetByArtist(string artist)
        {
            IMongoQuery query = Query<Song>.Where(s => s.Artist == artist);
            return Collection.FindAs<Song>(query).ToList();
        }

        public virtual IList<string> GetAlbumsByArtist(string artist)
        {
            return GetByArtist(artist).Select(s => s.Album).Distinct().ToList();
        }
    }
}