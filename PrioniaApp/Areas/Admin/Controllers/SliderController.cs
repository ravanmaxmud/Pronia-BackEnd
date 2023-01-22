using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Areas.Admin.ViewModels.Slider;
using PrioniaApp.Contracts.File;
using PrioniaApp.Database;
using PrioniaApp.Database.Models;
using PrioniaApp.Services.Abstracts;

namespace PrioniaApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/slider")]
    public class SliderController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public SliderController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        #region List
        [HttpGet("list",Name ="admin-slider-list")]
        public async  Task<IActionResult> List()
        {
            var model = await _dataContext.Sliders.Select(s => new ListItemViewModel(s.Id,s.HeaderTitle,s.MainTitle,s.Content,
                _fileService.GetFileUrl(s.BackgroundİmageInFileSystem,UploadDirectory.Slider),s.Button,s.ButtonRedirectUrl,s.CreatedAt))
                .ToListAsync();

            return View(model);
        }
        #endregion

        #region add
        [HttpGet("add", Name = "admin-slider-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost("add", Name = "admin-slider-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid) 
            {
                return View(model);
            } 
            

            var imageNameInSystem = await _fileService.UploadAsync(model.Backgroundİmage,UploadDirectory.Slider);

            AddSlider(model.Backgroundİmage.FileName,imageNameInSystem);

            return RedirectToRoute("admin-slider-list");


            async void AddSlider(string imageName ,string imageNameInSystem) 
            {
                var slider = new Slider
                {
                    HeaderTitle = model.HeaderTitle,
                    MainTitle = model.MainTitle,
                    Content = model.Content,
                    Backgroundİmage = imageName,
                    BackgroundİmageInFileSystem = imageNameInSystem,
                    CreatedAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                };

                await _dataContext.Sliders.AddAsync(slider);

                await _dataContext.SaveChangesAsync();
            }
        }

        #endregion
    }
}
