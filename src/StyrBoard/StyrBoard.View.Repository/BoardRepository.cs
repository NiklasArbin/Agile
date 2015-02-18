using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Raven.Client.Linq;
using StyrBoard.Domain.Model;
using StyrBoard.Domain.Repository;
using StyrBoard.View.Repository.Mappings;
using Board = StyrBoard.Domain.Model.Board;
using Sprint = StyrBoard.Domain.Model.Sprint;

namespace StyrBoard.View.Repository
{
    public class BoardRepository : IBoardRepository
    {
        private readonly IDocumentStore _documentStore;
        private readonly IRepository<UserStory> _userStoryRepository;
        private readonly IRepository<Sprint> _sprintRepository;
        private readonly IRepository<Board> _boardRepository;
        private readonly IPriority _priority;

        public BoardRepository(IDocumentStore documentStore, IRepository<UserStory> userStoryRepository, IRepository<Sprint> sprintRepository, IRepository<Domain.Model.Board> boardRepository, IPriority priority)
        {
            _documentStore = documentStore;
            _userStoryRepository = userStoryRepository;
            _sprintRepository = sprintRepository;
            _boardRepository = boardRepository;
            _priority = priority;
        }

        public View.Model.Board Get()
        {
            
            List<UserStory> userStories;
            var board = _boardRepository.Get(new Guid("889B5F70-F556-4BEE-8652-803DC97A90E6"));
            using (var session = _documentStore.OpenSession())
            {
                userStories = session.Query<UserStory>().Where(x => x.Sprint.Id == board.SprintId).ToList();
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


            var sprint1 = new Sprint { Title = "Sprint 1", Description = "The first sprint" };
            var sprint2 = new Sprint { Title = "Sprint 2", Description = "The second sprint" };
            _sprintRepository.Save(sprint1);
            _sprintRepository.Save(sprint2);

            var board = new Board
            {
                Id = new Guid("889B5F70-F556-4BEE-8652-803DC97A90E6"),
                Title = "Current Sprint",
                SprintId = sprint1.Id
            };

            _boardRepository.Save(board);

            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Open" }, Title = "Test 1", Sprint = sprint1 });
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "In Progress" }, Title = "Test 2", Sprint = sprint1 });
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Testing" }, Title = "Test 3", Sprint = sprint1 });
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Completed" }, Title = "Test 4", Sprint = sprint1 });
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Closed" }, Title = "Test 5", Sprint = sprint1 });

            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Open" }, Title = "Backlog 1" });
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Open" }, Title = "Backlog 2" });
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Open" }, Title = "Backlog 3" });
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Open" }, Title = "Backlog 4" });
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Open" }, Title = "Backlog 5" });
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Open" }, Title = "Backlog 6" });
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Open" }, Title = "Backlog 7" });
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Open" }, Title = "Backlog 8" });
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Open" }, Title = "Backlog 9" });

        }


    }
}
