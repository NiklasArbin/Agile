using Raven.Client;
using StyrBoard.Domain.Model;

namespace StyrBoard.Domain.Repository
{
    public class BoardRepository : BaseRepository<Board>
    {
        public BoardRepository(IDocumentStore store, IPriority priority)
            : base(store, priority, "Boards")
        { }
    }
}