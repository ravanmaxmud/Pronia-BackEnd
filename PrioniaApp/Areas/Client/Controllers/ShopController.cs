using Microsoft.AspNetCore.Mvc;

namespace PrioniaApp.Areas.Client.Controllers
{
    [Area("client")]
    [Route("shop")]
    public class ShopController : Controller
    {
        [HttpGet("index",Name ="client-shop-index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
