using System;
using System.Threading;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using MassTransit;
using Case.Processor.Infrastructure.MassTransit.Models;

namespace MyNamespace.Application.Commands
{
    public class ProcessCaseCommandHandler : IRequestHandler<ProcessCaseCommand>
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _serviceBus;

        public ProcessCaseCommandHandler(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetService<ILogger<ProcessCaseCommandHandler>>();
            _mapper = serviceProvider.GetService<IMapper>();
            _serviceBus = serviceProvider.GetService<IPublishEndpoint>();
        }

        public async Task<Unit> Handle(ProcessCaseCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(5000);  // emulate the time it would take to process
            _logger.LogDebug("Publish Process Case Event");
            var caseEvent = new FacCaseEvent() { CaseName = "moo" };
            await _serviceBus.Publish(caseEvent);
            _logger.LogDebug("Published Process Case Event");
            return new Unit();
        }

    }
}
