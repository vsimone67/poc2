using Fac.Service.Infrastructure.MassTransit.Models;
using MediatR;

namespace MyNamespace.Application.Commands
{
    public class ProcessCaseCommand : IRequest
    {
        public FacCaseSubmitted FacCase { get; set; }
    }
}
