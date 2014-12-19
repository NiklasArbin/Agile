using System;
using Raven.Client;
using StyrBoard.Domain.Model;

namespace StyrBoard.Domain.Repository
{
    public class UserStoryRepository:IRepository<UserStory>
    {
        private readonly IDocumentStore _store;

        public UserStoryRepository(IDocumentStore store)
        {
            _store = store;
        }

        public UserStory Get(int id)
        {
            using (var session = _store.OpenSession())
            {
                return session.Load<UserStory>(id);
            }
        }

        public void Save(UserStory item)
        {
            using (var session = _store.OpenSession())
            {
                session.Store(item);
                session.SaveChanges();
            }
        }

        public void Delete(UserStory item)
        {
            throw new NotImplementedException();
        }
    }
}