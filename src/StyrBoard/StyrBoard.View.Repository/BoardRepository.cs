using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using StyrBoard.Domain.Model;
using StyrBoard.Domain.Repository;
using StyrBoard.View.Model;
using StyrBoard.View.Repository.Mappings;
using Task = StyrBoard.Domain.Model.Task;

namespace StyrBoard.View.Repository
{
        int CreateTask(Task task);
    public class BoardRepository : IBoardRepository
    {
        private readonly IDocumentStore _documentStore;
        private readonly IRepository<UserStory> _userStoryRepository;

        public BoardRepository(IDocumentStore documentStore, IRepository<UserStory> userStoryRepository)
        {
            _documentStore = documentStore;
            _userStoryRepository = userStoryRepository;
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

            return userStories.ToViewModel();
        }

        private void CreateDefaultData()
        {
            var max = (from column in _columns from t in column.Tasks select t.Id).Concat(new[] {0}).Max();
            task.Id = max + 1;
            _columns[0].Tasks.Add(task);
            return task.Id;
        }

        {
            _userStoryRepository.Save(new UserStory() {State = new State() {Name = "Open"}, Title = "Test1"});
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "In Progress" }, Title = "Test2" });
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Testing" }, Title = "Test3" });
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Completed" }, Title = "Test4" });
            _userStoryRepository.Save(new UserStory() { State = new State() { Name = "Closed" }, Title = "Test5" });
        }
    }
}
