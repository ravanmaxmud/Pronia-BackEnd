﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Areas.Admin.ViewModels.SubNavbars;
using PrioniaApp.Database;
using PrioniaApp.Database.Models;

namespace PrioniaApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/subnav")]
    public class SubNavbarController : Controller
    {
        private readonly DataContext _dataContext;

        public SubNavbarController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #region List

        [HttpGet("list", Name ="admin-subnav-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dataContext.SubNavbars
                .Select
                (s => new ListItemViewModel
                (s.Id, s.Name, s.ToURL, s.Order, s.Navbar.Name))
                .ToListAsync();
            return View(model);
        }
        #endregion

        #region Add
        [HttpGet("add", Name = "admin-subnav-add")]
        public async Task<IActionResult> Add()
        {
            var model = new AddViewModel
            {
                Navbar = await _dataContext.Navbars.Select(n => new NavbarListItemViewModel(n.Id, n.Name)).ToListAsync()
            };
            return View(model);
        }

        [HttpPost("add", Name = "admin-subnav-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (!await _dataContext.Navbars.AnyAsync(a => a.Id == model.NavbarId))
            {
                ModelState.AddModelError(String.Empty, "Navbar Is Not Found");
                return View(model);
            }
            if (await _dataContext.SubNavbars.AnyAsync(a => a.Order == model.Order))
            {
                var navModel = new AddViewModel
                {
                    Navbar = await _dataContext.Navbars.Select(n => new NavbarListItemViewModel(n.Id, n.Name)).ToListAsync()
                };
                ModelState.AddModelError(String.Empty, "Order is not be the same");
                return View(navModel);
            }

            var subNavbar = new SubNavbar
            {
                Name = model.Name,
                ToURL = model.ToURL,
                NavbarId = model.NavbarId,
                Order = model.Order,

            };
            await _dataContext.SubNavbars.AddAsync(subNavbar);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-subnav-list");
        }
        #endregion


        #region Update
        [HttpGet("update/{id}", Name = "admin-subnav-update")]
        public async Task<IActionResult> Update([FromRoute] int id)
        {
            var subNav = await _dataContext.SubNavbars.Include(s => s.Navbar).FirstOrDefaultAsync(s => s.Id == id);
            if (subNav == null)
            {
                return NotFound();
            }

            var model = new UpdateViewModel
            {
                Id = subNav.Id,
                Name = subNav.Name,
                ToURL = subNav.ToURL,
                Order = subNav.Order,
                NavbarId = subNav.NavbarId,
                Navbar = _dataContext.Navbars.Select(n => new NavbarListItemViewModel(n.Id, n.Name)).ToList()
            };
            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-subnav-update")]
        public async Task<IActionResult> Update(UpdateViewModel model)
        {
            var subNav = await _dataContext.SubNavbars.Include(s => s.Navbar).FirstOrDefaultAsync(s => s.Id == model.Id);
            if (subNav is null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (!_dataContext.Navbars.Any(a => a.Id == model.NavbarId))
            {
                ModelState.AddModelError(String.Empty, "Navbar Is Not Found");
                return View(model);
            }
            if (_dataContext.SubNavbars.Any(a => a.Order == model.Order))
            {
                var navModel = new AddViewModel
                {
                    Navbar = await _dataContext.Navbars.Select(n => new NavbarListItemViewModel(n.Id, n.Name)).ToListAsync()
                };
                ModelState.AddModelError(String.Empty, "Order is not be the same");
                return View(navModel);
            }

            subNav.Name = model.Name;
            subNav.Order = model.Order;
            subNav.NavbarId = model.NavbarId;
            subNav.ToURL = model.ToURL;

            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-subnav-list");
        }
        #endregion

        [HttpPost("delete/{id}", Name = "admin-subnav-delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var subNav = await _dataContext.SubNavbars.Include(s => s.Navbar).FirstOrDefaultAsync(s => s.Id == id);
            if (subNav == null)
            {
                return NotFound();
            }

            _dataContext.SubNavbars.Remove(subNav);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-subnav-list");
        }
    }
}
