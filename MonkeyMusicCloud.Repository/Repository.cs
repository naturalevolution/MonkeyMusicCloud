using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MonkeyMusicCloud.Domain.IRepository;

namespace MonkeyMusicCloud.Repository
{
    public class Repository<T> : IRepository<T>
    {
        private MongoDatabase Database{ get { return MongoManager.GetInstance().Database; }}
        MongoCollection<T> Collection { get { return Database.GetCollection<T>("musics"); } }

        public virtual IList<T> GetAll()
        {
            MongoCursor<T> cursor = Collection.FindAllAs<T>();
            return cursor.ToList();
        }

        public void Add(T obj)
        {
            using (MongoManager.GetInstance().Database.RequestStart())
            {
                Collection.Save(obj);
            }
        }
    }
}
