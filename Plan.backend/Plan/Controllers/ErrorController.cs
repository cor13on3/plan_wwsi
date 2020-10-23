using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Plan.Core.Exceptions;

namespace Plan.API.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;
            var kod = 500;
            if (exception is BladBiznesowy) kod = 412;
            return Problem(detail: context.Error.Message, statusCode: kod, title: "Błąd!");
        }
    }
}
