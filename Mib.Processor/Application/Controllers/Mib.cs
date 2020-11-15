using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mib.Processor.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MibController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MibController> _logger;

        public MibController(IMediator mediator, ILogger<MibController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("MyRoute")]
        public async Task<ActionResult> MyRoute()
        {
            _logger.LogDebug("Begin");
            _logger.LogDebug("End");
            await Task.FromResult(1);
            return Ok("hello");
        }
    }
}
