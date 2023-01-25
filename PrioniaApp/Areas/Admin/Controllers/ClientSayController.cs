using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Areas.Admin.ViewModels.ClientSay;
using PrioniaApp.Contracts.File;
using PrioniaApp.Database;
using PrioniaApp.Migrations;
using PrioniaApp.Services.Abstracts;
using PrioniaApp.Database.Models;

namespace PrioniaApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/clientsay")]
    [Authorize(Roles = "admin")]
    public class ClientSayController : Controller
    {

        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public ClientSayController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        [HttpGet("list", Name = "admin-clientsay-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dataContext.ClientSays.Select(s => new ListItemViewModel(s.Id, s.User.FirstName, s.User.Roles.Name)).ToListAsync();

            return View(model);
        }

        [HttpGet("add", Name = "admin-clientsay-add")]
        public async Task<IActionResult> Add()
        {
            var model = new AddViewModel
            {
                Users = await _dataContext.Users.Select(u => new UserListItemViewModel(u.Id, u.FirstName, u.Roles.Name)).ToListAsync(),
            };

            return View(model);
        }

        [HttpPost("add", Name = "admin-clientsay-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var client = new PrioniaApp.Database.Models.ClientSay
            {
                Content = model.Content,
                CreatedAt = DateTime.Now,
                UserId = model.UserId,
            };
            await _dataContext.ClientSays.AddAsync(client);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-clientsay-list");
        }
    }
}
