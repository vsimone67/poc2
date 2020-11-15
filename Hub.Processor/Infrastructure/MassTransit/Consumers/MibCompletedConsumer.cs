using System;
using System.Threading.Tasks;
using Fac.Service.Infrastructure.MassTransit.Models;
using Hub.Processor.Application.Hubs;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Hub.Processor.Extensions.MassTransit.Consumers
{
    public class MibCompletedConsumer : IConsumer<MibCompleted>
    {
        private readonly ILogger<MibCompletedConsumer> _logger;
        private readonly IHubContext<MibHub> _hubContext;
        public MibCompletedConsumer(ILogger<MibCompletedConsumer> logger, IHubContext<MibHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }
        public async Task Consume(ConsumeContext<MibCompleted> context)
        {
            _logger.LogDebug("Sending Message To Hub");
            await _hubContext.Clients.All.SendAsync("MibCompleted", context.Message);
            //await _hubContext.Clients.User(context.Message.User).SendAsync("MibCompleted", context.Message);
            _logger.LogDebug("Hub Message Sent");
        }
    }
}
