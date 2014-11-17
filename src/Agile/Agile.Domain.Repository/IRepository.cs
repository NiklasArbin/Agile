using System;

namespace Agile.Domain.Repository
{
    public interface IRepository<T>
    {
        T Get(Guid id);
        void Save(T item);
    }
}
