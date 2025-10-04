using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreB2CAuthSample.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
