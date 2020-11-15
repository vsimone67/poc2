using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Hub.Processor.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddOpenApi(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplication1", Version = "v1" });
            });

            return services;
        }

        public static IApplicationBuilder UseOpenApi(this IApplicationBuilder builder)
        {
            builder.UseSwagger();
            builder.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication1 v1"));
            return builder;
        }
    }
}
