using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using StyrBoard.View.Model;
using StyrBoard.View.Repository;

namespace StyrBoard.Web.Controllers
{
    
    [HubName("MainHub")]
    public class HubController : Hub
    {
        private readonly IBoardRepository _boardRepository;

        public HubController(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        public void NotifyBoardUpdated()
        {
            Clients.AllExcept(new []{Context.ConnectionId}).BoardUpdated();
        }

        public void NotifyCardUpdated(int taskId)
        {
            //TODO
            //var task = _boardRepository.GetTask(taskId);
            //Clients.AllExcept(new[] { Context.ConnectionId }).CardUpdated(task);
        }

        public void NotifyCardDeleted(int taskId)
        {
            Clients.AllExcept(new[] { Context.ConnectionId }).CardDeleted(taskId);
        }
    }
}