using StyrBoard.Domain.Model;

namespace StyrBoard.Domain.Repository
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        T Get(int id);
        void Save(T item);
        void Delete(T item);
    }
}
