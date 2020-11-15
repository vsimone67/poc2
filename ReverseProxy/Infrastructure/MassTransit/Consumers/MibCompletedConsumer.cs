using System;
using System.Threading.Tasks;
using Fac.Service.Infrastructure.MassTransit.Models;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace ReverseProxy.Extensions.MassTransit.Consumers
{
    public class MibCompletedConsumer : IConsumer<MibCompleted>
    {
        ILogger<MibCompletedConsumer> _logger;
        public MibCompletedConsumer(ILogger<MibCompletedConsumer> logger)
        {
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<MibCompleted> context)
        {
            await Task.FromResult(1);
            _logger.LogInformation("Value: {Value}", context.Message.Result);
        }
    }
}
