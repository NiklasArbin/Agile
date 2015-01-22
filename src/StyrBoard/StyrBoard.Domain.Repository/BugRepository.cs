using Raven.Client;
using StyrBoard.Domain.Model;

namespace StyrBoard.Domain.Repository
{
    public class BugRepository : BaseRepository<Bug>
    {
        public BugRepository(IDocumentStore store)
            : base(store)
        { }
    }
}