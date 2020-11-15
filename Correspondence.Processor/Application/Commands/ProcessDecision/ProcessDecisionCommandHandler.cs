using System;
using System.Threading;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using MassTransit;
using Correspondence.Processor.Infrastructure.MassTransit.Models;

namespace MyNamespace.Application.Commands
{
    public class ProcessDecisionCommandHandler : IRequestHandler<ProcessDecisionCommand>
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _serviceBus;


        public ProcessDecisionCommandHandler(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetService<ILogger<ProcessDecisionCommandHandler>>();
            _mapper = serviceProvider.GetService<IMapper>();
            _serviceBus = serviceProvider.GetService<IPublishEndpoint>();
        }

        public async Task<Unit> Handle(ProcessDecisionCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(5000);  // emulate the time it would take to process
            _logger.LogDebug("Publish Facultative Decision Event");
            var caseEvent = new FacDecisionEvent() { FacDecision = request.FacDecision };
            await _serviceBus.Publish(caseEvent);
            _logger.LogDebug("Published Facultative Decision Event");
            return new Unit();
        }

    }
}
