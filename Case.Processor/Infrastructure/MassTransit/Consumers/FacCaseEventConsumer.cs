using System.Threading.Tasks;
using Fac.Service.Infrastructure.MassTransit.Models;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using MyNamespace.Application.Commands;

namespace Case.Processor.Extensions.MassTransit.Consumers
{
    public class FacCaseEventConsumer : IConsumer<FacCaseSubmitted>
    {
        ILogger<FacCaseEventConsumer> _logger;
        IMediator _mediator;
        public FacCaseEventConsumer(ILogger<FacCaseEventConsumer> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        public async Task Consume(ConsumeContext<FacCaseSubmitted> context)
        {
            _logger.LogDebug($"Recevied Case for processing command handler");
            await _mediator.Send(new ProcessCaseCommand() { FacCase = context.Message });
            _logger.LogDebug("Case for processing command handler completed");
        }
    }
}
