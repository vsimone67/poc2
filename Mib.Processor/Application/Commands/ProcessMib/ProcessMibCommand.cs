using Fac.Service.Infrastructure.MassTransit.Models;
using MediatR;

namespace Mib.Service.Application.Commands
{
    public class ProcessMibCommand : IRequest
    {
        public MibSubmitted MibToProcess { get; set; }
    }
}
