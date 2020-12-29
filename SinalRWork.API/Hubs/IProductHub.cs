using SinalRWork.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinalRWork.API.Hubs
{
    public interface IProductHub
    {
        Task ReceiveProduct(Product product);
    }
}
