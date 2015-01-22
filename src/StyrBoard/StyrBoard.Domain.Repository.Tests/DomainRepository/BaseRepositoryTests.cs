
// ReSharper disable InconsistentNaming

using System;

using FluentAssertions;

using NUnit.Framework;
using Raven.Client.Embedded;

namespace StyrBoard.Tests.DomainRepository
{
    [TestFixture]
    public class BaseRepositoryTests
    {
        private EmbeddableDocumentStore _documentStore;

        [TestFixtureSetUp]
        public void Setup()
        {
            _documentStore = new EmbeddableDocumentStore
           {
               DataDirectory = "TestData",
               UseEmbeddedHttpServer = false
           };
            _documentStore.Initialize();
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            _documentStore.Dispose();
        }

        [Test]
        public void Save_should_generate_integer_id_for_domainitem_when_new()
        {
            var repo = new TestDomainRepository(_documentStore);
            var userStory = new TestDomainEntity
            {
                Id = Guid.NewGuid()
            };
            
            repo.Save(userStory);
            
            userStory.Should().NotBeNull();
            userStory.DisplayId.Should().BeGreaterThan(0);
        }

        [Test]
        public void Save_should_not_generate_new_integer_id_for_domainitem_when_existing()
        {
            var repo = new TestDomainRepository(_documentStore);
            var entity = new TestDomainEntity
            {
                Id = Guid.NewGuid(),
                DisplayId = 123454
            };
            
            repo.Save(entity);
            
            entity.Should().NotBeNull();
            entity.DisplayId.Should().Be(123454);
        }
    }
}
