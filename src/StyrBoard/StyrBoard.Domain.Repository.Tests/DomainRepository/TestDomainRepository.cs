
using Raven.Client;

using StyrBoard.Domain.Repository;

namespace StyrBoard.Tests.DomainRepository
{
    public class TestDomainRepository : BaseRepository<TestDomainEntity>
    {
        public TestDomainRepository(IDocumentStore store) : base(store, "Tests")
        {
        }

        protected override void BeforeSave(TestDomainEntity item, IDocumentSession session)
        {
            // Do nothing here!
        }
    }
}
