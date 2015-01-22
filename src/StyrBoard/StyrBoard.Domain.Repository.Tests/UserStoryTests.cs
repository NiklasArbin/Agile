using System;
using FluentAssertions;
using NUnit.Framework;
using Raven.Client.Embedded;
using StyrBoard.Domain.Model;

namespace StyrBoard.Domain.Repository.Tests
{
    [TestFixture]
    public class IdMappingTests
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
        public void Map_should_generate_integer_id_for_domainitem_when_new()
        {
            //arrange
            var repo = new UserStoryRepository(_documentStore);
            var userStory = new UserStory
            {
                Id = Guid.NewGuid()
            };
            //act
            repo.Save(userStory);
            //assert
            userStory.Should().NotBeNull();
            userStory.DisplayId.Should().BeGreaterThan(0);
        }
        [Test]
        public void Map_should_not_generate_new_integer_id_for_domainitem_when_existing()
        {
            //arrange
            var repo = new UserStoryRepository(_documentStore);
            var userStory = new UserStory
            {
                Id = Guid.NewGuid(),
                DisplayId = 123454
            };
            //act
            repo.Save(userStory);
            //assert
            userStory.Should().NotBeNull();
            userStory.DisplayId.Should().Be(123454);
        }
    }
}
