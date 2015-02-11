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
    public class UserStoryController : ApiController
    {
        private readonly IRepository<UserStory> _userStoryRepository;
        private readonly ICardRepository _cardRepository;

        public UserStoryController(IRepository<UserStory> userStoryRepository, ICardRepository cardRepository)
        {
            _userStoryRepository = userStoryRepository;
            _cardRepository = cardRepository;
        }

        // GET: api/UserStory/5
        public HttpResponseMessage Get(Guid id)
        {
            var response = Request.CreateResponse();
            var card = _cardRepository.Get(id);
            response.Content = new StringContent(JsonConvert.SerializeObject(card));
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }

        // POST: api/UserStory
        public HttpResponseMessage Post(JObject value)
        {
            var task = JsonConvert.DeserializeObject<View.Model.Card>(value.ToString());
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

        // PUT: api/UserStory/5
        public void Put(Guid id, JObject value)
        {
            var task = JsonConvert.DeserializeObject<View.Model.Card>(value.ToString());
            var userStory = _userStoryRepository.Get(id);
            userStory.State = State.GetDefaultStates().Single(x => x.Id == task.ColumnId);
            userStory.Description = task.Description;
            userStory.Title = task.Name;
            _userStoryRepository.Save(userStory);
        }

        // DELETE: api/UserStory/5
        public void Delete(Guid id)
        {
            _userStoryRepository.Delete(id);
        }
    }
}
