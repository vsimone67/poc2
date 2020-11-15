using System;
using System.Threading;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using MassTransit;
using Fac.Service.Infrastructure.MassTransit.Models;

namespace Fac.Service.Application.Commands
{
    public class SendMibHandler : IRequestHandler<SendMib>
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ISendEndpointProvider _serviceBus;


        public SendMibHandler(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetService<ILogger<SendMibHandler>>();
            _mapper = serviceProvider.GetService<IMapper>();
            _serviceBus = serviceProvider.GetService<ISendEndpointProvider>();  // command (send)
        }

        public async Task<Unit> Handle(SendMib request, CancellationToken cancellationToken)
        {
            var mibToSubmit = new MibSubmitted() { FirstName = "MyFirstName", LastName = "MyLastName", Dob = new DateTime(2000, 01, 15) };
            await _serviceBus.Send(mibToSubmit);
            _logger.LogDebug("Mib Sent to service bus");
            return new Unit();
        }

    }
}
