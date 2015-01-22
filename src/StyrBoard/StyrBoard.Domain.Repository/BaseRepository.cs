
using System;
using System.Linq;

using Raven.Client;

using StyrBoard.Domain.Model;
using StyrBoard.Domain.Repository.Model;

namespace StyrBoard.Domain.Repository
{
    public abstract class BaseRepository<T> : IRepository<T> where T : IAggregateRoot
    {
        private readonly IDocumentStore _store;

        protected BaseRepository(IDocumentStore store)
        {
            _store = store;
        }

        public T Get(Guid id)
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<T>().Single(x => x.Id == id);
            }
        }

        public void Save(T item)
        {
            using (var session = _store.OpenSession())
            {
                BeforeSave(item, session);

                if (item.DisplayId < 1)
                {
                    var map = new GuidToIntMap { GuidId = item.Id };
                    session.Store(map);
                    item.DisplayId = map.Id;
                }
                session.Store(item);
                session.SaveChanges();
            }
        }

        protected virtual void BeforeSave(T item, IDocumentSession session)
        {}
    }
}
