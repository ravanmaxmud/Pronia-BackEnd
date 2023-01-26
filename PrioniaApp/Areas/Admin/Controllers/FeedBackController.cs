using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Areas.Admin.ViewModels.FeedBack;
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
    public class FeedBackController : Controller
    {

        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public FeedBackController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        [HttpGet("list", Name = "admin-clientsay-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dataContext.FeedBacks.Select(s => new ListItemViewModel(s.Id, s.User.FirstName, s.User.Roles.Name,_fileService.GetFileUrl(s.ImageNameInFileSystem,UploadDirectory.Feedback))).ToListAsync();

            return View(model);
        }

        [HttpGet("add", Name = "admin-clientsay-add")]
        public async Task<IActionResult> Add()
        {
            var model = new AddViewModel
            {
                Users = await _dataContext.Users.Select(u => new UserListItemViewModel(u.Id, u.FirstName, u.LastName,u.Email,u.Roles.Name)).ToListAsync(),
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

            var imageNameInSystem = await _fileService.UploadAsync(model.ImageName, UploadDirectory.Feedback);

            AddFeedBack(model.ImageName.FileName, imageNameInSystem);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-clientsay-list");

            async void AddFeedBack(string imageName, string imageNameInSystem)
            {
                var client = new PrioniaApp.Database.Models.FeedBack
                {
                    Content = model.Content,
                    CreatedAt = DateTime.Now,
                    UserId = model.UserId,
                    ImageName = imageName,
                    ImageNameInFileSystem = imageNameInSystem,
                };

                await _dataContext.FeedBacks.AddAsync(client);
            }
        }

        [HttpPost("delete/{id}", Name = "admin-clientsay-delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var slider = await _dataContext.FeedBacks.FirstOrDefaultAsync(s => s.Id == id);
            if (slider is null)
            {
                return NotFound();
            }
            await _fileService.DeleteAsync(slider.ImageNameInFileSystem, UploadDirectory.Feedback);
            _dataContext.FeedBacks.Remove(slider);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-clientsay-list");
        }
    }
}
