using Raven.Client;
using StyrBoard.Domain.Model;

namespace StyrBoard.Domain.Repository
{
    public class EpicRepository : BaseRepository<Epic>
    {
        public EpicRepository(IDocumentStore store)
            : base(store, "Epics")
        { }
    }
}