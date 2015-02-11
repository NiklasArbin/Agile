using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StyrBoard.Domain.Model;
using StyrBoard.Domain.Repository;
using StyrBoard.View.Repository;

namespace StyrBoard.Web.Controllers
{
    public class BoardController : ApiController
    {
        private readonly IRepository<UserStory> _userStoryRepository;
        private readonly IBoardRepository _boardRepository;

        public BoardController(IBoardRepository boardRepository, IRepository<UserStory> userStoryRepository)
        {
            _userStoryRepository = userStoryRepository;
            _boardRepository = boardRepository;
        }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            var response = Request.CreateResponse();
            var board = _boardRepository.Get();
            response.Content = new StringContent(JsonConvert.SerializeObject(board.Columns));
            response.StatusCode = HttpStatusCode.OK;

            return response;
        }

        [HttpGet]
        public HttpResponseMessage CanMove(int sourceColId, int targetColId)
        {
            var response = Request.CreateResponse();
            response.StatusCode = HttpStatusCode.OK;
            response.Content = new StringContent(JsonConvert.SerializeObject(new { canMove = true }));
            return response;
        }

        [HttpPost]
        public HttpResponseMessage MoveTask(JObject moveTaskParams)
        {
            dynamic json = moveTaskParams;
            var id = (Guid)(json.taskId);
            var userStory = _userStoryRepository.Get(id);

            var newState = State.GetDefaultStates().Single(s => s.Id == (int) json.targetColId);
            userStory.MoveTo(newState);

            _userStoryRepository.Save(userStory);

            var response = Request.CreateResponse();
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
    }
}
