using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Hub.Processor.Extensions
{
    public static class CorsExtention
    {
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors();

            return services;
        }

        public static IApplicationBuilder UserCorsPolicy(this IApplicationBuilder builder)
        {
            builder.UseCors(cors => cors
                   //.SetIsOriginAllowed((host) => true)
                   .WithOrigins("http://localhost:4200")
                   //.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials()
                   );

            return builder;
        }
    }
}