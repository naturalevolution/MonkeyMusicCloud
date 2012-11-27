using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MonkeyMusicCloud.Domain.IRepository;

namespace MonkeyMusicCloud.Repository
{
    public class Repository<T> : IRepository<T>
    {
        protected MongoDatabase Database{get { return MongoManager.GetInstance().Database; }}
        
        public virtual IList<T> GetAll()
        {
            MongoCursor<T> cursor = Collection.FindAllAs<T>();
            return cursor.ToList();
        }

        public virtual T Add(T obj)
        {
            using (MongoManager.GetInstance().Database.RequestStart())
            {
                Collection.Save(obj);
                return obj;
            }
        }

        public MongoCollection<T> Collection
        {
            get
            {
                if (!Database.CollectionExists(typeof(T).FullName))
                {
                    Database.CreateCollection(typeof(T).FullName);
                }
                return Database.GetCollection<T>(typeof(T).FullName);
            }
        }

        public virtual T GetById(Guid id)
        {
            return Collection.FindOneById(id);
        }
    }
}
