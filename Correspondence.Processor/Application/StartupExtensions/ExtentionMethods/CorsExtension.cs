﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Correspondence.Processor.Extensions
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
                   .SetIsOriginAllowed((host) => true)
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials());

            return builder;
        }
    }
}