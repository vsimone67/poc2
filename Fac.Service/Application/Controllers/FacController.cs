using System.Net;
using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Fac.Service.Application.Commands;
using Fac.Service.Infrastructure.MassTransit.Models;
using System.Net.Http;
using Fac.Service.Extensions;

namespace Fac.Service
{
    [Route("[controller]")]
    [ApiController]
    public class FacController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<FacController> _logger;
        private readonly IMibService _mibService;

        public FacController(IMediator mediator, ILogger<FacController> logger, IMibService mibService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mibService = mibService ?? throw new ArgumentNullException(nameof(mibService)); ;
        }

        [HttpGet]
        [Route("SubmitMib")]
        public async Task<ActionResult> SubmitMib()
        {
            _logger.LogDebug("Submitting MIB for processing");
            var data = await _mediator.Send(new SendMib());
            _logger.LogDebug("MIB has been sent");
            return Ok(data);
        }

        //[Authorize(Roles = "Administrator")]
        [HttpGet]
        [Route("FacCaseDecision")]
        public async Task<ActionResult> FacCaseDecision()
        {
            _logger.LogDebug("Submitting Fac case decison");
            var data = await _mediator.Send(new SendDecisionCommand());
            _logger.LogDebug("Case has been sent");
            return Ok(data);
        }

        [HttpGet]
        [Route("SubmitCase")]
        public async Task<ActionResult> SubmitCase()
        {
            _logger.LogDebug("Submitting Fac case");
            var data = await _mediator.Send(new SubmitCaseCommand());
            _logger.LogDebug("Submit case has been sent");
            return Ok(data);
        }

        [HttpPost]
        [Route("PostMib")]
        public async Task<IActionResult> PostMib([FromBody] MibSubmitted mib)
        {
            await Task.FromResult(1);
            return Ok(mib);
        }

        [HttpGet]
        [Route("Test")]
        public async Task<ActionResult> Test()
        {
            // int retries = 1;
            // var retryPolicy = Policy.Handle<Exception>().WaitAndRetryAsync(
            //   retryCount: 3, // Retry 3 times
            //   sleepDurationProvider: attempt => TimeSpan.FromMilliseconds(1000 * ((attempt == 0) ? 1 : attempt)), // Wait 200ms between each try.
            //   onRetry: (exception, calculatedWaitDuration) => // Capture some info for logging!
            //   {
            //       _logger.LogError($" Error Submitting Case: {exception.Message}, retrying: {retries} of {3} Date {DateTime.Now}");
            //       retries++;
            //   });

            // CancellationTokenSource source = new CancellationTokenSource();
            // await retryPolicy.ExecuteAsync(async token =>
            //     {
            //         HttpStatusCode returncode;
            //         var client = new HttpClient();

            //         var result = await client.GetAsync("http://mibprocessor-svc/mib/MyRoute");

            //         // Ensure we have a Success Status Code
            //         result.EnsureSuccessStatusCode();

            //         // Read Response Content (this will usually be JSON content)
            //         var content = await result.Content.ReadAsStringAsync();

            //         returncode = result.StatusCode;
            //         return Ok(content);
            //     }, source.Token).ConfigureAwait(false);
            // return Ok();

            var result = await _mibService.GetMibRouteData();
            return Ok(result);
        }

        [HttpGet]
        [Route("Test2")]
        public async Task<ActionResult> Test2()
        {

            HttpStatusCode returncode;
            var client = new HttpClient();

            var result = await client.GetAsync("http://workoutservice-svc.fitnesstracker.svc.cluster.local/workout/GetBodyInfo");

            // Ensure we have a Success Status Code
            result.EnsureSuccessStatusCode();

            // Read Response Content (this will usually be JSON content)
            var content = await result.Content.ReadAsStringAsync();

            returncode = result.StatusCode;

            return Ok(content);
        }
    }
}
