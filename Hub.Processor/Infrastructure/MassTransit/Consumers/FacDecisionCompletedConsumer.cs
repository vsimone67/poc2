using System.Threading.Tasks;
using Correspondence.Processor.Infrastructure.MassTransit.Models;
using Hub.Processor.Application.Hubs;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Hub.Processor.Extensions.MassTransit.Consumers
{
    public class FacDecisionCompletedConsumer : IConsumer<FacDecisionEvent>
    {
        private readonly ILogger<FacDecisionCompletedConsumer> _logger;
        private readonly IHubContext<FacDecisionHub> _hubContext;

        public FacDecisionCompletedConsumer(ILogger<FacDecisionCompletedConsumer> logger, IHubContext<FacDecisionHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        public async Task Consume(ConsumeContext<FacDecisionEvent> context)
        {
            _logger.LogDebug("Sending Message To Decision Hub");
            await _hubContext.Clients.All.SendAsync("FacDecisionMade", context.Message);
            _logger.LogDebug("Decision Hub Message Sent");
        }
    }
}