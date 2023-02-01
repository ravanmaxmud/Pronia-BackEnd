using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PrioniaApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/blogfile")]
    [Authorize(Roles = "admin")]
    public class BlogFileController : Controller
    {

        public async Task<IActionResult> List()
        {
            return View();
        }
    }
}
