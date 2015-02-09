using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StyrBoard.View.Model;
using StyrBoard.View.Repository;

namespace StyrBoard.Web.Controllers
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
        public HttpResponseMessage Post(JObject value)
        {
            var task = JsonConvert.DeserializeObject<Task>(value.ToString());
            var response = Request.CreateResponse();
            response.StatusCode = HttpStatusCode.Created;
            var id = _boardRepository.CreateTask(task);
            response.Headers.Location = new Uri(string.Format("{0}/{1}", Request.RequestUri, id));
            return response;
        }

        // PUT: api/Task/5
        public void Put(int id, JObject value)
        {
            var task = JsonConvert.DeserializeObject<Task>(value.ToString());
            var taskToUpdate = _boardRepository.GetTask(id);
            taskToUpdate.ColumnId = task.ColumnId;
            taskToUpdate.Description = task.Description;
            taskToUpdate.Name = task.Name;
        }

        // DELETE: api/Task/5
        public void Delete(int id)
        {
            _boardRepository.DeleteTask(id);
        }
    }
}
