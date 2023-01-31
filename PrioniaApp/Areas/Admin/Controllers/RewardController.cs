using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Areas.Admin.ViewModels.Reward;
using PrioniaApp.Contracts.File;
using PrioniaApp.Database;
using PrioniaApp.Database.Models;
using PrioniaApp.Services.Abstracts;

namespace PrioniaApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/reward")]
    [Authorize(Roles = "admin")]
    public class RewardController : Controller
    {
        private readonly DataContext _dataContext;

        private readonly IFileService _fileService;

        public RewardController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        [HttpGet("List",Name ="admin-reward-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dataContext.Rewards.Select(r => new ListItemViewModel(r.Id,
                _fileService.GetFileUrl(r.ImageNameInFileSystem, Contracts.File.UploadDirectory.Reward),r.CreatedAt)).ToListAsync();

            return View(model);
        }


        [HttpGet("add", Name = "admin-reward-add")]
        public async Task<IActionResult> Add()
        {
            //var model = await _dataContext.Rewards.Select(r => new ListItemViewModel(r.Id,
            //    _fileService.GetFileUrl(r.ImageNameInFileSystem, Contracts.File.UploadDirectory.Reward), r.CreatedAt)).ToListAsync();

            return View();
        }

        [HttpPost("add", Name = "admin-reward-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var imageNameInSystem = await _fileService.UploadAsync(model.Image,Contracts.File.UploadDirectory.Reward);

            var reward = new Reward 
            {
                ImageName = model.Image.FileName,
                ImageNameInFileSystem = imageNameInSystem,
                CreatedAt = DateTime.Now,
                UpdateAt = DateTime.Now
            };

            await _dataContext.AddAsync(reward);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-reward-list");
        }


        [HttpPost("delete/{id}", Name = "admin-reward-delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var reward = await _dataContext.Rewards.FirstOrDefaultAsync(r=> r.Id == id);


            if (reward is null)
            {
                return NotFound();
            }

            await _fileService.DeleteAsync(reward.ImageNameInFileSystem, UploadDirectory.Reward);
            _dataContext.Rewards.Remove(reward);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-reward-list");
        }
    }
}
