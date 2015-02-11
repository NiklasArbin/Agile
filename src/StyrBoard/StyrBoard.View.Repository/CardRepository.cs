using System;
using System.Linq;
using Raven.Client;
using StyrBoard.Domain.Model;
using StyrBoard.Domain.Repository;
using StyrBoard.View.Model;
using StyrBoard.View.Repository.Mappings;

namespace StyrBoard.View.Repository
{
    public interface ICardRepository
    {
        Card Get(int id);
        Card Get(Guid id);
    }

    public class CardRepository : ICardRepository
    {
        private readonly IDocumentStore _documentStore;
        private readonly IRepository<UserStory> _userStoryRepository;

        public CardRepository(IDocumentStore documentStore, IRepository<UserStory> userStoryRepository)
        {
            _documentStore = documentStore;
            _userStoryRepository = userStoryRepository;
        }

        public Card Get(int id)
        {
            using (var session = _documentStore.OpenSession())
            {
                var us = session.Query<UserStory>().Single(u => u.DisplayId == id);
                return us.ToViewModel();
            }
        }

        public Card Get(Guid id)
        {

            var us = _userStoryRepository.Get(id);
            return us.ToViewModel();

        }
    }
}