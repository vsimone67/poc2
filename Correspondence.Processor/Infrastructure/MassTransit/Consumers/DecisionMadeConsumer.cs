using System.Threading.Tasks;
using Fac.Service.Infrastructure.MassTransit.Models;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using MyNamespace.Application.Commands;

namespace Correspondence.Processor.Extensions.MassTransit.Consumers
{
    public class DecisionMadeConsumer : IConsumer<FacCaseDecision>
    {
        ILogger<DecisionMadeConsumer> _logger;
        private readonly IMediator _mediator;
        public DecisionMadeConsumer(ILogger<DecisionMadeConsumer> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        public async Task Consume(ConsumeContext<FacCaseDecision> context)
        {
            _logger.LogDebug($"Recevied Decision Command, processing command handler");
            await _mediator.Send(new ProcessDecisionCommand() { FacDecision = context.Message });
            _logger.LogDebug("Received Decision command handler completed");
        }
    }
}
