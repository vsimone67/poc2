using Blazorise.Snackbar;
using System;

namespace Fac.Presentation.Blazor.Models.Events
{
    public class ToastEventArgs : EventArgs
    {
        public string Message { get; set; }
        public SnackbarColor Color { get; set; }
        public int Interval { get; set; }

        public ToastEventArgs()
        {
            Color = SnackbarColor.Success; // default to success
            Interval = 3000;
        }
    }
}