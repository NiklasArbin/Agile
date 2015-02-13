
using System;
using Raven.Client;
using StyrBoard.Domain.Model;
using StyrBoard.Domain.Repository.Model;

namespace StyrBoard.Domain.Repository
{
    public abstract class BaseRepository<T> : IRepository<T> where T : IAggregateRoot
    {
        private readonly IDocumentStore _store;
        private readonly IPriority _priority;
        private string _tableName;

        protected BaseRepository(IDocumentStore store, IPriority priority, string tableName)
        {
            _store = store;
            _priority = priority;
            _tableName = tableName;
        }

        public T Get(Guid id)
        {
            using (var session = _store.OpenSession())
            {
                return session.Load<T>(_tableName + "/" + id);
            }
        }

        public T Get(int id)
        {
            Guid guid;

            using (var session = _store.OpenSession())
            {
                var queryString = typeof (GuidToIntMap).Name + "s/" + id;
                var map = session.Load<GuidToIntMap>(queryString);
                guid = map.GuidId;
            }

            return Get(guid);
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
                _priority.Add(item.Id);
                session.Store(_priority);
                session.Store(item);
                session.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            using (var session = _store.OpenSession())
            {
                session.Delete<T>(id);
                session.SaveChanges();
            }
        }

        protected virtual void BeforeSave(T item, IDocumentSession session)
        {}
    }
}
