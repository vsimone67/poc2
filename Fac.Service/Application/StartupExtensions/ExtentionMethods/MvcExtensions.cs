using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Steeltoe.Management.Endpoint;
using Steeltoe.Management.Endpoint.Health;
using Steeltoe.Management.Endpoint.Metrics;

namespace Fac.Service.Extensions
{
    public static class MvcExtensions
    {
        public static IServiceCollection AddMvcExtensions(this IServiceCollection services, IConfiguration Configuration)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // HTTPGlobalExcetptionFilter will fire on any internal exception and send back to UI the error
            services.AddMvc(options => options.Filters.Add(typeof(HttpGlobalExceptionFilter)))
                    .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddControllers();

            //TODO: Uncomment line below and add DB Context
            //services.AddHealthChecks().AddDbContextCheck<CrmContext>();
            services.AddHealthChecks();
            services.AddHealthActuator(Configuration);
            services.AddMetricsActuator(Configuration);
            return services;
        }

        public static IApplicationBuilder UseMvcExtensions(this IApplicationBuilder builder)
        {
            builder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.Map<HealthEndpoint>();
                endpoints.Map<MetricsEndpoint>();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
            return builder;
        }
    }
}