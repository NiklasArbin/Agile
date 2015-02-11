using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using StyrBoard.View.Model;
using StyrBoard.View.Repository;

namespace StyrBoard.Web.Controllers
{
    
    [HubName("MainHub")]
    public class HubController : Hub
    {
        private readonly ICardRepository _cardRepository;


        public HubController(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public void NotifyBoardUpdated()
        {
            Clients.AllExcept(new []{Context.ConnectionId}).BoardUpdated();
        }

        public void NotifyCardUpdated(Guid id)
        {
            var task = _cardRepository.Get(id);
            Clients.AllExcept(new[] { Context.ConnectionId }).CardUpdated(task);
        }

        public void NotifyCardDeleted(Guid id)
        {
            Clients.AllExcept(new[] { Context.ConnectionId }).CardDeleted(id);
        }

        public void NotifyCardAdded(string location)
        {
            Clients.All.CardAdded(location);
        }
    }
}