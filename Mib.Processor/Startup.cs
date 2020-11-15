using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Mib.Processor.Extensions;

namespace Mib.Processor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCorsPolicy();
            services.AddMvcExtensions(Configuration);
            services.AddCommandQueryHandlers();
            services.AddMappingProfiles();
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
            services.ConfigureDiEnvironment(Configuration);
            services.MapConfigToClass(Configuration);
            services.AddDistributedTracing(Configuration);
            services.AddMassTransitRabbitMq();
            services.AddOpenApi();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseOpenApi();
            }

            app.UseRouting();
            app.UserCorsPolicy();
            app.UseMvcExtensions();
            //app.UseHttpsRedirection();
        }
    }
}
