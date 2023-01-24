using Microsoft.AspNetCore.Mvc;
using PrioniaApp.Areas.Client.ViewModels.Authentication;

namespace PrioniaApp.Areas.Client.Controllers
{
    [Area("client")]
    [Route("auth")]
    public class AuthenticationController : Controller
    {
        [HttpGet("login",Name ="client-login")]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost("login", Name = "client-login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            return View();
        }


        [HttpGet("register", Name = "client-register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            return View();
        }
    }
}
