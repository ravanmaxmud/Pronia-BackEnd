using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Areas.Admin.ViewModels.Navbars;
using PrioniaApp.Database;

namespace PrioniaApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("navbar")]
    public class NavbarController : Controller
    {

        private readonly DataContext _dataContext;

        public NavbarController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("list", Name = "admin-nav-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Navbars
                .Select
                (n =>new ListItemViewModel(n.Id, n.Name, n.ToURL, n.Order, n.CreatedAt))
                .ToListAsync();

            return View(model);
        }
    }
}
