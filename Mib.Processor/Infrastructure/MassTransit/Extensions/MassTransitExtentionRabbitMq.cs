using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using System;
using GreenPipes;
using Mib.Service.Extensions.MassTransit.Consumers;
using Fac.Service.Infrastructure.MassTransit.Models;

namespace Mib.Processor.Extensions
{
    public static class MassTransitExtention
    {
        public static IServiceCollection AddMassTransitRabbitMq(this IServiceCollection services)
        {
            //https://masstransit-project.com/getting-started/
            services.AddMassTransit(x =>
              {
                  ServiceBusSettings serviceBusSettings;
                  using (var serviceProvider = services.BuildServiceProvider())
                  {
                      var configuration = serviceProvider.GetService<IConfiguration>();
                      services.Configure<ServiceBusSettings>(configuration.GetSection("ServiceBusSettings"));
                      serviceBusSettings = configuration.GetOptions<ServiceBusSettings>("ServiceBusSettings");
                  }
                  x.AddConsumer<MibSubmittedConsumer>();

                  x.UsingRabbitMq((context, cfg) =>
                  {
                      cfg.Host(new Uri($"rabbitmq://{serviceBusSettings.ServerName}"), h =>
                      {
                          h.Username(serviceBusSettings.UserName);
                          h.Password(serviceBusSettings.Password);
                      });
                      cfg.ReceiveEndpoint(serviceBusSettings.ListenQueueName, e =>
                      {

                          e.Consumer<MibSubmittedConsumer>(context);
                          e.UseMessageRetry(r =>
                          {
                              r.Immediate(serviceBusSettings.NumberOfRetries);
                              r.Intervals(serviceBusSettings.RetryInterval);
                          });

                      });

                      EndpointConvention.Map<MibCompleted>(new Uri($"rabbitmq://{serviceBusSettings.ServerName}/{serviceBusSettings.SubmitQueueName}"));

                  });
              });

            services.AddMassTransitHostedService();
            return services;
        }


    }


}