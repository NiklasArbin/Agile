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
    public class TaskController : ApiController
    {
        private readonly IRepository<UserStory> _userStoryRepository;
        private readonly ICardRepository _cardRepository;

        public TaskController(IRepository<UserStory> userStoryRepository, ICardRepository cardRepository)
        {
            _userStoryRepository = userStoryRepository;
            _cardRepository = cardRepository;
        }

        // GET: api/Task/5
        //public HttpResponseMessage Get(Guid id)
        //{
        //    var response = Request.CreateResponse();
        //    var card = _cardRepository.Get(id);
        //    response.Content = new StringContent(JsonConvert.SerializeObject(card));
        //    response.StatusCode = HttpStatusCode.OK;
        //    return response;
        //}

        // POST: api/Task
        public HttpResponseMessage Post(JObject value)
        {
            var newTask = JsonConvert.DeserializeObject<View.Model.Task>(value.ToString());
            var response = Request.CreateResponse();

            var story = _userStoryRepository.Get(newTask.UserStoryId);
            story.AddTask(new Task { Title = newTask.Name });

            _userStoryRepository.Save(story);

            response.StatusCode = HttpStatusCode.Created;
            response.Headers.Location = new Uri(string.Format("{0}/{1}", Request.RequestUri, story.Id));
            return response;
        }
    }
}
