using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using StyrBoard.View.Repository;

namespace StyrBoard.Web.Controllers
{
    public class ListController : ApiController
    {
        private readonly IListRepository _listRepository;

        public ListController(IListRepository listRepository)
        {
            _listRepository = listRepository;
        }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            var response = Request.CreateResponse();
            var list = _listRepository.Get();
            response.Content = new StringContent(JsonConvert.SerializeObject(list.Cards));
            response.StatusCode = HttpStatusCode.OK;

            return response;
        }
    }
}
