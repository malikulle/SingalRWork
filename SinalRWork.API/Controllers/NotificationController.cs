using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SinalRWork.API.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinalRWork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {

        private readonly IHubContext<MyHub> _hubContext;
        public NotificationController(IHubContext<MyHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpGet("{teamCount}")]
        public async Task<IActionResult> SetTeamCount([FromRoute] int teamCount)
        {
            MyHub.TeamCount = teamCount;
            await _hubContext.Clients.All.SendAsync("Notify", $"Takımlar {teamCount} kişi kadardır");

            return Ok();
        }


    }
}
