using System;
using System.Threading;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using MassTransit;
using Fac.Service.Infrastructure.MassTransit.Models;

namespace Mib.Service.Application.Commands
{
    public class ProcessMibCommandHandler : IRequestHandler<ProcessMibCommand>
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _serviceBus;


        public ProcessMibCommandHandler(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetService<ILogger<ProcessMibCommandHandler>>();
            _mapper = serviceProvider.GetService<IMapper>();
            _serviceBus = serviceProvider.GetService<IPublishEndpoint>(); // event (publish)
        }

        public async Task<Unit> Handle(ProcessMibCommand request, CancellationToken cancellationToken)
        {
            var mibToProcess = request.MibToProcess;

            _logger.LogDebug("Entered ProcessMib Command Handler");
            await Task.Delay(10000);  // emulate the time it would take to call MIB and get data back
            _logger.LogDebug("Finished task delay");
            var mibCompleted = new MibCompleted() { MibId = 1000, Result = "OK" };
            _logger.LogDebug("Mib has been processed, sending event back");
            await _serviceBus.Publish(mibCompleted);
            _logger.LogDebug("Mib Completed, sent response back to queue");
            return new Unit();
        }

    }
}
