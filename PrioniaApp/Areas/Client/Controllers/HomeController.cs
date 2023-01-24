using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Areas.Client.ViewModels.Home;
using PrioniaApp.Contracts.File;
using PrioniaApp.Database;
using PrioniaApp.Services.Abstracts;

namespace PrioniaApp.Areas.Client.Controllers
{
    [Area("client")]
    [Route("home")]
    public class HomeController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;
        public HomeController(DataContext dbContext,IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        [HttpGet("~/", Name = "client-home-index")]
        [HttpGet("home-index")]
        public async Task<IActionResult> Index()
        {
            var model = new IndexViewModel
            {
                Sliders = await _dbContext.Sliders.Select(s => new SliderViewModel(s.Id, s.HeaderTitle, s.MainTitle, s.Content, s.Button, s.ButtonRedirectUrl,
                _fileService.GetFileUrl(s.BackgroundİmageInFileSystem, UploadDirectory.Slider))).ToListAsync(),


                Products = await _dbContext.Products.Select(p=> new ProductListItemViewModel(p.Id,p.Name,p.Description,p.Price,
                p.ProductImages!.Take(1).FirstOrDefault() != null
                ? _fileService.GetFileUrl(p.ProductImages.Take(1).FirstOrDefault()!.ImageNameInFileSystem,UploadDirectory.Products)
                :String.Empty,
                   p.ProductImages!.Skip(1).Take(1).FirstOrDefault() != null
                ? _fileService.GetFileUrl(p.ProductImages.Skip(1).Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Products)
                : String.Empty,
                p.ProductCatagories!.Select(pc => pc.Catagory).Select(c => new ProductListItemViewModel.CategoryViewModeL(c.Title, c.Parent.Title)).ToList(),
                p.ProductColors!.Select(pc => pc.Color).Select(c => new ProductListItemViewModel.ColorViewModeL(c.Name)).ToList(),
                p.ProductSizes!.Select(ps => ps.Size).Select(s => new ProductListItemViewModel.SizeViewModeL(s.Title)).ToList(),
                p.ProductTags!.Select(ps => ps.Tag).Select(s => new ProductListItemViewModel.TagViewModel(s.Title)).ToList()))
                .ToListAsync(),
            };

            return View(model);
        }
    }
}
