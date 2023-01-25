using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrioniaApp.Database;

namespace PrioniaApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/clientsay")]
    [Authorize(Roles = "admin")]
    public class ClientSayController : Controller
    {

        private readonly DataContext _dataContext;

        public ClientSayController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("list", Name = "admin-clientsay-list")]
        public async Task<IActionResult> List()
        {
            return View();
        }
    }
}
