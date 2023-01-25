using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PrioniaApp.Areas.Client.Controllers
{
    [Area("client")]
    [Route("account")]
    [Authorize]
    public class AccountController : Controller
    {
        [HttpGet("dashboard",Name ="client-account-dashboard")]
        public IActionResult DashBoard()
        {
            return View();
        }

        [HttpGet("order", Name = "client-account-order")]
        public IActionResult Order()
        {
            return View();
        }

        [HttpGet("address", Name = "client-account-address")]
        public IActionResult Address()
        {
            return View();
        }

        [HttpGet("details", Name = "client-account-details")]
        public IActionResult Details()
        {
            return View();
        }
    }
}
