using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Agile.View.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Agile.Web.Controllers
{
    public class BoardController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var response = Request.CreateResponse();
            var columns = new List<Column>
            {
                new Column
                {
                    Id = 1, Description = "Desc", Name = "Open",
                    Tasks = new List<Task>
                    {
                        new Task{ColumnId = 1, Description = "TD", Id = 1, Name = "Task 1"}
                    }
                
                },
                new Column
                {
                    Id = 2, Description = "Desc", Name = "In Progress",
                    Tasks = new List<Task>
                    {
                        new Task{ColumnId = 2, Description = "TD", Id = 2, Name = "Task 1"}
                    }
                
                },
                new Column
                {
                    Id = 3, Description = "Desc", Name = "Testing",
                    Tasks = new List<Task>
                    {
                        new Task{ColumnId = 2, Description = "TD", Id = 2, Name = "Task 1"}
                    }
                
                },
                new Column
                {
                    Id = 4, Description = "Desc", Name = "Done",
                    Tasks = new List<Task>
                    {
                        new Task{ColumnId = 2, Description = "TD", Id = 2, Name = "Task 1"}
                    }
                },
                new Column
                {
                    Id = 4, Description = "Desc", Name = "Done",
                    Tasks = new List<Task>
                    {
                        new Task{ColumnId = 2, Description = "TD", Id = 2, Name = "Task 1"}
                    }
                }
            };
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
