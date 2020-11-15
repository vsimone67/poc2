using System;
using Fac.Service.Infrastructure.MassTransit.Models;

namespace Correspondence.Processor.Infrastructure.MassTransit.Models
{
    public class FacDecisionEvent
    {
        public FacCaseDecision FacDecision { get; set; }
    }
}
