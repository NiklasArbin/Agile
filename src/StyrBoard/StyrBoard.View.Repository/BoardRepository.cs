using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using StyrBoard.Domain.Model;
using StyrBoard.Domain.Repository;
using StyrBoard.View.Model;
using StyrBoard.View.Repository.Mappings;

namespace StyrBoard.View.Repository
{
    public class BoardRepository : IBoardRepository
    {
        private readonly IDocumentStore _documentStore;
        private readonly IRepository<UserStory> _userStoryRepository;
        private readonly IPriority _priority;

        public BoardRepository(IDocumentStore documentStore, IRepository<UserStory> userStoryRepository, IPriority priority)
        {
            _documentStore = documentStore;
            _userStoryRepository = userStoryRepository;
            _priority = priority;
        }

        public Board Get()
        {
            List<UserStory> userStories;

            using (var session = _documentStore.OpenSession())
            {
                userStories = session.Query<UserStory>().ToList();
            }

            if (!userStories.Any())
            {
                CreateDefaultData();
                using (var session = _documentStore.OpenSession())
                {
                    userStories = session.Query<UserStory>().ToList();
                }
            }

            return userStories.ToViewModel(_priority);
        }

        private void CreateDefaultData()
        {
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Open" }, Title = "Test1" });
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "In Progress" }, Title = "Test2" });
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Testing" }, Title = "Test3" });
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Completed" }, Title = "Test4" });
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Closed" }, Title = "Test5" });
        }


    }
}
