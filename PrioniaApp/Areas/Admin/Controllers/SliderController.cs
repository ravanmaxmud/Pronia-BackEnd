using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Areas.Admin.ViewModels.Slider;
using PrioniaApp.Contracts.File;
using PrioniaApp.Database;
using PrioniaApp.Database.Models;
using PrioniaApp.Services.Abstracts;
using System.Data;

namespace PrioniaApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/slider")]
    [Authorize(Roles = "admin")]
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

            await _dataContext.SaveChangesAsync();

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
                    Button = model.Button,
                    ButtonRedirectUrl = model.ButtonRedirectUrl,
                    CreatedAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                };

                await _dataContext.Sliders.AddAsync(slider);
            }
        }

        #endregion


        #region update
        [HttpGet("update/{id}", Name = "admin-slider-update")]
        public async Task<IActionResult> Update([FromRoute]int id)
        {
            var slider = await _dataContext.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider is null)
            {
                return NotFound();
            }
            var model = new UpdateViewModel
            {
                Id = slider.Id,
                HeaderTitle = slider.HeaderTitle,
                MainTitle = slider.MainTitle,
                Content = slider.Content,
                BackgroundİmageUrl = _fileService.GetFileUrl(slider.BackgroundİmageInFileSystem,UploadDirectory.Slider),
                Button = slider.Button,
                ButtonRedirectUrl = slider.ButtonRedirectUrl
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-slider-update")]
        public async Task<IActionResult> Update(UpdateViewModel model)
        {

            var slider = await _dataContext.Sliders.FirstOrDefaultAsync(s => s.Id == model.Id);
            if (slider is null)
            {
                return NotFound();
            }


            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _fileService.DeleteAsync(model.Backgroundİmage.FileName,UploadDirectory.Slider);

            var backGroundImageFileSystem = await _fileService.UploadAsync(model.Backgroundİmage, UploadDirectory.Slider);


            await UpdateSliderImage(model.Backgroundİmage.FileName, backGroundImageFileSystem);

            return RedirectToRoute("admin-slider-list");
        
           
            async Task UpdateSliderImage(string imageName, string imageNameInSystem) 
            {
                slider.HeaderTitle = model.HeaderTitle;
                slider.MainTitle = model.MainTitle;
                slider.Content = model.Content;
                slider.Backgroundİmage = imageName;
                slider.BackgroundİmageInFileSystem = imageNameInSystem;
                slider.Button = model.Button;
                slider.ButtonRedirectUrl = model.ButtonRedirectUrl;
                slider.UpdateAt = DateTime.Now;

                await _dataContext.SaveChangesAsync();
            }
        }

        #endregion

        [HttpPost("delete/{id}", Name = "admin-slider-delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var slider = await _dataContext.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider is null)
            {
                return NotFound();
            }
            await _fileService.DeleteAsync(slider.BackgroundİmageInFileSystem, UploadDirectory.Slider);
            _dataContext.Sliders.Remove(slider);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-slider-list");
        }

    }
}
