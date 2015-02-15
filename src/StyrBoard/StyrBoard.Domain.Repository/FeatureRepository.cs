using Raven.Client;
using StyrBoard.Domain.Model;

namespace StyrBoard.Domain.Repository
{
    public class FeatureRepository : BaseRepository<Feature>
    {
        public FeatureRepository(IDocumentStore store, IPriority priority)
            : base(store, priority, "Features")
        { }
    }
}