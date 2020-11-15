using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Correspondence.Processor.Extensions;

namespace Correspondence.Processor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var basePath = Environment.GetEnvironmentVariable("appdirectory").NullToEmpty();

                var configuration = new ConfigurationBuilder()
                    .AddJsonFile($"{basePath}appsettings.json")
                    .Build();

                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();

                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .AddAppConfigurationFromEnvironment()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
