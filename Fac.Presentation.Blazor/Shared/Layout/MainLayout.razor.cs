using System.Threading.Tasks;
using Fac.Presentation.Blazor.Constants;
using Fac.Presentation.Blazor.Service.EventService;
using Fac.Presentation.Blazor.SignalR;
using Microsoft.AspNetCore.Components;
using Fac.Presentation.Blazor.Models.Events;

namespace Fac.Presentation.Blazor.Shared.Layout
{
    public partial class MainLayout
    {
        [Inject]
        private IEventService eventService { get; set; }
        protected override async Task OnInitializedAsync()
        {

            var mibHubConnection = await SignalRHubManager.CreateConnection<object>("http://traefik.prod/mibhub", "MibCompleted", async (message) =>
             {
                 await eventService.SendEventAsync(SiteConstants.Event.Toast.ToString(), new ToastEventArgs() { Message = "Mib Processed" });
             });

            var facDecisionConnection = await SignalRHubManager.CreateConnection<object>("http://traefik.prod/facdecision", "FacDecisionMade", async (message) =>
             {
                 await eventService.SendEventAsync(SiteConstants.Event.Toast.ToString(), new ToastEventArgs() { Message = "FAC Processed" });
             });

            var facCaseConnection = await SignalRHubManager.CreateConnection<object>("http://traefik.prod/faccase", "FacCaseSubmitted", async (message) =>
             {
                 await eventService.SendEventAsync(SiteConstants.Event.Toast.ToString(), new ToastEventArgs() { Message = "Case Processed" });
             });
        }
    }
}