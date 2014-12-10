using System.Net;
using System.Net.Http;
using System.Web.Http;
using Agile.View.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Agile.Web.Controllers
{
    public class BoardController : ApiController
    {
        private readonly IBoardRepository _boardRepository;

        public BoardController(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            var response = Request.CreateResponse();
            var columns = _boardRepository.GetColumns();
            response.Content = new StringContent(JsonConvert.SerializeObject(columns));
            response.StatusCode = HttpStatusCode.OK;

            return response;
        }

        [HttpGet]
        public HttpResponseMessage CanMove(int sourceColId, int targetColId)
        {
            var response = Request.CreateResponse();
            response.StatusCode = HttpStatusCode.OK;
            response.Content = new StringContent(JsonConvert.SerializeObject(new { canMove = false }));

            if (sourceColId == (targetColId - 1))
            {
                response.Content = new StringContent(JsonConvert.SerializeObject(new { canMove = true }));
            }

            return response;
        }

        [HttpPost]
        public HttpResponseMessage MoveTask(JObject moveTaskParams)
        {
            dynamic json = moveTaskParams;
            //var repo = new BoardRepository();
            //repo.MoveTask((int)json.taskId, (int)json.targetColId);

            var response = Request.CreateResponse();
            response.StatusCode = HttpStatusCode.OK;

            return response;
        }
    }
}
