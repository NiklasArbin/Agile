using Raven.Client;

using StyrBoard.Domain.Model;

namespace StyrBoard.Domain.Repository
{
    public class UserStoryRepository : BaseRepository<UserStory>
    {
        public UserStoryRepository(IDocumentStore store, IPriority priority)
            : base(store,priority, "UserStories")
        {}
    }
}
