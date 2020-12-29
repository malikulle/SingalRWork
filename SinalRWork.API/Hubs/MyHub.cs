using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRWork.Domain.Data;
using SignalRWork.Domain.Entities;
using SinalRWork.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinalRWork.API.Hubs
{
    public class MyHub : Hub
    {
        private readonly AppDbContext _context;

        private static List<string> Names { get; set; } = new List<string>();
        private static int ClientCount { get; set; } = 0;
        public static int TeamCount { get; set; } = 7;

        public MyHub(AppDbContext context)
        {
            _context = context;
        }

        public async Task SendProduct(Product product)
        {
            await Clients.All.SendAsync("ReceiveProduct", product);
        }

        public async Task SendName(string name)
        {
            if (Names.Count >= TeamCount)
            {                
                await Clients.Caller.SendAsync("Error", $"Takım En fazla {TeamCount} Kişi olabilir");
            }
            else
            {
                Names.Add(name);
                await Clients.All.SendAsync("ReceiveName", name);
            }

        }

        public async Task GetNames()
        {
            await Clients.All.SendAsync("ReceiveNames", Names);
        }

        public async Task SendNameByGroup(string Name,string teamName)
        {
            var team = _context.Teams.FirstOrDefault(x => x.Name == teamName);

            if (team != null)
            {
                team.Users.Add(new User { Name = Name });
            }
            else
            {
                var newTeam = new Team
                {
                    Name = teamName
                };
                newTeam.Users.Add(new User { Name = Name });

                _context.Add(newTeam);
            }

            await _context.SaveChangesAsync();

            await Clients.Group(teamName).SendAsync("ReceiveMessageByGroup",Name,team.Id);
        }

        public async Task GetNamesByGroup()
        {
            var teams = _context.Teams.Include(x => x.Users).Select(x => new
            {
                teamId = x.Id,
                teamName = x.Name,
                Users = x.Users.ToList()
            }).ToList();

            await Clients.All.SendAsync("ReceiveNamesByGroup",teams);
        }

        public async Task AddToGroup(string teamName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, teamName);
        }

        public async Task RemoveToGroup(string teamName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, teamName);
        }

        public async override Task OnConnectedAsync()
        {
            ClientCount++;
            await Clients.All.SendAsync("ReceiveClientCount", ClientCount);

            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            ClientCount--;
            if (ClientCount < 0)
            {
                ClientCount = 0;
            }
            await Clients.All.SendAsync("ReceiveClientCount", ClientCount);

            await base.OnDisconnectedAsync(exception);
        }
    }
}
