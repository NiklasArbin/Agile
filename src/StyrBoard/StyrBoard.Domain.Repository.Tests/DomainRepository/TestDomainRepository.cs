
using Raven.Client;
using StyrBoard.Domain.Model;
using StyrBoard.Domain.Repository;

namespace StyrBoard.Tests.DomainRepository
{
    public class TestDomainRepository : BaseRepository<TestDomainEntity>
    {
        public TestDomainRepository(IDocumentStore store, IPriority priority)
            : base(store, priority, "Tests")
        {
        }

        protected override void BeforeSave(TestDomainEntity item, IDocumentSession session)
        {
            // Do nothing here!
        }
    }
}
