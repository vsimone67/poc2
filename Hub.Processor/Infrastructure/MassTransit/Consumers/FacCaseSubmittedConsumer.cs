using System.Threading.Tasks;
using FacCaseProcessor.Service.Infrastructure.MassTransit.Models;
using Hub.Processor.Application.Hubs;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
namespace Hub.Processor.Extensions.MassTransit.Consumers
{
    public class FacCaseSubmittedConsumer : IConsumer<FacCaseEvent>
    {
        private readonly ILogger<FacDecisionCompletedConsumer> _logger;
        private readonly IHubContext<FacCaseHub> _hubContext;
        public FacCaseSubmittedConsumer(ILogger<FacDecisionCompletedConsumer> logger, IHubContext<FacCaseHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }
        public async Task Consume(ConsumeContext<FacCaseEvent> context)
        {
            _logger.LogDebug("Sending Message To Case Hub");
            await _hubContext.Clients.All.SendAsync("FacCaseSubmitted", context.Message);
            _logger.LogDebug("Case Hub Message Sent");
        }
    }
}
