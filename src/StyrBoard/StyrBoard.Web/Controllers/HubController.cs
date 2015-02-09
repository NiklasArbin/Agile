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

        public void NotifyCardUpdated(int taskId)
        {
            var task = _cardRepository.Get(taskId);
            Clients.AllExcept(new[] { Context.ConnectionId }).CardUpdated(task);
        }

        public void NotifyCardDeleted(int taskId)
        {
            Clients.AllExcept(new[] { Context.ConnectionId }).CardDeleted(taskId);
        }

        public void NotifyCardAdded(string location)
        {
            Clients.All.CardAdded(location);
        }
    }
}