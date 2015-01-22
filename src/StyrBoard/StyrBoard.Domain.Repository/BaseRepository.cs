using System;
using System.Linq;
using Raven.Client;
using StyrBoard.Domain.Model;
using StyrBoard.Domain.Repository.Model;

namespace StyrBoard.Domain.Repository
{
    public abstract class BaseRepository<T> :IRepository<T> where T:IAggregateRoot
    {
        protected readonly IDocumentStore Store;

        protected BaseRepository(IDocumentStore store)
        {
            Store = store;
        }

        public T Get(Guid id)
        {
            using (var session = Store.OpenSession())
            {
                return session.Query<T>().Single(x => x.Id == id);
            }
        }

        public void Save(T item)
        {
            using (var session = Store.OpenSession())
            {
                if (item.DisplayId < 1)
                {
                    var map = new GuidToIntMap { GuidId = item.Id };
                    session.Store(map);
                    item.DisplayId = map.Id;
                }
                SaveInternal(item, session);
            }
        }

        protected abstract void SaveInternal(T item, IDocumentSession session);
    }
}