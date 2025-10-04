using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreB2CAuthSample.Controllers
{
   
        public class MicrosoftIdentityController : Controller
        {
            [Route("MicrosoftIdentity/Account/Error")]
            public IActionResult Error(string message)
            {
                var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
                var error = exceptionFeature?.Error;

                ViewData["ErrorMessage"] = error?.Message;
                ViewData["StackTrace"] = error?.StackTrace;

                return View();
            }
        }

    
}
