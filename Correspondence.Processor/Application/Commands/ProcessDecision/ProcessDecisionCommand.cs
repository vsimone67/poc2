using Fac.Service.Infrastructure.MassTransit.Models;
using MediatR;

namespace MyNamespace.Application.Commands
{
    public class ProcessDecisionCommand : IRequest
    {
        public FacCaseDecision FacDecision { get; set; }
    }
}
