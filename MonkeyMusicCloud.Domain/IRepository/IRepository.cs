using System.Collections.Generic;

namespace MonkeyMusicCloud.Domain.IRepository
{
    public interface IRepository<T>
    {
        IList<T> GetAll();
        void Add(T obj);
    }
}
