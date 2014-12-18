using System;

namespace StyrBoard.Domain.Repository
{
    public interface IRepository<T>
    {
        T Get(Guid id);
        void Save(T item);
    }
}
