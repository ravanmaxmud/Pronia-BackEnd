using Microsoft.AspNetCore.Mvc;

namespace PrioniaApp.Areas.Client.Controllers
{
    [Area("client")]
    [Route("account")]
    public class BlogController : Controller
    {
        [HttpGet("blogindex",Name ="client-blog-index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
