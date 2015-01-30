using Raven.Client;
using StyrBoard.Domain.Model;

namespace StyrBoard.Domain.Repository
{
    public class SprintRepository : BaseRepository<Sprint>
    {
        public SprintRepository(IDocumentStore store)
            : base(store, "Sprints")
        { }
    }
}