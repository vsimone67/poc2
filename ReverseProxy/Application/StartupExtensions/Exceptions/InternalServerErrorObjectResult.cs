using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReverseProxy.Extensions
{
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object error)
            : base(error)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }

}
