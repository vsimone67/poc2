using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Hub.Processor.Application.Hubs
{
    public class FacCaseHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly ILogger<FacCaseHub> _logger;
        public FacCaseHub(ILogger<FacCaseHub> logger)
        {
            _logger = logger;
        }
        public override async Task OnConnectedAsync()
        {
            _logger.LogDebug($"Client Connected {Context.User.Identity.Name}");
            await base.OnConnectedAsync();

            await this.Clients.All.SendAsync("UserConnected");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
            await this.Clients.All.SendAsync("UserDisConnected");
        }
    }
}
