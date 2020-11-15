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
    public class SendDecisionCommandHandler : IRequestHandler<SendDecisionCommand>
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ISendEndpointProvider _serviceBus;

        public SendDecisionCommandHandler(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetService<ILogger<SendDecisionCommandHandler>>();
            _mapper = serviceProvider.GetService<IMapper>();
            _serviceBus = serviceProvider.GetService<ISendEndpointProvider>();
        }

        public async Task<Unit> Handle(SendDecisionCommand request, CancellationToken cancellationToken)
        {
            var caseToSubmit = new FacCaseDecision() { DecisionType = "New Case" };
            await _serviceBus.Send(caseToSubmit);
            _logger.LogDebug("Case Submitted");
            return new Unit();
        }

    }
}
