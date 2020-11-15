using System.Threading.Tasks;
using Fac.Service.Infrastructure.MassTransit.Models;
using MassTransit;
using MediatR;
using Mib.Service.Application.Commands;
using Microsoft.Extensions.Logging;

namespace Mib.Service.Extensions.MassTransit.Consumers
{
    public class MibSubmittedConsumer : IConsumer<MibSubmitted>
    {
        ILogger<MibSubmittedConsumer> _logger;
        private readonly IMediator _mediator;
        public MibSubmittedConsumer(ILogger<MibSubmittedConsumer> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        public async Task Consume(ConsumeContext<MibSubmitted> context)
        {
            _logger.LogDebug($"Recevied Mib To process for {context.Message.FirstName} {context.Message.LastName}");
            await _mediator.Send(new ProcessMibCommand() { MibToProcess = context.Message });
            _logger.LogDebug("MibSubmitted Consume has completed");

        }
    }
}
