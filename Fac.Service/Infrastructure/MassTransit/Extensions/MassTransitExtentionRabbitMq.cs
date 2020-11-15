using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using System;
using Fac.Service.Infrastructure.MassTransit.Models;

namespace Fac.Service.Extensions.MassTransit.RabbitMq
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

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{serviceBusSettings.ServerName}"), h =>
                    {
                        h.Username(serviceBusSettings.UserName);
                        h.Password(serviceBusSettings.Password);
                    });

                    // the is the way to setup publising events
                    EndpointConvention.Map<MibSubmitted>(new Uri($"rabbitmq://{serviceBusSettings.ServerName}/{serviceBusSettings.SubmitMibQueue}"));
                    EndpointConvention.Map<FacCaseDecision>(new Uri($"rabbitmq://{serviceBusSettings.ServerName}/{serviceBusSettings.FacCaseDecisionQueue}"));
                    EndpointConvention.Map<FacCaseSubmitted>(new Uri($"rabbitmq://{serviceBusSettings.ServerName}/{serviceBusSettings.FacCaseSubmittedQueue}"));
                });
            });

            services.AddMassTransitHostedService();
            return services;
        }
    }
}