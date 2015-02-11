using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StyrBoard.Domain.Model;
using StyrBoard.Domain.Repository;
using StyrBoard.View.Repository;
using Task = StyrBoard.View.Model.Task;

namespace StyrBoard.Web.Controllers
{
    public class TaskController : ApiController
    {
        private readonly IRepository<UserStory> _userStoryRepository;
        private readonly ICardRepository _cardRepository;

        public TaskController(IRepository<UserStory> userStoryRepository, ICardRepository cardRepository)
        {
            _userStoryRepository = userStoryRepository;
            _cardRepository = cardRepository;
        }

        // GET: api/Task
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Task/5
        public HttpResponseMessage Get(Guid id)
        {
            var response = Request.CreateResponse();
            var card = _cardRepository.Get(id);
            response.Content = new StringContent(JsonConvert.SerializeObject(card));
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }

        // POST: api/Task
        public HttpResponseMessage Post(JObject value)
        {
            var task = JsonConvert.DeserializeObject<Task>(value.ToString());
            var response = Request.CreateResponse();

            var story = new UserStory
            {
                Description = task.Description,
                Title = task.Name,
                State = new State { Name = "Open", Id = 1},
            };
            _userStoryRepository.Save(story);
            
            response.StatusCode = HttpStatusCode.Created;
            response.Headers.Location = new Uri(string.Format("{0}/{1}", Request.RequestUri, story.Id));
            return response;
        }

        // PUT: api/Task/5
        public void Put(Guid id, JObject value)
        {
            var task = JsonConvert.DeserializeObject<Task>(value.ToString());
            var userStory = _userStoryRepository.Get(id);
            userStory.State = State.GetDefaultStates().Single(x => x.Id == task.ColumnId);
            userStory.Description = task.Description;
            userStory.Title = task.Name;
            _userStoryRepository.Save(userStory);
        }

        // DELETE: api/Task/5
        public void Delete(Guid id)
        {
            _userStoryRepository.Delete(id);
        }
    }
}
