using Raven.Client;

using StyrBoard.Domain.Model;

namespace StyrBoard.Domain.Repository
{
    public class UserStoryRepository : BaseRepository<UserStory>
    {
        public UserStoryRepository(IDocumentStore store) : base(store, "UserStories")
        {}
    }
}
