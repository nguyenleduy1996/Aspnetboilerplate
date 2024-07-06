using Abp.Dependency;
using Abp.Runtime.Session;
using Castle.Core.Configuration;
using Castle.Core.Logging;
using LearnAPI.Repos;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRDemo3ytEFC.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRDemo3ytEFC.Hubs
{
    public class DashboardHub : Hub, ITransientDependency
    {

        private readonly LearndataContext _Context;
        public IAbpSession AbpSession { get; set; }
        public ILogger Logger { get; set; }

        public DashboardHub(LearndataContext context)
        {
            AbpSession = NullAbpSession.Instance;
            Logger = NullLogger.Instance;
            _Context = context;
        }

        public async Task SendMessage(string message)
        {
            //await Clients.All.SendAsync("getMessage", string.Format("User {0}: {1}", AbpSession.UserId, message));
            await Clients.User("3").SendAsync("getMessage", message);
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            Logger.Debug("A client connected to MyChatHub: " + Context.ConnectionId);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
            Logger.Debug("A client disconnected from MyChatHub: " + Context.ConnectionId);
        }

        public async Task SendProducts()
        {
            var  product = await _Context.Product.ToListAsync();
            var ProductRepository = new ProductRepository(_Context);
            var pro = ProductRepository.GetProducts();
            await Clients.All.SendAsync("ReceivedProducts", pro);

            /* var products = productRepository.GetProducts();
             await Clients.All.SendAsync("ReceivedProducts", products);

             var productsForGraph = productRepository.GetProductsForGraph();
             await Clients.All.SendAsync("ReceivedProductsForGraph", productsForGraph);*/
        }
    }


  
}

