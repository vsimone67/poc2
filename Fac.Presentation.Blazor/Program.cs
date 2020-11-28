using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Blazorise;
using Fac.Presentation.Blazor.Service.EventService;

namespace Fac.Presentation.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services
            .AddBlazorise(options =>
            {
                options.ChangeTextOnKeyPress = true;
            })
            .AddBootstrapProviders()
            .AddFontAwesomeIcons();

            builder.Services.AddSingleton(new HttpClient
            {
                BaseAddress = new Uri("http://traefik.prod")
            });

            builder.Services.AddSingleton<IEventService, EventService>();
            builder.RootComponents.Add<App>("#app");

            var host = builder.Build();

            host.Services
            .UseBootstrapProviders()
            .UseFontAwesomeIcons();

            await host.RunAsync();
        }
    }
}