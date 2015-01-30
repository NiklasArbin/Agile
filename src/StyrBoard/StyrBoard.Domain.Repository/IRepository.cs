using System;
using StyrBoard.Domain.Model;

namespace StyrBoard.Domain.Repository
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        T Get(Guid id);
        T Get(int id);
        void Save(T item);
    }
}
