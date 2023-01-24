using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Areas.Client.ViewModels.Home;
using PrioniaApp.Contracts.File;
using PrioniaApp.Database;
using PrioniaApp.Database.Models;
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
            };

            return View(model);
        }
        
        [HttpGet("GetModel/{id}", Name = "Product-GetModel")]
        public async Task<ActionResult> GetModelAsync(int id)
        {
            var product = await _dbContext.Products.Include(p => p.ProductImages)
                .Include(p => p.ProductCatagories)
                .Include(p => p.ProductTags)
                .Include(p => p.ProductColors)
                .Include(p => p.ProductSizes).FirstOrDefaultAsync(p => p.Id == id);


            if (product is null)
            {
                return NotFound();
            }

            var model = new ProductListItemViewModel(product.Id, product.Name, product.Description, product.Price,
                product.ProductImages!.Take(1).FirstOrDefault() != null
                ? _fileService.GetFileUrl(product.ProductImages.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Products)
                : String.Empty,
                   product.ProductImages!.Skip(1).Take(1).FirstOrDefault() != null
                ? _fileService.GetFileUrl(product.ProductImages.Skip(1).Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Products)
                : String.Empty,
                product.ProductCatagories!.Select(pc => pc.Catagory).Select(c => new ProductListItemViewModel.CategoryViewModeL(c.Title, c.Parent.Title)).ToList(),
                product.ProductColors!.Select(pc => pc.Color).Select(c => new ProductListItemViewModel.ColorViewModeL(c.Name)).ToList(),
                product.ProductSizes!.Select(ps => ps.Size).Select(s => new ProductListItemViewModel.SizeViewModeL(s.Title)).ToList(),
                product.ProductTags!.Select(ps => ps.Tag).Select(s => new ProductListItemViewModel.TagViewModel(s.Title)).ToList());

            return PartialView("~/Areas/Client/Views/Shared/Partials/_ProductModalPartial.cshtml", model);
        }
    }
}
