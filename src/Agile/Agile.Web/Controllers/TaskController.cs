using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Agile.View.Repository;
using Newtonsoft.Json;

namespace Agile.Web.Controllers
{
    public class TaskController : ApiController
    {
        private readonly IBoardRepository _boardRepository;

        public TaskController(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        // GET: api/Task
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Task/5
        public HttpResponseMessage Get(int id)
        {
            var response = Request.CreateResponse();
            var task = _boardRepository.GetTask(id);
            response.Content = new StringContent(JsonConvert.SerializeObject(task));
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }

        // POST: api/Task
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Task/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Task/5
        public void Delete(int id)
        {
        }
    }
}
