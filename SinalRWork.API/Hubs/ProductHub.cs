using Microsoft.AspNetCore.SignalR;
using SinalRWork.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinalRWork.API.Hubs
{
    public class ProductHub : Hub<IProductHub>
    {
        public async Task SendProduct(Product product)
        {
            await Clients.All.ReceiveProduct(product);
        }
    }
}
