using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Areas.Client.ViewModels.Shop;
using PrioniaApp.Contracts.File;
using PrioniaApp.Database;
using PrioniaApp.Services.Abstracts;

namespace PrioniaApp.Areas.Client.Controllers
{
    [Area("client")]
    [Route("shop")]
    public class ShopController : Controller
    {

        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;
        public ShopController(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }


        [HttpGet("index/{id}",Name ="client-shop-index")]
        public async Task<IActionResult> Index(int id)
        {
            var product = await _dbContext.Products.Include(p => p.ProductImages)
                .Include(p => p.ProductColors)
                .Include(p => p.ProductSizes).FirstOrDefaultAsync(p => p.Id == id);


            if (product is null)
            {
                return NotFound();
            }

            var model = new ShopViewModel(product.Name, product.Description, product.Price,
  
                _dbContext.ProductColors.Include(pc => pc.Color).Where(pc => pc.ProductId == product.Id)
                .Select(pc => new ShopViewModel.ColorViewModeL(pc.Color.Name, pc.Color.Id)).ToList(),

                _dbContext.ProductSizes.Include(ps => ps.Size).Where(ps => ps.ProductId == product.Id)
                .Select(ps => new ShopViewModel.SizeViewModeL(ps.Size.Title, ps.Size.Id)).ToList(),

                _dbContext.ProductCatagories.Include(ps => ps.Catagory).Where(ps => ps.ProductId == product.Id)
                .Select(ps => new ShopViewModel.CatagoryViewModeL(ps.Catagory.Title, ps.Catagory.Id)).ToList(),

                  _dbContext.ProductTags.Include(ps => ps.Tag).Where(ps => ps.ProductId == product.Id)
                .Select(ps => new ShopViewModel.TagViewModeL(ps.Tag.Title, ps.Tag.Id)).ToList(),

                  _dbContext.ProductImages.Where(p=> p.ProductId == product.Id).Select(p=> new ShopViewModel.ImageViewModeL(_fileService.GetFileUrl(p.ImageNameInFileSystem,UploadDirectory.Products))).ToList()
                );
            return View(model);
        }
    }
}
