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
    public class SubmitCaseCommandHandler : IRequestHandler<SubmitCaseCommand>
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ISendEndpointProvider _serviceBus;


        public SubmitCaseCommandHandler(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetService<ILogger<SubmitCaseCommandHandler>>();
            _mapper = serviceProvider.GetService<IMapper>();
            _serviceBus = serviceProvider.GetService<ISendEndpointProvider>();
        }

        public async Task<Unit> Handle(SubmitCaseCommand request, CancellationToken cancellationToken)
        {
            var mibToSubmit = new FacCaseSubmitted() { Action = "Save" };
            await _serviceBus.Send(mibToSubmit);
            _logger.LogDebug("Case Submitted");
            return new Unit();
        }

    }
}
