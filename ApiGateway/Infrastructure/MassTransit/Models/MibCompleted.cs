using System;

namespace Fac.Service.Infrastructure.MassTransit.Models
{
    public class MibCompleted
    {
        public int MibId { get; set; }
        public string Result { get; set; }
    }
}
