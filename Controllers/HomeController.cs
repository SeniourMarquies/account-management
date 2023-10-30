using Microsoft.AspNetCore.Mvc;
using nop.gg.Models;
using System.Diagnostics;

namespace nop.gg.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Returns the "Index" view, typically the main page of the application.
            return View();
        }

        public IActionResult Privacy()
        {
            // Returns the "Privacy" view, typically a page that displays privacy-related information or settings.
            return View();
        }

        public IActionResult AccessDenied()
        {
            // Returns the "AccessDenied" view, which is displayed when a user does not have permission to access a certain resource.
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Returns the "Error" view, which is shown in case of an unhandled error.
            // It may display error information and a unique request identifier.
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
