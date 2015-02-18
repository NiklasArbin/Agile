using System;
using System.Linq;
using Raven.Client;
using StyrBoard.Domain.Model;
using StyrBoard.View.Model;
using StyrBoard.View.Repository.Mappings;

namespace StyrBoard.View.Repository
{
    public class ListRepository : IListRepository
    {
        private readonly IDocumentStore _documentStore;
        private readonly IPriority _priority;

        public ListRepository(IDocumentStore documentStore, IPriority priority)
        {
            _documentStore = documentStore;
            _priority = priority;
        }

        public List Get()
        {
            var list = new List();
            using (var session = _documentStore.OpenSession())
            {
                var userStories = session.Query<UserStory>().Where(x=>x.Sprint == null || x.Sprint.Id == Guid.Empty).ToList();
                list.Cards.AddRange(userStories.Select(x => x.ToViewModel(_priority)).OrderBy(x => x.Priority));
            }
            return list;
        }
    }
}
