using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using StyrBoard.Domain.Model;
using StyrBoard.Domain.Repository;
using StyrBoard.View.Model;
using StyrBoard.View.Repository.Mappings;
using Sprint = StyrBoard.Domain.Model.Sprint;

namespace StyrBoard.View.Repository
{
    public class BoardRepository : IBoardRepository
    {
        private readonly IDocumentStore _documentStore;
        private readonly IRepository<UserStory> _userStoryRepository;
        private readonly IRepository<Sprint> _sprintRepository;
        private readonly IPriority _priority;

        public BoardRepository(IDocumentStore documentStore, IRepository<UserStory> userStoryRepository, IRepository<Sprint> sprintRepository , IPriority priority)
        {
            _documentStore = documentStore;
            _userStoryRepository = userStoryRepository;
            _sprintRepository = sprintRepository;
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
            var sprint1 = new Sprint {Title = "Sprint 1", Description = "The first sprint"};
            var sprint2 = new Sprint {Title = "Sprint 2", Description = "The second sprint"};
            _sprintRepository.Save(sprint1);
            _sprintRepository.Save(sprint2);

            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Open" }, Title = "Test 1", Sprint = sprint1});
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "In Progress" }, Title = "Test 2", Sprint = sprint1 });
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Testing" }, Title = "Test 3", Sprint = sprint1 });
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Completed" }, Title = "Test 4", Sprint = sprint1 });
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Closed" }, Title = "Test 5", Sprint = sprint1 });

            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Open" }, Title = "Backlog 1"});
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Open" }, Title = "Backlog 2"});
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Open" }, Title = "Backlog 3"});
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Open" }, Title = "Backlog 4"});
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Open" }, Title = "Backlog 5"});
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Open" }, Title = "Backlog 6"});
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Open" }, Title = "Backlog 7"});
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Open" }, Title = "Backlog 8"});
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Open" }, Title = "Backlog 9"});

        }


    }
}
