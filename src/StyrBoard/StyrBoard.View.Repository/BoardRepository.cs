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
            using (var session = _documentStore.OpenSession())
            {
                var userStories = session.Query<UserStory>().ToList();
                if (userStories.Any())
                    return userStories.ToViewModel();
            }

            return GetDummyData().ToViewModel();
        }

        private List<UserStory> GetDummyData()
        {
            var result = new List<UserStory>();

            result.Add(CreateDummyUserStory("Open", "Test1", 1));
            result.Add(CreateDummyUserStory("In Progress", "Test2", 2));
            result.Add(CreateDummyUserStory("Testing", "Test3", 3));
            result.Add(CreateDummyUserStory("Completed", "Test4", 4));
            result.Add(CreateDummyUserStory("Closed", "Test5", 5));

            return result;
        }

        private UserStory CreateDummyUserStory(string stateName, string title, int id)
        {
            return new UserStory()
            {
                State = new State() {Name = stateName},
                Title=title,
                DisplayId = id
            };
        }
    }
}
