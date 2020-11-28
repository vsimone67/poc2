using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using Fac.Presentation.Blazor.Service.EventService;
using Fac.Presentation.Blazor.Constants;
using Fac.Presentation.Blazor.Models.Events;

namespace Fac.Presentation.Blazor.Pages.SendMessages
{
    public partial class SendMessages
    {
        [Inject]
        private HttpClient httpClient { get; set; }
        [Inject]
        private IEventService eventService { get; set; }
        protected async Task SubmitMib()
        {
            await httpClient.GetAsync("/fac/submitmib");
            await eventService.SendEventAsync(SiteConstants.Event.Toast.ToString(), new ToastEventArgs() { Message = "Mib Sent" });
        }
        protected async Task SubmitCaseDecision()
        {
            await httpClient.GetAsync("/fac/FacCaseDecision");
            await eventService.SendEventAsync(SiteConstants.Event.Toast.ToString(), new ToastEventArgs() { Message = "Decision Sent" });
        }
        protected async Task SubmitCase()
        {
            await httpClient.GetAsync("/fac/SubmitCase");
            await eventService.SendEventAsync(SiteConstants.Event.Toast.ToString(), new ToastEventArgs() { Message = "Case Sent" });
        }
    }
}