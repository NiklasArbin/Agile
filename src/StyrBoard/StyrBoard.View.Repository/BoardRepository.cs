using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using StyrBoard.Domain.Model;
using StyrBoard.View.Model;
using StyrBoard.View.Repository.Mappings;
using Task = StyrBoard.Domain.Model.Task;

namespace StyrBoard.View.Repository
{
    public class BoardRepository : IBoardRepository
    {
        private readonly IDocumentStore _documentStore;

        public BoardRepository(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public Board Get()
        {
            List<UserStory> userStories;

            using (var session = _documentStore.OpenSession())
            {
                userStories = session.Query<UserStory>().ToList();
                if (!userStories.Any())
                    userStories = GetDummyData();
            }

            return userStories.ToViewModel();
        }

        private List<UserStory> GetDummyData()
        {
            var result = new List<UserStory>();

            result.Add(new UserStory() {State = new State() {Name = "Open"}});
            result.Add(new UserStory() { State = new State() { Name = "In Progress" } });
            result.Add(new UserStory() { State = new State() { Name = "Testing" } });
            result.Add(new UserStory() { State = new State() { Name = "Completed" } });
            result.Add(new UserStory() { State = new State() { Name = "Closed" } });

            return result;
        }
    }
}
