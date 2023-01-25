using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Areas.Admin.ViewModels.Navbars;
using PrioniaApp.Areas.Client.ViewCompanents;
using PrioniaApp.Database;
using PrioniaApp.Database.Models;
using System.Data;

namespace PrioniaApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/navbar")]
    [Authorize(Roles = "admin")]
    public class NavbarController : Controller
    {

        private readonly DataContext _dataContext;

        public NavbarController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        #region List
        [HttpGet("list", Name = "admin-nav-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Navbars
                .Select
                (n => new ListItemViewModel(n.Id, n.Name, n.ToURL, n.Order, n.CreatedAt))
                .ToListAsync();

            return View(model);
        }
        #endregion


        #region Add
        [HttpGet("add", Name = "admin-nav-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }


        [HttpPost("add", Name = "admin-nav-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await _dataContext.Navbars.AnyAsync(n => n.Order == model.Order))
            {
                ModelState.AddModelError(String.Empty, "Order is not be the same");
                return View(model);
            }

            var navbar = new PrioniaApp.Database.Models.Navbar
            {
                Name = model.Name,
                Order = model.Order,
                ToURL = model.ToURL,
                CreatedAt = DateTime.Now,
                UpdateAt = DateTime.Now,
            };

            await _dataContext.Navbars.AddAsync(navbar);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-nav-list");
        }
        #endregion

        #region update

        [HttpGet("update/{id}", Name = "admin-nav-update")]
        public async Task<IActionResult> Update([FromRoute] int id)
        {
            var navItem = await _dataContext.Navbars.FirstOrDefaultAsync(n => n.Id == id);

            if (navItem is null)
            {
                return NotFound();
            }

            var model = new UpdateViewModel
            {
                Id = navItem.Id,
                Name = navItem.Name,
                ToURL = navItem.ToURL,
                Order = navItem.Order
            };

            return View(model);
        }


        [HttpPost("update/{id}", Name = "admin-nav-update")]
        public async Task<IActionResult> Update(UpdateViewModel model)
        {
            var navItem = await _dataContext.Navbars.FirstOrDefaultAsync(n => n.Id == model.Id);
            if (navItem is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }


            navItem.Name = model.Name;
            navItem.Order = model.Order;
            navItem.ToURL = model.ToURL;


            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-nav-list");
        }
        #endregion

        #region delete

        [HttpPost("delete/{id}", Name = "admin-nav-delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var navItem = await _dataContext.Navbars.FirstOrDefaultAsync(n => n.Id == id);

            if (navItem is null)
            {
                return NotFound();
            }

             _dataContext.Navbars.Remove(navItem);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-nav-list");
        }
        #endregion
    }
}
