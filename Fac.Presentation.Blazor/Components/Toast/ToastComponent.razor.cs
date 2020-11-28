using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Blazorise.Snackbar;
using Fac.Presentation.Blazor.Service.EventService;
using System;
using Fac.Presentation.Blazor.Constants;
using Fac.Presentation.Blazor.Models.Events;

namespace Fac.Presentation.Blazor.Components.Toast
{
    public partial class ToastComponent
    {
        [Inject]
        private IEventService eventService { get; set; }

        private SnackbarStack snackbarStack;
        private int Interval;
        protected override async Task OnInitializedAsync()
        {
            //eventService.Subscribe(SiteConstants.Event.Toast.ToString(), OnReceiveEvent); // subscribe to the toast event that will trigger the message being displayed
            eventService.Subscribe(SiteConstants.Event.Toast.ToString(), async (args) => await OnReceiveEventAsync(args)); // subscribe to the toast event that will trigger the message being displayed
            await Task.CompletedTask;
        }
        private Task OnReceiveEvent(EventArgs eventArgs)
        {
            var messageArgs = (ToastEventArgs)eventArgs;  // convert the args to the toast version
            Interval = messageArgs.Interval;
            snackbarStack.Interval = Interval;
            snackbarStack.Push(messageArgs.Message, messageArgs.Color);
            return Task.CompletedTask;
        }

        private async Task OnReceiveEventAsync(EventArgs eventArgs)
        {
            var messageArgs = (ToastEventArgs)eventArgs;  // convert the args to the toast version
            Interval = messageArgs.Interval;
            snackbarStack.Interval = Interval;
            snackbarStack.Push(messageArgs.Message, messageArgs.Color);
            await Task.CompletedTask;
        }
    }
}