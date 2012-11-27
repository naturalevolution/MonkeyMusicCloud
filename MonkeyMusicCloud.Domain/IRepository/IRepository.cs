using System;
using System.Collections.Generic;
using MongoDB.Driver;

namespace MonkeyMusicCloud.Domain.IRepository
{
    public interface IRepository<T>
    {
        IList<T> GetAll();
        T Add(T obj);
        MongoCollection<T> Collection { get; }
    }
}
