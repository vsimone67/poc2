using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using Polly.Extensions.Http;

namespace Fac.Service.Extensions
{
    public static class HttpClientExtensions
    {
        public static IServiceCollection AddHttpClientHandlers(this IServiceCollection services, IConfiguration Configuration)
        {
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var logger = serviceProvider.GetService<ILoggerFactory>();

                //https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
                //services.AddHttpClient<IHttpClientWrapper, HttpClientWrapper>().AddPolicyHandler(GetRetryPolicy(3, logger.CreateLogger("httpclient")));
                services.AddHttpClient<IMibService, MibService>(client =>
                {
                    client.BaseAddress = new Uri("http://mibprocessor-svc/mib/");
                }).AddPolicyHandler(GetRetryPolicy(3, logger.CreateLogger("mibservice")));
            }

            return services;
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(int numberOfRetries, ILogger logger)
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(numberOfRetries, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), onRetry: (exception, calculatedWaitDuration) =>
                {
                    //logger.LogError($"Retry Policy Executed => {exception.Exception.Message}");
                });
        }

        static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }

    }



    public interface IHttpClientWrapper
    {
        Task<T> GetData<T>(string url);
        string BaseAddress { get; set; }

    }
    public class HttpClientWrapper : IHttpClientWrapper
    {
        public string BaseAddress { get; set; }
        private readonly HttpClient _httpClient;

        public HttpClientWrapper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetData<T>(string url)
        {

            if (!string.IsNullOrEmpty(BaseAddress))
                _httpClient.BaseAddress = new Uri(BaseAddress);

            var responseString = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<T>(responseString);

        }

        // public async Task PostData<T>(string url, T model)
        // {
        //     if (!string.IsNullOrEmpty(BaseAddress))
        //         _httpClient.BaseAddress = new Uri(BaseAddress);

        //     var responseString = await _httpClient.PostAsync(url,model);
        //     return JsonConvert.DeserializeObject<T>(responseString);
        // }
    }

    public interface IMibService
    {
        Task<string> GetMibRouteData();
    }
    public class MibService : IMibService
    {
        private readonly HttpClient _httpClient;

        public MibService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetMibRouteData()
        {

            var responseString = await _httpClient.GetStringAsync("MyRoute");

            return JsonConvert.DeserializeObject<string>(responseString);

        }
    }


}
