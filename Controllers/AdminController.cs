using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace nop.gg.Controllers
{
    [Authorize(Roles = "admin")] // Specifies that only users with the "admin" role can access the methods in this controller.
    public class AdminController : Controller
    {
        [Authorize] // Specifies that only authenticated users can access the "Index" method.
        public IActionResult Index()
        {
            // Returns the "Index" view, which is accessible to authenticated users with the "admin" role.
            return View();
        }
    }
}
