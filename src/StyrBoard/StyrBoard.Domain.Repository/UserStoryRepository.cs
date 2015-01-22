
using System;
using System.Linq;
using Raven.Client;

using StyrBoard.Domain.Model;
using StyrBoard.Domain.Repository.Model;

namespace StyrBoard.Domain.Repository
{
    public class UserStoryRepository : IRepository<UserStory>
    {
        private readonly IDocumentStore _store;

        public UserStoryRepository(IDocumentStore store)
        {
            _store = store;
        }

        public UserStory Get(Guid id)
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<UserStory>().Single(x => x.Id == id);
            }
        }

        public void Save(UserStory item)
        {
            using (var session = _store.OpenSession())
            {
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
    }
}
