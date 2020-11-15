using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using System;

namespace ReverseProxy.Extensions
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
                //x.AddConsumer<MibCompletedConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{serviceBusSettings.ServerName}"), h =>
                    {
                        h.Username(serviceBusSettings.UserName);
                        h.Password(serviceBusSettings.Password);
                    });
                    //   cfg.ReceiveEndpoint(serviceBusSettings.ListenQueueName, e =>
                    //   {

                    //       e.Consumer<MibCompletedConsumer>(context);
                    //       e.UseMessageRetry(r =>
                    //       {
                    //           r.Immediate(serviceBusSettings.NumberOfRetries);
                    //           r.Intervals(serviceBusSettings.RetryInterval);
                    //       });

                    //   });

                    //   EndpointConvention.Map<MibSubmitted>(new Uri($"rabbitmq://{serviceBusSettings.ServerName}/{serviceBusSettings.SubmitQueueName}"));

                });
            });

            services.AddMassTransitHostedService();
            return services;
        }
    }
}