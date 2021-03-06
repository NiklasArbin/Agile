﻿using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Agile.Web.Controllers
{
    [HubName("MainHub")]
    public class HubController : Hub
    {
        public void NotifyBoardUpdated()
        {
            Clients.All.BoardUpdated();
        }
    }
}