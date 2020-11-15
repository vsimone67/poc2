using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using System;
using GreenPipes;
using Hub.Processor.Extensions.MassTransit.Consumers;
using Hub.Processor.Extensions;
using Hub.Processor;

namespace Hub.Processor.Extensions
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
                  x.AddConsumer<MibCompletedConsumer>();  // this is the way to subscribe to events/commands
                  x.AddConsumer<FacDecisionCompletedConsumer>();
                  x.AddConsumer<FacCaseSubmittedConsumer>();

                  x.UsingRabbitMq((context, cfg) =>
                  {
                      cfg.Host(new Uri($"rabbitmq://{serviceBusSettings.ServerName}"), h =>
                      {
                          h.Username(serviceBusSettings.UserName);
                          h.Password(serviceBusSettings.Password);
                      });
                      cfg.ReceiveEndpoint(serviceBusSettings.SubmitMibEventQueue, e => // this is the way to subscribe to events/commands
                      {
                          e.Consumer<MibCompletedConsumer>(context);
                          e.UseMessageRetry(r =>
                          {
                              r.Immediate(serviceBusSettings.NumberOfRetries);
                              r.Intervals(serviceBusSettings.RetryInterval);
                          });

                      });
                      cfg.ReceiveEndpoint(serviceBusSettings.FacDecisionEventQueue, e => // this is the way to subscribe to events/commands
                      {
                          e.Consumer<FacDecisionCompletedConsumer>(context);
                          e.UseMessageRetry(r =>
                          {
                              r.Immediate(serviceBusSettings.NumberOfRetries);
                              r.Intervals(serviceBusSettings.RetryInterval);
                          });

                      });
                      cfg.ReceiveEndpoint(serviceBusSettings.FacCaseEventQueue, e => // this is the way to subscribe to events/commands
                      {
                          e.Consumer<FacCaseSubmittedConsumer>(context);
                          e.UseMessageRetry(r =>
                          {
                              r.Immediate(serviceBusSettings.NumberOfRetries);
                              r.Intervals(serviceBusSettings.RetryInterval);
                          });

                      });
                  });
              });

            services.AddMassTransitHostedService();
            return services;
        }
    }



}