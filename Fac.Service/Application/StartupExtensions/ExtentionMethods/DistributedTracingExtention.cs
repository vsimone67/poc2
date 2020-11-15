using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.Management.Tracing;

namespace Fac.Service.Extensions
{
    public static class DistributedTracingExtension
    {
        public static IServiceCollection AddDistributedTracing(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDistributedTracing(Configuration, builder => builder.UseZipkinWithTraceOptions(services));
            return services;
        }
    }


}
